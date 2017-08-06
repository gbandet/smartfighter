from collections import defaultdict
from operator import itemgetter

from django.db.models import Prefetch, Q
from django_filters import FilterSet
from rest_framework.decorators import detail_route
from rest_framework.mixins import ListModelMixin
from rest_framework.response import Response
from rest_framework.viewsets import GenericViewSet, ReadOnlyModelViewSet
from rest_framework_extensions.mixins import NestedViewSetMixin

from smartfighter.apps.ranking.models import (
    CHARACTERS,
    Game,
    GamePhase,
    MatchResult,
    Player,
    PlayerResults,
    Round,
    RoundResult,
    Season,
)
from smartfighter.front.serializers import (
    GameSerializer,
    PlayerSerializer,
    SeasonSerializer,
    SimplePlayerSerializer,
    SimpleSeasonSerializer,
)


class GameFilter(FilterSet):
    class Meta:
        model = Game
        fields = {
            'season': ['exact', 'isnull'],
            'phase': ['exact'],
            'date': ['gt', 'gte', 'lt', 'lte'],
        }


class GameViewSet(ReadOnlyModelViewSet):
    queryset = Game.objects.all()
    serializer_class = GameSerializer
    ordering_fields = ('date',)
    ordering = '-date'
    filter_class = GameFilter


class PlayerFilter(FilterSet):
    class Meta:
        model = Player
        fields = {
            'name': ['exact', 'icontains'],
        }


class PlayerViewSet(ReadOnlyModelViewSet):
    queryset = Player.objects.all()
    serializer_class = PlayerSerializer
    lookup_field = 'name'
    ordering_fields = ('name',)
    ordering = 'name'
    filter_class = PlayerFilter

    def list(self, request, *args, **kwargs):
        self.serializer_class = SimplePlayerSerializer
        return super(PlayerViewSet, self).list(request, *args, **kwargs)

    @detail_route(methods=['get'])
    def stats(self, request, name=None):
        player = self.get_object()
        game_filters = self._get_game_filters()

        response = {}
        season = game_filters.get('season')
        if season:
            result = player.season_results.get(season=season)
            response['season'] = {
                'id': season.id,
                'name': season.name,
                'rating': result.elo_rating,
                'min_rating': result.min_rating,
                'max_rating': result.max_rating,

            }

        opponents = {}
        characters = {}
        def get_opponent(player):
            return opponents.setdefault(player.card_id, defaultdict(int, {
                'id': player.card_id,
                'name': player.name,
                'round_statuses': {},
            }))

        def get_characters_stats(player, opponent):
            player_stats = characters.setdefault(player, defaultdict(int, {
                'code': player,
                'name': CHARACTERS.get(player),
                'opponents': {},
            }))
            opponent_stats = player_stats['opponents'].setdefault(opponent, defaultdict(int, {
                'code': opponent,
                'name': CHARACTERS.get(opponent),
            }))
            return player_stats, opponent_stats

        for game in player.games_as_first_player.filter(**game_filters).select_related():
            opponent = get_opponent(game.player2)
            opponent['count'] += 1
            opponent['wins'] += 1 if game.result == MatchResult.Player1 else 0
            opponent['draws'] += 1 if game.result == MatchResult.Draw else 0
            opponent['losses'] += 1 if game.result == MatchResult.Player2 else 0
            if game.player1_character and game.player2_character:
                total_stats, stats = get_characters_stats(
                    game.player1_character, game.player2_character)
                total_stats['count'] += 1
                stats['count'] += 1
                total_stats['wins'] += 1 if game.result == MatchResult.Player1 else 0
                stats['wins'] += 1 if game.result == MatchResult.Player1 else 0
                total_stats['draws'] += 1 if game.result == MatchResult.Draw else 0
                stats['draws'] += 1 if game.result == MatchResult.Draw else 0
                total_stats['losses'] += 1 if game.result == MatchResult.Player2 else 0
                stats['losses'] += 1 if game.result == MatchResult.Player2 else 0

        for game in player.games_as_second_player.filter(**game_filters).select_related():
            opponent = get_opponent(game.player1)
            opponent['count'] += 1
            opponent['wins'] += 1 if game.result == MatchResult.Player2 else 0
            opponent['draws'] += 1 if game.result == MatchResult.Draw else 0
            opponent['losses'] += 1 if game.result == MatchResult.Player1 else 0
            if game.player1_character and game.player2_character:
                total_stats, stats = get_characters_stats(
                    game.player2_character, game.player1_character)
                total_stats['count'] += 1
                stats['count'] += 1
                total_stats['wins'] += 1 if game.result == MatchResult.Player2 else 0
                stats['wins'] += 1 if game.result == MatchResult.Player2 else 0
                total_stats['draws'] += 1 if game.result == MatchResult.Draw else 0
                stats['draws'] += 1 if game.result == MatchResult.Draw else 0
                total_stats['losses'] += 1 if game.result == MatchResult.Player1 else 0
                stats['losses'] += 1 if game.result == MatchResult.Player1 else 0

        round_filters = {'game__' + k: v for k, v in game_filters.items()}
        for round_ in Round.objects.filter(Q(game__player1=player, **round_filters) |
                                           Q(game__player2=player, **round_filters)
                                          ).select_related():
            if round_.game.player1 == player:
                opponent = get_opponent(round_.game.player2)
                opponent['round_count'] += 1
                opponent['round_wins'] += 1 if round_.result == MatchResult.Player1 else 0
                opponent['round_draws'] += 1 if round_.result == MatchResult.Draw else 0
                opponent['round_losses'] += 1 if round_.result == MatchResult.Player2 else 0
                round_status = opponent['round_statuses'].setdefault(round_.player1, defaultdict(int, {
                    'code': round_.player1,
                    'label': RoundResult.choices_dict.get(round_.player1),
                }))
                round_status['count'] += 1
            else:
                opponent = get_opponent(round_.game.player1)
                opponent['round_count'] += 1
                opponent['round_wins'] += 1 if round_.result == MatchResult.Player2 else 0
                opponent['round_draws'] += 1 if round_.result == MatchResult.Draw else 0
                opponent['round_losses'] += 1 if round_.result == MatchResult.Player1 else 0
                round_status = opponent['round_statuses'].setdefault(round_.player2, defaultdict(int, {
                    'code': round_.player2,
                    'label': RoundResult.choices_dict.get(round_.player2),
                }))
                round_status['count'] += 1

        opponents = sorted(opponents.values(), key=itemgetter('count'), reverse=True)
        characters = sorted(characters.values(), key=itemgetter('count'), reverse=True)

        totals = defaultdict(int)
        totals['round_statuses'] = {}
        for opponent in opponents:
            totals['count'] += opponent['count']
            totals['wins'] += opponent['wins']
            totals['draws'] += opponent['draws']
            totals['losses'] += opponent['losses']
            totals['round_count'] += opponent['round_count']
            totals['round_wins'] += opponent['round_wins']
            totals['round_draws'] += opponent['round_draws']
            totals['round_losses'] += opponent['round_losses']
            for status_code, round_status in opponent['round_statuses'].items():
                total_status = totals['round_statuses'].setdefault(status_code, defaultdict(int, {
                    'code': round_status['code'],
                    'label': round_status['label'],
                }))
                total_status['count'] += round_status['count']

        response['opponents'] = opponents
        response['global'] = totals
        response['characters'] = characters

        return Response(response)

    def _get_game_filters(self):
        param = self.request.GET.get('season')
        if param is None:
            return {}
        if param == 'unranked':
            return {'season': None, 'phase': GamePhase.Unranked}
        try:
            season = Season.objects.get(pk=param)
        except (ValueError, Season.DoesNotExist):
            return {}
        return {'season': season, 'phase': GamePhase.Ranked}


class SeasonFilter(FilterSet):
    class Meta:
        model = Season
        fields = {
            'name': ['exact', 'icontains'],
            'start_date': ['gt', 'gte', 'lt', 'lte'],
            'end_date': ['gt', 'gte', 'lt', 'lte'],
        }


class SeasonGameViewSet(NestedViewSetMixin, ListModelMixin, GenericViewSet):
    queryset = Game.objects.filter(phase=GamePhase.Ranked).select_related()
    serializer_class = GameSerializer
    ordering_fields = ('date',)
    ordering = '-date'
    filter_class = GameFilter


class SeasonViewSet(NestedViewSetMixin, ReadOnlyModelViewSet):
    queryset = Season.objects.all()
    serializer_class = SeasonSerializer
    ordering_fields = ('id', 'name', 'start_date', 'end_date')
    ordering = '-start_date'
    filter_class = SeasonFilter

    def list(self, request, *args, **kwargs):
        self.serializer_class = SimpleSeasonSerializer
        return super(SeasonViewSet, self).list(request, *args, **kwargs)

    @detail_route(methods=['get'])
    def ranking(self, request, pk=None):
        season = self.get_object()
        response = {
            'ranking': [],
            'placement': [],
        }

        current_rank = 0
        next_rank = 0
        current_elo = None
        for results in PlayerResults.objects.filter(season=season).select_related(
                'player').prefetch_related(
                    Prefetch('player__games_as_first_player',
                             queryset=Game.objects.filter(season=season)),
                    Prefetch('player__games_as_second_player',
                             queryset=Game.objects.filter(season=season))).order_by('-elo_rating'):

            total = 0
            wins = 0
            draws = 0
            losses = 0
            for game in results.player.games_as_first_player.all():
                total += 1
                if game.result == MatchResult.Player1:
                    wins += 1
                elif game.result == MatchResult.Draw:
                    draws += 1
                else:
                    losses += 1
            for game in results.player.games_as_second_player.all():
                total += 1
                if game.result == MatchResult.Player2:
                    wins += 1
                elif game.result == MatchResult.Draw:
                    draws += 1
                else:
                    losses += 1
            data = {
                'name': results.player.name,
                'rating': results.elo_rating,
                'games': total,
                'wins': wins,
                'draws': draws,
                'losses': losses,
            }
            if total >= season.placement_games:
                next_rank += 1
                if not current_elo or results.elo_rating != current_elo:
                    current_rank = next_rank
                    current_elo = results.elo_rating
                data['rank'] = current_rank
                response['ranking'].append(data)
            elif total > 0:
                response['placement'].append(data)

        return Response(response)

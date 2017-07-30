from collections import defaultdict
import json
from operator import itemgetter

from django.db.models import Count, F, Prefetch, Q
from django.shortcuts import get_object_or_404
from django.urls import reverse
from django.utils import timezone
from django.views.generic import RedirectView, TemplateView
from rest_framework.decorators import detail_route
from rest_framework.response import Response
from rest_framework.viewsets import ReadOnlyModelViewSet

from smartfighter.apps.ranking.models import Game, GamePhase, MatchResult, Player, PlayerResults, Round, RoundResult, Season
from smartfighter.apps.ranking.serializers import SeasonSerializer


class SeasonViewSet(ReadOnlyModelViewSet):
    queryset = Season.objects.all()
    serializer_class = SeasonSerializer
    pagination_class = None

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


class IndexView(RedirectView):
    def get_redirect_url(self, *args, **kwargs):
        season = Season.objects.order_by('-id').first()
        if season:
            return reverse('season', kwargs={'season_id': season.pk})
        else:
            return reverse('unranked')


class BaseView(TemplateView):
    def get_context_data(self, **kwargs):
        context = super(BaseView, self).get_context_data(**kwargs)
        context['last_seasons'] = Season.objects.order_by('-id')
        return context


class SeasonListView(BaseView):
    template_name = 'front/season_list.html'

    def get_context_data(self, **kwargs):
        context = super(SeasonListView, self).get_context_data(**kwargs)
        context['seasons'] = Season.objects.order_by('-id')
        return context


class SeasonView(BaseView):
    template_name = 'front/season.html'

    def get_context_data(self, season_id, **kwargs):
        context = super(SeasonView, self).get_context_data(**kwargs)
        season = get_object_or_404(Season, pk=season_id)

        context['season'] = season
        context['games'] = Game.objects.filter(
            season=season, phase=GamePhase.Ranked).select_related().order_by('-date')[:20]
        context['ranking'] = []
        context['placement'] = []

        for results in PlayerResults.objects.filter(season=season).select_related(
            'player').prefetch_related(
                Prefetch('player__games_as_first_player',
                         queryset=Game.objects.filter(season=season)),
                Prefetch('player__games_as_second_player',
                         queryset=Game.objects.filter(season=season))).order_by('-elo_rating'):
            games_played = results.player.games_as_first_player.count() + results.player.games_as_second_player.count()
            if games_played >= season.placement_games:
                context['ranking'].append(results)
            elif games_played > 0:
                context['placement'].append(results)

        context['is_finished'] = season.end_date and season.end_date < timezone.now()
        if season.playoff_data:
            try:
                context['playoffs'] = json.loads(season.playoff_data)
            except ValueError:
                pass
        return context


class UnrankedView(BaseView):
    template_name = 'front/unranked.html'

    def get_context_data(self, **kwargs):
        context = super(UnrankedView, self).get_context_data(**kwargs)
        context['games'] = Game.objects.filter(
            phase=GamePhase.Unranked).select_related().order_by('-date')[:50]
        return context


class PlayerView(BaseView):
    template_name = 'front/player.html'

    def _get_game_filters(self):
        param = self.request.GET.get('season')
        if param is None:
            return {}
        if param == 'unranked':
            return {'season': None, 'phase': GamePhase.Unranked}
        try:
            season = Season.objects.get(pk=param)
        except ValueError, Season.DoesNotExist:
            return {}
        return {'season': season, 'phase': GamePhase.Ranked}

    def get_context_data(self, name, **kwargs):
        context = super(PlayerView, self).get_context_data(**kwargs)
        player = get_object_or_404(Player, name=name)

        opponents = {}
        def get_opponent(player):
            if player.name not in opponents:
                opponents[player.name] = defaultdict(int)
                opponents[player.name]['player'] = player
            return opponents[player.name]

        game_filters = self._get_game_filters()
        context['season'] = game_filters.get('season')
        context['unranked'] = game_filters.get('phase') == GamePhase.Unranked
        context['player_seasons'] = Season.objects.filter(player_results__player=player).order_by('-id')
        if game_filters.get('season'):
            context['result'] = player.season_results.get(season=game_filters['season'])

        for game in player.games_as_first_player.filter(**game_filters).select_related():
            opponent = get_opponent(game.player2)
            opponent['total'] += 1
            opponent['win'] += 1 if game.result == MatchResult.Player1 else 0
            opponent['draw'] += 1 if game.result == MatchResult.Draw else 0
            opponent['lose'] += 1 if game.result == MatchResult.Player2 else 0

        for game in player.games_as_second_player.filter(**game_filters).select_related():
            opponent = get_opponent(game.player1)
            opponent['total'] += 1
            opponent['win'] += 1 if game.result == MatchResult.Player2 else 0
            opponent['draw'] += 1 if game.result == MatchResult.Draw else 0
            opponent['lose'] += 1 if game.result == MatchResult.Player1 else 0

        round_filters = {'game__' + k: v for k,v in game_filters.items()}
        for round_ in Round.objects.filter(
                    Q(game__player1=player, **round_filters) | Q(game__player2=player, **round_filters)
                ).select_related():
            if round_.game.player1 == player:
                opponent = get_opponent(round_.game.player2)
                opponent['round_total'] += 1
                opponent['round_win'] += 1 if round_.result == MatchResult.Player1 else 0
                opponent['round_draw'] += 1 if round_.result == MatchResult.Draw else 0
                opponent['round_lose'] += 1 if round_.result == MatchResult.Player2 else 0
                opponent['round_V'] += 1 if round_.player1 == RoundResult.Victory else 0
                opponent['round_CA'] += 1 if round_.player1 == RoundResult.CriticalArt else 0
                opponent['round_EX'] += 1 if round_.player1 == RoundResult.Ex else 0
                opponent['round_C'] += 1 if round_.player1 == RoundResult.Chip else 0
                opponent['round_P'] += 1 if round_.player1 == RoundResult.Perfect else 0
                opponent['round_T'] += 1 if round_.player1 == RoundResult.TimeOut else 0
                opponent['round_D'] += 1 if round_.player1 == RoundResult.DoubleKO else 0
            else:
                opponent = get_opponent(round_.game.player1)
                opponent['round_total'] += 1
                opponent['round_win'] += 1 if round_.result == MatchResult.Player2 else 0
                opponent['round_draw'] += 1 if round_.result == MatchResult.Draw else 0
                opponent['round_lose'] += 1 if round_.result == MatchResult.Player1 else 0
                opponent['round_V'] += 1 if round_.player2 == RoundResult.Victory else 0
                opponent['round_CA'] += 1 if round_.player2 == RoundResult.CriticalArt else 0
                opponent['round_EX'] += 1 if round_.player2 == RoundResult.Ex else 0
                opponent['round_C'] += 1 if round_.player2 == RoundResult.Chip else 0
                opponent['round_P'] += 1 if round_.player2 == RoundResult.Perfect else 0
                opponent['round_T'] += 1 if round_.player2 == RoundResult.TimeOut else 0
                opponent['round_D'] += 1 if round_.player2 == RoundResult.DoubleKO else 0


        opponents = sorted(opponents.values(), key=itemgetter('total'), reverse=True)

        totals = defaultdict(int)
        for opponent in opponents:
            totals['total'] += opponent['total']
            totals['win'] += opponent['win']
            totals['draw'] += opponent['draw']
            totals['lose'] += opponent['lose']
            totals['round_total'] += opponent['round_total']
            totals['round_win'] += opponent['round_win']
            totals['round_draw'] += opponent['round_draw']
            totals['round_lose'] += opponent['round_lose']
            totals['round_V'] += opponent['round_V']
            totals['round_CA'] += opponent['round_CA']
            totals['round_EX'] += opponent['round_EX']
            totals['round_C'] += opponent['round_C']
            totals['round_P'] += opponent['round_P']
            totals['round_T'] += opponent['round_T']
            totals['round_D'] += opponent['round_D']

        context['player'] = player
        context['win_rate'] = 100. * totals['win'] / totals['total'] if totals['total'] else '-'
        context['totals'] = totals
        context['opponents'] = opponents
        return context

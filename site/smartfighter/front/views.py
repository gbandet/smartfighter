from collections import defaultdict
from operator import itemgetter

from django.db.models import Count, F, Q
from django.views.generic import TemplateView
from django.http import Http404

from smartfighter.apps.ranking.models import Game, MatchResult, Player, Round, RoundResult

class IndexView(TemplateView):
    template_name = 'front/index.html'

    def get_context_data(self, **kwargs):
        context = super(TemplateView, self).get_context_data(**kwargs)
        context['games'] = Game.objects.all().select_related().order_by('-date')[:20]
        context['ranking'] = []
        context['placement'] = []
        for player in Player.objects.prefetch_related(
                'games_as_first_player', 'games_as_second_player').order_by('-elo_rating'):
            games_played = player.games_as_first_player.count() + player.games_as_second_player.count()
            if games_played >= 15:
                context['ranking'].append(player)
            elif games_played > 0:
                 context['placement'].append(player)
        return context


class PlayerView(TemplateView):
    template_name = 'front/player.html'

    def get_context_data(self, name, **kwargs):
        context = super(TemplateView, self).get_context_data(**kwargs)
        try:
            player = Player.objects.get(name=name)
        except Player.DoesNotExist:
            raise Http404("Player %s does not exists." % name)

        opponents = {}
        def get_opponent(player):
            if player.name not in opponents:
                opponents[player.name] = defaultdict(int)
                opponents[player.name]['player'] = player
            return opponents[player.name]

        for game in player.games_as_first_player.select_related():
            opponent = get_opponent(game.player2)
            opponent['total'] += 1
            opponent['win'] += 1 if game.result == MatchResult.Player1 else 0
            opponent['draw'] += 1 if game.result == MatchResult.Draw else 0
            opponent['lose'] += 1 if game.result == MatchResult.Player2 else 0

        for game in player.games_as_second_player.select_related():
            opponent = get_opponent(game.player1)
            opponent['total'] += 1
            opponent['win'] += 1 if game.result == MatchResult.Player2 else 0
            opponent['draw'] += 1 if game.result == MatchResult.Draw else 0
            opponent['lose'] += 1 if game.result == MatchResult.Player1 else 0

        for round_ in Round.objects.filter(Q(game__player1=player) | Q(game__player2=player)).select_related():
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

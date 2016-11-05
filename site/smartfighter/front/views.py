from django.db.models import Count, Q
from django.views.generic import TemplateView

from smartfighter.apps.ranking.models import Game, Player

class IndexView(TemplateView):
    template_name = 'front/index.html'

    def get_context_data(self, **kwargs):
        context = super(TemplateView, self).get_context_data(**kwargs)
        context['games'] = Game.objects.all().select_related().order_by('-date')[:20]
        context['ranking'] =  Player.objects.annotate(
            first=Count('games_as_first_player')).annotate(second=Count('games_as_second_player')).filter(
                Q(first__gt=0)|Q(second__gt=0)).prefetch_related(
                    'games_as_first_player', 'games_as_second_player').order_by('-elo_rating')
        return context

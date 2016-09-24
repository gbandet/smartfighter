from django.views.generic import TemplateView

from smartfighter.apps.ranking.models import Game

class GameListView(TemplateView):
    template_name = 'front/game_list.html'

    def get_context_data(self, **kwargs):
        context = super(TemplateView, self).get_context_data(**kwargs)
        context['games'] = Game.objects.all().select_related()[:5]
        return context


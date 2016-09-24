from django.views.generic import TemplateView

from smartfighter.apps.ranking.models import Game, Player

class IndexView(TemplateView):
    template_name = 'front/index.html'

    def get_context_data(self, **kwargs):
        context = super(TemplateView, self).get_context_data(**kwargs)
        context['games'] = Game.objects.all().select_related()[:5]
        context['ranking'] = Player.objects.all().order_by('-elo_rating')[:10]
        return context


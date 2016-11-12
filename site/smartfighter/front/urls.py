from django.conf.urls import url
from django.views.generic import TemplateView

from smartfighter.front.views import IndexView, PlayerView

urlpatterns = [
    url(r'^$', IndexView.as_view(), name='ranking'),
    url(r'^player/(?P<name>.*)$', PlayerView.as_view(), name='player'),
    url(r'^instructions', TemplateView.as_view(template_name='front/instructions.html'),
        name='instructions')
]

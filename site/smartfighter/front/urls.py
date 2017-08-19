from django.conf.urls import url

from smartfighter.front.views import BaseView, IndexView, PlayerView, SeasonListView, SeasonView, UnrankedView


urlpatterns = [
    url(r'^$', IndexView.as_view(), name='index'),
    url(r'^season/(?P<season_id>\d+)$', SeasonView.as_view(), name='season'),
    url(r'^season$', SeasonListView.as_view(), name='season-list'),
    url(r'^unranked$', UnrankedView.as_view(), name='unranked'),
    url(r'^player/(?P<name>.*)$', PlayerView.as_view(), name='player'),
    url(r'^instructions', BaseView.as_view(template_name='front/instructions.html'),
        name='instructions')
]
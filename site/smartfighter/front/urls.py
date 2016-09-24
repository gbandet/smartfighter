from django.conf.urls import url

from smartfighter.front.views import GameListView

urlpatterns = [
    url(r'^$', GameListView.as_view(), name='game-list'),
]

from django.conf.urls import url

from smartfighter.front.views import IndexView

urlpatterns = [
    url(r'^$', IndexView.as_view(), name='index'),
]

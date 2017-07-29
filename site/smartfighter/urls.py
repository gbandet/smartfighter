from django.conf.urls import include, url

from smartfighter import api, front

urlpatterns = [
    url(r'^api/', include(api.router.urls)),
    url(r'^ui/', include(front.router.urls)),
    url(r'^', include('smartfighter.front.urls')),
]

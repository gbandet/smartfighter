from django.conf.urls import include, url

from smartfighter.api import router


urlpatterns = [
    url(r'^api/', include(router.urls)),
    url(r'^', include('smartfighter.front.urls')),
]

from rest_framework.routers import DefaultRouter

from smartfighter.front.views import SeasonViewSet


router = DefaultRouter()
router.register(r'seasons', SeasonViewSet)

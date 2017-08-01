from rest_framework.routers import DefaultRouter

from smartfighter.front.views import GameViewSet, SeasonViewSet


router = DefaultRouter()
router.register(r'games', GameViewSet)
router.register(r'seasons', SeasonViewSet)

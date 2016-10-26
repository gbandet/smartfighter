from rest_framework.routers import DefaultRouter

from smartfighter.api.views import GameViewSet, PlayerViewSet


router = DefaultRouter()
router.register(r'game', GameViewSet)
router.register(r'player', PlayerViewSet)

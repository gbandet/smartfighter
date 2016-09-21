from rest_framework.routers import DefaultRouter

from smartfighter.api.views import GameViewSet


router = DefaultRouter()
router.register(r'game', GameViewSet)

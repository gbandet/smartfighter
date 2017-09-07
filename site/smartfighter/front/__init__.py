from rest_framework_extensions.routers import ExtendedDefaultRouter

from smartfighter.front.views import GameViewSet, PlayerViewSet, SeasonGameViewSet, SeasonViewSet


router = ExtendedDefaultRouter(trailing_slash=False)
router.register(r'games', GameViewSet)
router.register(r'players', PlayerViewSet)
router.register(r'seasons', SeasonViewSet).register(
    r'games', SeasonGameViewSet, base_name='season-games', parents_query_lookups=['season'])

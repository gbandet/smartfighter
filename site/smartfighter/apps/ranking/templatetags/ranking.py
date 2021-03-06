from django import template

from smartfighter.apps.ranking.models import MatchResult, CHARACTERS


register = template.Library()


@register.filter
def win_percentage(player):
    match_total = 0
    win_total = 0
    for game in player.games_as_first_player.all():
        match_total += 1
        if game.result == MatchResult.Player1:
            win_total += 1
    for game in player.games_as_second_player.all():
        match_total += 1
        if game.result == MatchResult.Player2:
            win_total += 1

    if match_total == 0:
        return '-'
    return int(round(100 * win_total / float(match_total)))

@register.filter
def character_name(code):
    if not code:
        return None
    return CHARACTERS.get(code, 'Unknown')

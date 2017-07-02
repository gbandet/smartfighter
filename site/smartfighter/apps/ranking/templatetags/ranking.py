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


@register.filter
def best_character_of_player(player):
    match_total = {}
    win_total = {}
    for game in player.games_as_first_player.all():
        if game.player1_character not in match_total:
            match_total[game.player1_character] = 0
            win_total[game.player1_character] = 0
        match_total[game.player1_character] += 1
        if game.result == MatchResult.Player1:
            win_total[game.player1_character] += 1

    for game in player.games_as_second_player.all():
        if game.player2_character not in match_total:
            match_total[game.player2_character] = 0
            win_total[game.player2_character] = 0
        match_total[game.player2_character] += 1
        if game.result == MatchResult.Player2:
            win_total[game.player2_character] += 1

    best_percent = 0
    best_character = ''
    for character in CHARACTERS:
        if character not in win_total:
            continue
        character_percent = int(round(100 * win_total[character] / float(match_total[character])))
        if best_percent > character_percent:
            continue
        best_percent = character_percent
        best_character = character

    return CHARACTERS.get(best_character, '-')

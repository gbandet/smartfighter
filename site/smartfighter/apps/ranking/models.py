from __future__ import unicode_literals

from django.db import models


class MatchResult(object):
    Player1 = 1
    Player2 = 2
    Draw = 0

    choices = (
        (Player1, 'Player1'),
        (Player2, 'Player2'),
        (Draw, 'Draw'),
    )


class Player(models.Model):
    card_id = models.CharField(max_length=8, primary_key=True)
    name = models.CharField(max_length=255)

    def __str__(self):
        return self.name


class Game(models.Model):
    id = models.CharField(max_length=32, primary_key=True)
    player1 = models.ForeignKey(Player, related_name="games_as_first_player")
    player2 = models.ForeignKey(Player, related_name="games_as_second_player")
    result = models.IntegerField(choices=MatchResult.choices)

    @property
    def rounds(self):
        return self.round_set.order_by('order')


class RoundResult(object):
    Loss = 1
    Victory = 2
    CriticalArt = 3
    Ex = 4
    Chip = 5
    Perfect = 6
    TimeOut = 7
    DoubleKO = 8

    choices = (
        (Loss, ''),
        (Victory, 'V'),
        (CriticalArt, 'CA'),
        (Ex, 'EX'),
        (Chip, 'C'),
        (Perfect, 'P'),
        (TimeOut, 'T'),
        (DoubleKO, 'D'),
    )


class Round(models.Model):
    game = models.ForeignKey(Game)
    order = models.IntegerField()
    result = models.IntegerField(choices=MatchResult.choices)
    player1 = models.IntegerField(choices=RoundResult.choices)
    player2 = models.IntegerField(choices=RoundResult.choices)

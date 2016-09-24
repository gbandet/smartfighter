from __future__ import unicode_literals

from django.db import models
from django.db.models.signals import post_save

from smartfighter.apps.ranking.elo import Elo


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
    elo_rating = models.IntegerField(default=1000)

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

    def update_player_ratings(self):
        p1_score, p2_score = self._get_player_scores()
        p1_rating = Elo.get_new_rating(p1_score, self.player1.elo_rating, self.player2.elo_rating)
        p2_rating = Elo.get_new_rating(p2_score, self.player2.elo_rating, self.player1.elo_rating)
        self.player1.elo_rating = p1_rating
        self.player2.elo_rating = p2_rating
        self.player1.save()
        self.player2.save()

    def _get_player_scores(self):
        if self.result == MatchResult.Player1:
            return (1, 0)
        if self.result == MatchResult.Player2:
            return (0, 1)
        if self.result == MatchResult.Draw:
            return (.5, .5)
        raise RuntimeError('Invalid result value: %s' % self.result)


def game_post_save(sender, instance, created, **kwargs):
    if created:
        instance.update_player_ratings()
post_save.connect(game_post_save, Game)


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

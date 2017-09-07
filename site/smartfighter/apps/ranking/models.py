from __future__ import unicode_literals

from django.db import models
from django.db.models.signals import post_save
from django.utils import timezone

from smartfighter.apps.ranking.elo import Elo


class MatchResult(object):
    Player1 = 0
    Player2 = 1
    Draw = -1

    choices = (
        (Player1, 'Player1'),
        (Player2, 'Player2'),
        (Draw, 'Draw'),
    )


class GamePhase(object):
    Unranked = 0
    Ranked = 1
    Playoffs = 2

    choices = (
        (Unranked, 'Unranked'),
        (Ranked, 'Ranked'),
        (Playoffs, 'Playoffs'),
    )


class Player(models.Model):
    card_id = models.CharField(max_length=8, primary_key=True)
    name = models.CharField(max_length=255, db_index=True)

    def __unicode__(self):
        return self.name


class Season(models.Model):
    name = models.CharField(max_length=255)
    start_date = models.DateTimeField(null=True)
    end_date = models.DateTimeField(null=True)
    placement_games = models.IntegerField(default=15)
    playoff_data = models.TextField()

    def __unicode__(self):
        return self.name

    @classmethod
    def get_current_season(cls):
        now = timezone.now()
        return cls.objects.filter(~(models.Q(start_date__gt=now) | models.Q(end_date__lte=now))).order_by('id').first()


class PlayerResults(models.Model):
    player = models.ForeignKey(Player, related_name="season_results")
    season = models.ForeignKey(Season, related_name="player_results")
    elo_rating = models.IntegerField(default=1000, db_index=True)
    min_rating = models.IntegerField(null=True)
    max_rating = models.IntegerField(null=True)

    class Meta:
        unique_together = (('season', 'player'),)


CHARACTERS = {
    'ALX': 'Alex',
    'BLR': 'Vega',
    'BRD': 'Birdie',
    'BSN': 'Balrog',
    'CMY': 'Cammy',
    'CNL': 'Chun-Li',
    'DSM': 'Dahlsim',
    'FAN': 'F.A.N.G',
    'GUL': 'Guile',
    'IBK': 'Ibuki',
    'JRI': 'Juri',
    'KEN': 'Ken',
    'KRN': 'Karin',
    'LAR': 'Laura',
    'NCL': 'Necalli',
    'NSH': 'Nash',
    'RMK': 'R. Mika',
    'RSD': 'Rashid',
    'RYU': 'Ryu',
    'URN': 'Urien',
    'VEG': 'M. Bison',
    'ZGF': 'Zangief',
    'Z20': 'Kolin',
    'Z21': 'Akuma',
    'Z22': 'Ed',
    'Z23': 'Menat',
    'Z24': 'Abigail',
}


class Game(models.Model):
    id = models.CharField(max_length=32, primary_key=True)
    season = models.ForeignKey(Season, related_name="games", null=True)
    phase = models.IntegerField(choices=GamePhase.choices, default=GamePhase.Unranked)
    player1 = models.ForeignKey(Player, related_name="games_as_first_player")
    player2 = models.ForeignKey(Player, related_name="games_as_second_player")
    result = models.IntegerField(choices=MatchResult.choices)
    player1_character = models.CharField(max_length=3, null=True)
    player2_character = models.CharField(max_length=3, null=True)
    player1_rating_change = models.IntegerField(null=True, blank=True)
    player2_rating_change = models.IntegerField(null=True, blank=True)
    date = models.DateTimeField(db_index=True)

    @property
    def rounds(self):
        return self.round_set.order_by('order')

    def update_player_ratings(self):
        if self.season and self.phase == GamePhase.Ranked:
            p1_score, p2_score = self._get_player_scores()
            p1_results, dummy = self.season.player_results.get_or_create(player=self.player1)
            p2_results, dummy = self.season.player_results.get_or_create(player=self.player2)
            self.player1_rating_change = self._update_player_rating(p1_results, p1_score, p1_results.elo_rating, p2_results.elo_rating)
            self.player2_rating_change = self._update_player_rating(p2_results, p2_score, p2_results.elo_rating, p1_results.elo_rating)
            self.save()

    def _update_player_rating(self, result, score, old_rating, opponent_rating):
        new_rating = Elo.get_new_rating(score, old_rating, opponent_rating)
        result.elo_rating = new_rating

        if Game.objects.filter(
                season=result.season, phase=GamePhase.Ranked).filter(
                    models.Q(player1=result.player)|models.Q(player2=result.player)).count() >= result.season.placement_games:
            result.min_rating = min(result.min_rating, new_rating) if result.min_rating is not None else new_rating
            result.max_rating = max(result.max_rating, new_rating) if result.max_rating is not None else new_rating
        result.save()
        return new_rating - old_rating

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

    choices_dict = dict(choices)


class Round(models.Model):
    game = models.ForeignKey(Game)
    order = models.IntegerField()
    result = models.IntegerField(choices=MatchResult.choices)
    player1 = models.IntegerField(choices=RoundResult.choices)
    player2 = models.IntegerField(choices=RoundResult.choices)

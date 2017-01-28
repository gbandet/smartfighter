# -*- coding: utf-8 -*-
# Generated by Django 1.10.1 on 2017-01-28 17:06
from __future__ import unicode_literals

from django.db import migrations, models
import django.db.models.deletion


def create_first_season(apps, schema_editor):
    Game = apps.get_model('ranking', 'Game')
    Player = apps.get_model('ranking', 'Player')
    PlayerResults = apps.get_model('ranking', 'PlayerResults')
    Season = apps.get_model('ranking', 'Season')

    season = Season.objects.create(name='Season 1')
    for player in Player.objects.all():
        PlayerResults.objects.create(
            player=player, season=season, elo_rating=player.elo_rating)
    Game.objects.update(season=season, phase=1)


class Migration(migrations.Migration):

    dependencies = [
        ('ranking', '0001_initial'),
    ]

    operations = [
        migrations.CreateModel(
            name='PlayerResults',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('elo_rating', models.IntegerField(db_index=True, default=1000)),
                ('player', models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='season_results', to='ranking.Player')),
            ],
        ),
        migrations.CreateModel(
            name='Season',
            fields=[
                ('id', models.AutoField(auto_created=True, primary_key=True, serialize=False, verbose_name='ID')),
                ('name', models.CharField(max_length=255)),
                ('start_date', models.DateTimeField(null=True)),
                ('end_date', models.DateTimeField(null=True)),
                ('playoff_data', models.TextField()),
            ],
        ),
        migrations.AddField(
            model_name='game',
            name='phase',
            field=models.IntegerField(choices=[(0, 'Unranked'), (1, 'Ranked'), (2, 'Playoffs')], default=0),
        ),
        migrations.AlterField(
            model_name='game',
            name='result',
            field=models.IntegerField(choices=[(0, 'Player1'), (1, 'Player2'), (-1, 'Draw')]),
        ),
        migrations.AlterField(
            model_name='round',
            name='result',
            field=models.IntegerField(choices=[(0, 'Player1'), (1, 'Player2'), (-1, 'Draw')]),
        ),
        migrations.AddField(
            model_name='playerresults',
            name='season',
            field=models.ForeignKey(on_delete=django.db.models.deletion.CASCADE, related_name='player_results', to='ranking.Season'),
        ),
        migrations.AlterUniqueTogether(
            name='playerresults',
            unique_together=set([('season', 'player')]),
        ),
        migrations.AddField(
            model_name='game',
            name='season',
            field=models.ForeignKey(null=True, on_delete=django.db.models.deletion.CASCADE, related_name='games', to='ranking.Season'),
        ),
        migrations.RunPython(create_first_season),
        migrations.RemoveField(
            model_name='player',
            name='elo_rating',
        ),
    ]
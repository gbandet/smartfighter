from rest_framework import serializers
from rest_framework.relations import PrimaryKeyRelatedField

from smartfighter.apps.ranking.models import Game, Player, Round


class PlayerSerializer(serializers.ModelSerializer):
    class Meta:
        model = Player


class RoundSerializer(serializers.ModelSerializer):
    class Meta:
        model = Round
        fields = ('result', 'player1', 'player2')


class GameSerializer(serializers.ModelSerializer):
    player1 = PrimaryKeyRelatedField(queryset=Player.objects.all())
    player2 = PrimaryKeyRelatedField(queryset=Player.objects.all())
    player1_details = PlayerSerializer(source='player1', read_only=True)
    player2_details = PlayerSerializer(source='player2', read_only=True)
    rounds = RoundSerializer(many=True, read_only=True)

    class Meta:
        model = Game

    def create(self, validated_data):
        print(validated_data)
        return super(GameSerializer, self).create(validated_data)

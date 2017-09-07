import json

from rest_framework import fields, serializers

from smartfighter.apps.ranking.models import CHARACTERS, Game, Player, Round, RoundResult, Season


class RoundStatusField(fields.ReadOnlyField):
    def to_representation(self, value):
        return {
            'code': value,
            'label': RoundResult.choices_dict.get(value, '?'),
        }


class RoundSerializer(serializers.ModelSerializer):
    class Meta:
        model = Round
        fields = ('result', 'player1_status', 'player2_status')

    player1_status = RoundStatusField(source='player1')
    player2_status = RoundStatusField(source='player2')


class GameSerializer(serializers.ModelSerializer):
    class Meta:
        model = Game
        fields = ('id', 'season', 'phase', 'player1', 'player2', 'result', 'date', 'rounds')

    player1 = fields.SerializerMethodField()
    player2 = fields.SerializerMethodField()
    rounds = RoundSerializer(many=True, read_only=True)

    def get_player1(self, obj):
        return self.get_player_data(obj, 'player1')

    def get_player2(self, obj):
        return self.get_player_data(obj, 'player2')

    def get_player_data(self, obj, prefix):
        player = getattr(obj, prefix)
        character = getattr(obj, prefix + '_character')
        return {
            'id': player.card_id,
            'name': player.name,
            'rating_change': getattr(obj, prefix + '_rating_change'),
            'character_code': character,
            'character_name': CHARACTERS.get(character),
        }


class SimpleSeasonSerializer(serializers.ModelSerializer):
    class Meta:
        model = Season
        fields = ('id', 'name', 'start_date', 'end_date', 'placement_games')


class SeasonSerializer(serializers.ModelSerializer):
    class Meta:
        model = Season
        fields = ('id', 'name', 'start_date', 'end_date', 'placement_games', 'playoffs_data')

    playoffs_data = serializers.SerializerMethodField()

    def get_playoffs_data(self, season):
        if season.playoff_data:
            return json.loads(season.playoff_data)
        return None


class PlayerSeasonField(serializers.RelatedField):
    season_serializer = SimpleSeasonSerializer

    def to_representation(self, value):
        return self.season_serializer(value.season).data


class SimplePlayerSerializer(serializers.ModelSerializer):
    class Meta:
        model = Player
        fields = ('name',)


class PlayerSerializer(serializers.ModelSerializer):
    class Meta:
        model = Player
        fields = ('name', 'seasons')

    seasons = PlayerSeasonField(source='season_results', many=True, read_only=True)

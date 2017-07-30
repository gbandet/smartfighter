export class Season {
  id: number;
  name: string;
  start_date: string;
  end_date: string;
  placement_games: number;
  playoff_data: any;
}

export class RankingPlayer {
  name: string;
  rank: number;
  rating: number;
  games: number;
  wins: number;
  draws: number;
  losses: number;
}

export class SeasonRanking {
  ranking: RankingPlayer[];
  placement: RankingPlayer[];
}

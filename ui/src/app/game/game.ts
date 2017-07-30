import { Player } from '../player/player';

export class Game {
  id: string;
  season: number;
  phase: number;
  date: string;
  player1_details: Player;
  player1_character: string;
  player1_rating_change: number;
  player2_details: Player;
  player2_character: string;
  player2_rating_change: number;
  result: number;
  rounds: Round[];
}

export class Round {
  result: number;
  player1: number;
  player2: number;
}

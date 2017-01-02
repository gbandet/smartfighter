using System;
using System.Collections.Generic;

namespace SmartFighter {
    public static class GameMode {
        public const int Story = 2;
        public const int Versus = 3;
        public const int Survival = 4;
        public const int Casual = 5;
        public const int BattleLounge = 6;
        public const int Ranked = 7;
        public const int Training = 8;
        public const int CFN = 9;
        public const int BattleSettings = 10;
        public const int Shop = 11;
        public const int Options = 12;
        public const int Challenge = 13;
        public const int Log = 14;
        public const int Informations = 15;
    }

    public static class VersusMode {
        public const int PvP = 0;
        public const int PvAI = 1;
        public const int AIvP = 2;
    }

    public static class MatchResult {
        public const int Player1 = 0;
        public const int Player2 = 1;
        public const int Draw = -1;
        public const int Unknown = -100;
    }

    public static class RoundResult {
        public const int Unknown = 0;
        public const int Loss = 1;
        public const int Win = 2;
        public const int CrticalArt = 3;
        public const int EX = 4;
        public const int Chip = 5;
        public const int Perfect = 6;
        public const int TimeOut = 7;
        public const int DoubleKO = 8;
    }

    public class GameState {
        private class Game {
            public string id;
            public int roundCount;
            public int roundTimer;
            public string player1;
            public string player2;
            public int result = MatchResult.Unknown;

            public Game(string id, string player1, string player2, int rounds, int timer) {
                this.id = id;
                this.player1 = player1;
                this.player2 = player2;
                roundCount = rounds;
                roundTimer = timer;
            }
        }

        public int gameMode = 0;
        public int versusMode = 0;
        public int roundCount = 0;
        public int roundTimer = 0;
        public string player1Id;
        public string player2Id;

        private Game currentGame;
        private bool gameStarted;

        public bool isInVersus() {
            return gameMode == GameMode.Versus && versusMode == VersusMode.PvP;
        }

        public bool isGameStarted() {
            return gameStarted;
        }

        public void startGame() {
            if (isInVersus()) {
                currentGame = new Game(Guid.NewGuid().ToString("N"), player1Id, player2Id, roundCount, roundTimer);
                gameStarted = true;
            }
        }

        public void setMatchResults(int result) {
            if (isGameStarted()) {
                if (currentGame.player1 != null && currentGame.player2 != null) {
                    currentGame.result = result;
                    Logger.Instance.log("SET MATCH {0} --> {1} ({2} vs. {3})", currentGame.id, result, currentGame.player1, currentGame.player2);
                    ApiQueue.registerGame(currentGame.id, currentGame.player1, currentGame.player2, result, DateTime.UtcNow);
                } else {
                    currentGame = null;
                }
                gameStarted = false;
            }
        }

        public void setRoundResults(int[] player1, int[] player2) {
            if (player1.Length != player2.Length) {
                return;
            }
            if (isInVersus() && currentGame != null) {
                if (currentGame.result == MatchResult.Unknown) {
                    return;
                }

                List<Api.Round> rounds = new List<Api.Round>();
                Logger.Instance.log("*** Match {0} ***", currentGame.id);
                Logger.Instance.log("- Winner: {0}", getWinnerString(currentGame.result));
                Logger.Instance.log("- Rounds:");
                int score1 = 0;
                int score2 = 0;
                int scoreMax = roundCount / 2 + 1;
                for (int index = 0; index < player1.Length; index++) {
                    int status1 = player1[index];
                    int status2 = player2[index];
                    if (status1 != RoundResult.Unknown && status1 != RoundResult.Loss) {
                        score1++;
                    }
                    if (status2 != RoundResult.Unknown && status2 != RoundResult.Loss) {
                        score2++;
                    }
                    Logger.Instance.log(" * {0} | {1}", getRoundCode(status1), getRoundCode(status2));
                    rounds.Add(new Api.Round(getRoundResult(status1, status2), status1, status2));
                    if (score1 >= scoreMax || score2 >= scoreMax) {
                        break;
                    }
                }

                ApiQueue.registerRounds(currentGame.id, rounds.ToArray());
                currentGame = null;
            }
        }

        private int getRoundResult(int player1, int player2) {
            if (player1 == RoundResult.DoubleKO || (player1 == RoundResult.TimeOut && player2 == RoundResult.TimeOut)) {
                return MatchResult.Draw;
            }
            if (player1 == RoundResult.Loss) {
                return MatchResult.Player2;
            }
            return MatchResult.Player1;
        }

        private string getWinnerString(int result) {
            if (result == MatchResult.Player1) {
                return "Player 1";
            }
            if (result == MatchResult.Player2) {
                return "Player 2";
            }
            if (result == MatchResult.Draw) {
                return "Draw";
            }
            return "Unknonw";
        }

        public string getRoundCode(int result) {
            if (result == RoundResult.Loss) {
                return "  ";
            }
            if (result == RoundResult.Win) {
                return " V";
            }
            if (result == RoundResult.CrticalArt) {
                return "CA";
            }
            if (result == RoundResult.EX) {
                return "EX";
            }
            if (result == RoundResult.Chip) {
                return " C";
            }
            if (result == RoundResult.Perfect) {
                return " P";
            }
            if (result == RoundResult.TimeOut) {
                return " T";
            }
            if (result == RoundResult.DoubleKO) {
                return " D";
            }
            return " ?";
        }
    }
}
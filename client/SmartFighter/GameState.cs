using System;


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
        public const int BattleSettings = 14;
        public const int Shop = 11;
        public const int Options = 12;
        public const int Challenge = 13;
        public const int Log = 14;
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
        public int gameMode = 0;
        public int versusMode = 0;
        public int roundCount = 0;
        public int roundTimer = 0;
        public string player1Id;
        public string player2Id;

        private int currentResult = MatchResult.Unknown;
        private bool gameStarted;
        private string gameID;

        public bool isInVersus() {
            return gameMode == GameMode.Versus && versusMode == VersusMode.PvP;
        }

        public bool isGameStarted() {
            return gameStarted;
        }

        public void startGame() {
            if (isInVersus()) {
                currentResult = MatchResult.Unknown;
                gameID = Guid.NewGuid().ToString("N");
                gameStarted = true;
            }
        }

        public void setMatchResults(int result) {
            if (isGameStarted()) {
                if (player1Id != null && player2Id != null) {
                    Logger.Instance.log("SET MATCH {0} --> {1} ({2} vs. {3})", gameID, result, player1Id, player2Id);
                    ApiQueue.registerGame(gameID, player1Id, player2Id, result, DateTime.UtcNow);
                    currentResult = result;
                } else {
                    currentResult = MatchResult.Unknown;
                    gameID = null;
                }
                gameStarted = false;
            }
        }

        public void setRoundResults(int[] player1, int[] player2) {
            if (player1.Length != player2.Length) {
                return;
            }
            if (isInVersus() && gameID != null) {
                if (currentResult == MatchResult.Unknown) {
                    return;
                }

                Api.Round[] rounds = new Api.Round[player1.Length];

                Logger.Instance.log("*** Match {0} ***", gameID);
                Logger.Instance.log("- Winner: {0}", getWinnerString());
                Logger.Instance.log("- Rounds:");
                for (int index = 0; index < player1.Length; index++) {
                    int status1 = player1[index];
                    int status2 = player2[index];
                    Logger.Instance.log(" * {0} | {1}", getRoundCode(status1), getRoundCode(status2));

                    rounds[index] = new Api.Round(getRoundResult(status1, status2), status1, status2);
                }

                ApiQueue.registerRounds(gameID, rounds);
                gameID = null;
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

        private string getWinnerString() {
            if (currentResult == MatchResult.Player1) {
                return "Player 1";
            }
            if (currentResult == MatchResult.Player2) {
                return "Player 2";
            }
            if (currentResult == MatchResult.Draw) {
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
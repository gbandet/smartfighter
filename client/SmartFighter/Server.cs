using SmartFighter.Common;


namespace SmartFighter {
    class SmartFighterServer : SmartFighterInterface {
        private GameState game;

        public SmartFighterServer() {
            game = new GameState();
        }

        public override void writeLog(string message) {
            Logger.Instance.log("> " + message);
        }

        public override void writeLog(string message, params object[] args) {
            Logger.Instance.log("> " + message, args);
        }

        public override void setGameMode(int mode) {
            game.gameMode = mode;
        }

        public override void setVersusMode(int mode) {
            game.versusMode = mode;
        }

        public override void setRoundCount(int rounds) {
            game.roundCount = rounds;
        }

        public override void setRoundTimer(int timer) {
            game.roundTimer = timer;
        }

        public override void setMatchResults(int result) {
            game.setMatchResults(result);
        }

        public override void setRoundResults(int[] player1, int[] player2) {
            game.setRoundResults(player1, player2);
        }

        public override void setGameStart() {
            game.startGame();
        }
    }
}

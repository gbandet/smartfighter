using SmartFighter.Common;


namespace SmartFighter {
    public delegate void GameModeChangedHandler();

    class SmartFighterServer : SmartFighterInterface {
        private GameState game;

        public event GameModeChangedHandler GameModeChangedEvent;

        public SmartFighterServer(GameState game) {
            this.game = game;
        }

        public override void writeLog(string message) {
            Logger.Instance.log("> " + message);
        }

        public override void writeLog(string message, params object[] args) {
            Logger.Instance.log("> " + message, args);
        }

        public override void setGameMode(int mode) {
            game.gameMode = mode;
            game.versusMode = -1;
            GameModeChangedEvent();
        }

        public override void setVersusMode(int mode) {
            game.versusMode = mode;
            GameModeChangedEvent();
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

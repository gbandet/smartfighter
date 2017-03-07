using SmartFighter.Common;
using System;


namespace SmartFighter {
    class SmartFighterServer : SmartFighterInterface {

        public SmartFighterServer() { }

        public override void writeLog(string message) {
            Console.WriteLine("> " + message);
        }

        public override void writeLog(string message, params object[] args) {
            Console.WriteLine("> " + message, args);
        }

        public override void setGameMode(int mode) {
            writeLog("setGameMode -> {0}", mode);
        }

        public override void setVersusMode(int mode) {
            writeLog("setVersusMode -> {0}", mode);
        }

        public override void setRoundCount(int rounds) {
            writeLog("setRoundCount -> {0}", rounds);
        }

        public override void setRoundTimer(int timer) {
            writeLog("setRoundTimer -> {0}", timer);
        }

        public override void setMatchResults(int result) {
            writeLog("setMatchResults -> {0}", result);
        }

        public override void setRoundResults(int[] player1, int[] player2) {
            writeLog("setRoundResults");
        }

        public override void setGameStart() {
            writeLog("setGameStart");
        }

        public override void setCharacters(string player1, string player2) {
            writeLog("setCharacters -> {0} {1}", player1, player2);
        }
    }
}
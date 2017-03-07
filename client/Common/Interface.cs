using System;


namespace SmartFighter.Common {
    public abstract class SmartFighterInterface : MarshalByRefObject {
        public bool Active = true;

        public abstract void writeLog(string message);

        public abstract void writeLog(string message, params object[] args);

        public abstract void setGameMode(int mode);

        public abstract void setVersusMode(int mode);

        public abstract void setRoundCount(int rounds);

        public abstract void setRoundTimer(int timer);

        public abstract void setMatchResults(int result);

        public abstract void setRoundResults(int[] player1, int[] player2);

        public abstract void setGameStart();

        public abstract void setCharacters(string player1, string player2);
    }
}
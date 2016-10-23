using System;


namespace SmartFighter {
    class Logger {
        private static Logger instance;

        private Logger() { }

        public static Logger Instance {
            get {
                if (instance == null) {
                    instance = new Logger();
                }
                return instance;
            }
        }

        public delegate void LogHandler(string message);
        public event LogHandler LogEvent;

        public void log(string message) {
            LogEvent(message + "\n");
        }

        public void log(string message, params object[] args) {
            log(String.Format(message, args));
        }
    }
}
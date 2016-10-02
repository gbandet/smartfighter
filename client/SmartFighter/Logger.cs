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

        private App app;

        public void setApp(App app) {
            this.app = app;
        }

        public void log(string message) {
            app.appendToLogs(message + "\n");
        }

        public void log(string message, params object[] args) {
            log(String.Format(message, args));
        }
    }
}

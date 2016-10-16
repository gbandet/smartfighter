using Newtonsoft.Json;
using System.IO;


namespace SmartFighter {
    public class JoystickButton {
        public string joystickGuid;
        public int buttonNumber;
    }

    public class Config {
        private static Config instance;

        private Config() { }

        public static Config Instance {
            get {
                if (instance == null) {
                    instance = new Config();
                }
                return instance;
            }
        }

        public string apiUrl = "";
        public string nfcReaderName = "";
        public string player1Joystick = "";
        public int player1Button;
        public string player2Joystick = "";
        public int player2Button;

        public void initDialog(ConfigDialog dlg) {
            dlg.apiText.Text = apiUrl;
            dlg.nfcCombo.Text = nfcReaderName;
            dlg.player1JoystickButton.joystickGuid = player1Joystick;
            dlg.player2JoystickButton.joystickGuid = player2Joystick;
            dlg.player1JoystickButton.buttonNumber = player1Button;
            dlg.player2JoystickButton.buttonNumber = player2Button;
            dlg.refresh();
        }

        public void updateFromDialog(ConfigDialog dlg) {
            apiUrl = dlg.apiText.Text;
            nfcReaderName = dlg.nfcCombo.Text;
            player1Joystick = dlg.player1JoystickButton.joystickGuid;
            player2Joystick = dlg.player2JoystickButton.joystickGuid;
            player1Button = dlg.player1JoystickButton.buttonNumber;
            player2Button = dlg.player2JoystickButton.buttonNumber;
            save();
        }

        public static void load() {
            string json = File.ReadAllText(getFileName());
            try {
                Config.instance = JsonConvert.DeserializeObject<Config>(json);
            } catch (JsonReaderException exc) {
                Logger.Instance.log("Config loading error: {0}", exc.Message);
            }
        }

        public static void save() {
            string json = JsonConvert.SerializeObject(Config.instance, Formatting.Indented);
            File.WriteAllText(getFileName(), json);
        }

        private static string getFileName() {
            return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "SmartFighter.json");
        }
    }
}
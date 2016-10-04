using Newtonsoft.Json;
using System.IO;


namespace SmartFighter {
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

        public void initDialog(ConfigDialog dlg) {
            dlg.apiText.Text = apiUrl;
            dlg.nfcCombo.Text = nfcReaderName;
        }

        public void updateFromDialog(ConfigDialog dlg) {
            apiUrl = dlg.apiText.Text;
            nfcReaderName = dlg.nfcCombo.Text;
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
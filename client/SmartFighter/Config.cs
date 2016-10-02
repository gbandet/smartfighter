using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;


namespace SmartFighter {
    public static class Config {
        public static string apiUrl;
        public static string nfcReaderName;

        public static void load() {
            var data = new Dictionary<string, string>();
            var lines = readConfigLines();
            foreach (var line in lines) {
                var tuple = parseLine(line);
                if (tuple.Item1 != null) {
                    data.Add(tuple.Item1, tuple.Item2);
                }
            }
            if (data.ContainsKey("apiurl")) {
                apiUrl = data["apiurl"];
            }
            if (data.ContainsKey("nfcreader")) {
                nfcReaderName = data["nfcreader"];
            }
        }

        public static void save() {
            var lines = readConfigLines();
            var configLines = new List<string>();
            foreach (var line in lines) {
                var newLine = line;
                var tuple = parseLine(line);
                if (tuple.Item1 == "apiurl") {
                    newLine = string.Format("apiurl={0}", apiUrl);
                } else if (tuple.Item1 == "nfcreader") {
                    newLine = string.Format("nfcreader={0}", nfcReaderName);
                }
                configLines.Add(newLine);
            }
            writeConfigLines(configLines);
        }

        private static string[] readConfigLines() {
            var filename = getFileName();
            string[] rows;
            try {
                rows = File.ReadAllLines(filename);
            } catch (FileNotFoundException) {
                return new string[] { };
            } catch (Exception) {
                MessageBox.Show(String.Format("Cannot read config file at \"{0}\"", filename));
                return new string[] { };
            }
            return rows;
        }

        private static void writeConfigLines(IEnumerable<string> lines) {
            var filename = getFileName();
            try {
                File.WriteAllLines(filename, lines);
            } catch (Exception) {
                MessageBox.Show(String.Format("Cannot write config file at \"{0}\"", filename));
            }
        }

        private static string getFileName() {
            return Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "SmartFighter.cfg");
        }

        private static Tuple<string, string> parseLine(string line) {
            var index = line.IndexOf('=');
            if (index < 0) {
                return new Tuple<string, string>(null, null);
            }
            return new Tuple<string, string>(line.Substring(0, index).ToLower(), line.Substring(index + 1));
        }

        public static void initDialog(ConfigDialog dlg) {
            dlg.apiText.Text = apiUrl;
            dlg.nfcCombo.Text = nfcReaderName;
        }

        public static void updateFromDialog(ConfigDialog dlg) {
            apiUrl = dlg.apiText.Text;
            nfcReaderName = dlg.nfcCombo.Text;
            save();
        }
    }
}
using System;
using System.IO;
using System.Windows.Forms;


namespace SmartFighter {
    static class Program {
        [STAThread]
        static void Main() {
            try {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new App());
            } catch (Exception exc) {
                File.AppendAllText(
                    Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "SmartFighter.log"),
                    exc.ToString() + "\n");
                MessageBox.Show(exc.ToString());
            }
        }
    }
}

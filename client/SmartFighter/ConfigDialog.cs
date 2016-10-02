using System.Windows.Forms;


namespace SmartFighter {
    public partial class ConfigDialog : Form {
        public ConfigDialog() {
            InitializeComponent();
            nfcCombo.Items.AddRange(Nfc.GetReaderNames());
        }
    }
}

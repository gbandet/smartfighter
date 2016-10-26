using System;
using System.Windows.Forms;

namespace SmartFighter {
    public partial class RegisterDialog : Form {
        public string cardId;

        public RegisterDialog(Nfc nfc) {
            InitializeComponent();
            cardValue.Text = "Scan a card...";
            nfc.NfcCardEvent += onCardRead;
            validate();
        }

        public bool validate() {
            var valid = cardId != null && cardId != "" && nameValue.Text != "";
            okButton.Enabled = valid;
            return valid;
        }

        private void onCardRead(string uid) {
            if (InvokeRequired) {
                Invoke(new Action<string>(onCardRead), uid);
                return;
            }
            cardId = uid;
            cardValue.Text = uid;
            validate();
        }

        private void nameValue_TextChanged(object sender, EventArgs e) {
            validate();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmartFighter {
    public partial class Overlay : Form {
        public Overlay() {
            InitializeComponent();
        }

        private const string MSG_SCAN = "Scan your card";
        private const string MSG_DISCONNECT = "Confirm disconnection or scan another card.";

        public void setScanPlayer1() {
            player1Info.Text = MSG_SCAN;
            player1Name.Hide();
            player1Info.Show();
            player2Name.Show();
            player2Info.Hide();
        }

        public void setScanPlayer2() {
            player2Info.Text = MSG_SCAN;
            player1Name.Show();
            player1Info.Hide();
            player2Name.Hide();
            player2Info.Show();
        }

        public void setDisconnectPlayer1() {
            player1Info.Text = MSG_DISCONNECT;
            player1Name.Hide();
            player1Info.Show();
            player2Name.Show();
            player2Info.Hide();
        }

        public void setDisconnectPlayer2() {
            player2Info.Text = MSG_DISCONNECT;
            player1Name.Show();
            player1Info.Hide();
            player2Name.Hide();
            player2Info.Show();
        }

        public void hideInfoLabels() {
            player1Info.Hide();
            player2Info.Hide();
            player1Name.Show();
            player2Name.Show();
        }
    }
}

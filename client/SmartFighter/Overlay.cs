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

        public void setPlayerSelection(int player, bool hasCard) {
            if (!hasCard) {
                infoLabel.Text = String.Format("Player {0}: Scan your card.", player);
            } else {
                infoLabel.Text = String.Format("Player {0}: To disconnect push the button again or scan another card.", player);
            }
        }

        public void setNoSelection() {
            infoLabel.Text = "Push a button to select a player.";
        }

        public void setReady() {
            infoLabel.Text = "Match ready.";
        }
    }
}

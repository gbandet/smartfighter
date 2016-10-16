using SharpDX.DirectInput;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;


namespace SmartFighter {
    public partial class ConfigDialog : Form {
        public JoystickButton player1JoystickButton = new JoystickButton();
        public JoystickButton player2JoystickButton = new JoystickButton();

        private BackgroundWorker inputWorker;

        public ConfigDialog() {
            InitializeComponent();
            nfcCombo.Items.AddRange(Nfc.GetReaderNames());
        }

        public void refresh() {
            if (player1JoystickButton.joystickGuid != null && player1JoystickButton.joystickGuid != "") {
                updateJoystickLabel(player1ButtonLabel, player1JoystickButton.joystickGuid, player1JoystickButton.buttonNumber);
            } else {
                player1ButtonLabel.Text = "";
            }
            if (player2JoystickButton.joystickGuid != null && player2JoystickButton.joystickGuid != "") {
                updateJoystickLabel(player2ButtonLabel, player2JoystickButton.joystickGuid, player2JoystickButton.buttonNumber);
            } else {
                player2ButtonLabel.Text = "";
            }
        }

        private void updateJoystickLabel(Label label, string guid, int button) {
            label.Text = String.Format("{0}@{1}", guid, button);
        }

        private void Player1Button_Click(object sender, EventArgs e) {
            selectInput(player1JoystickButton, player1ButtonLabel);
        }

        private void player2Button_Click(object sender, EventArgs e) {
            selectInput(player2JoystickButton, player2ButtonLabel);
        }

        private void selectInput(JoystickButton button, Label label) {
            inputWorker = new BackgroundWorker();
            inputWorker.WorkerSupportsCancellation = true;
            inputWorker.DoWork += waitInput;
            inputWorker.RunWorkerCompleted += (sender, args) => refresh();
            label.Text = "Push a joystick button...";
            inputWorker.RunWorkerAsync(Tuple.Create(button, label));
        }

        private void waitInput(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;
            var argument = (Tuple<JoystickButton, Label>)args.Argument;
            var joystickButton = argument.Item1;
            var label = argument.Item2;
            args.Result = args.Argument;

            var devices = new List<Joystick>();
            var directInput = new DirectInput();
            foreach (var instance in directInput.GetDevices(DeviceType.Gamepad, DeviceEnumerationFlags.AllDevices))
                devices.Add(new Joystick(directInput, instance.InstanceGuid));
            foreach (var instance in directInput.GetDevices(DeviceType.Joystick, DeviceEnumerationFlags.AllDevices))
                devices.Add(new Joystick(directInput, instance.InstanceGuid));

            if (devices.Count == 0) {
                MessageBox.Show("No joysticks or gamepads found.");
                return;
            }
            while (!worker.CancellationPending) {
                foreach (var device in devices) {
                    var state = new JoystickState();
                    device.Acquire();
                    device.GetCurrentState(ref state);
                    for (var i = 0; i < state.Buttons.Length; i++) {
                        if (state.Buttons[i]) {
                            joystickButton.joystickGuid = device.Information.InstanceGuid.ToString("N");
                            joystickButton.buttonNumber = i;
                            return;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
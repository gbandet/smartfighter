using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace SmartFighter {
    public partial class App : Form {
        private Connector connector;
        private BackgroundWorker apiWorker;
        private BackgroundWorker connectorWorker;
        private Nfc nfcReader;
        private BackgroundWorker nfcWorker;
        private Input inputReader;
        private BackgroundWorker inputWorker;

        private enum WorkerState {
            Stopped,
            Running,
            Stopping,
        };

        public App() {
            InitializeComponent();

            Logger logger = Logger.Instance;
            logger.setApp(this);

            Config.load();

            connector = new Connector();
            connectorWorker = new BackgroundWorker();
            connectorWorker.WorkerSupportsCancellation = true;
            connectorWorker.DoWork += connector.run;
            connectorWorker.RunWorkerCompleted += (sender, args) => onConnectorExit((bool)args.Result);
            updateWorkerState(WorkerState.Running, connectorButton, connectorLabel);
            connectorWorker.RunWorkerAsync();

            apiWorker = new BackgroundWorker();
            apiWorker.WorkerSupportsCancellation = true;
            apiWorker.DoWork += ApiQueue.run;
            apiWorker.RunWorkerCompleted += (sender, args) => onApiQueueExit();
            updateWorkerState(WorkerState.Running, apiButton, apiLabel);
            apiWorker.RunWorkerAsync();

            nfcReader = new Nfc();
            nfcReader.NfcCardEvent += onCardRead;
            nfcWorker = new BackgroundWorker();
            nfcWorker.WorkerSupportsCancellation = true;
            nfcWorker.DoWork += nfcReader.run;
            nfcWorker.RunWorkerCompleted += (sender, args) => onNfcExit();
            updateWorkerState(WorkerState.Running, nfcButton, nfcLabel);
            nfcWorker.RunWorkerAsync();

            inputReader = new Input();
            inputReader.Player1ButtonEvent += onPlayer1Button;
            inputReader.Player2ButtonEvent += onPlayer2Button;
            inputWorker = new BackgroundWorker();
            inputWorker.WorkerSupportsCancellation = true;
            inputWorker.DoWork += inputReader.run;
            inputWorker.RunWorkerCompleted += (sender, args) => onInputExit();
            updateWorkerState(WorkerState.Running, inputButton, inputLabel);
            inputWorker.RunWorkerAsync();
        }

        private void onPlayer1Button() {

        }

        private void onPlayer2Button() {

        }

        private void onCardRead(string uid) {

        }

        private void onConnectorExit(bool isSuccess) {
            updateWorkerState(WorkerState.Stopped, connectorButton, connectorLabel);
            if (!isSuccess) {
                appendToLogs("Connector ended with errors.");
            }
        }

        private void onApiQueueExit() {
            updateWorkerState(WorkerState.Stopped, apiButton, apiLabel);
        }

        private void onNfcExit() {
            updateWorkerState(WorkerState.Stopped, nfcButton, nfcLabel);
        }

        private void onInputExit() {
            updateWorkerState(WorkerState.Stopped, inputButton, inputLabel);
        }

        public void appendToLogs(string message) {
            if (InvokeRequired) {
                Invoke(new Action<string>(appendToLogs), new object[] { message });
                return;
            }
            logBox.AppendText(message);
        }

        private void connectorButton_Click(object sender, EventArgs e) {
            toggleWorkerState(connectorWorker, connectorButton, connectorLabel);
        }

        private void apiButton_Click(object sender, EventArgs e) {
            toggleWorkerState(apiWorker, apiButton, apiLabel);
        }

        private void nfcButton_Click(object sender, EventArgs e) {
            toggleWorkerState(nfcWorker, nfcButton, nfcLabel);
        }

        private void inputButton_Click(object sender, EventArgs e) {
            toggleWorkerState(inputWorker, inputButton, inputLabel);
        }

        private void toggleWorkerState(BackgroundWorker worker, Button button, Label label) {
            if (!worker.CancellationPending) {
                if (worker.IsBusy) {
                    updateWorkerState(WorkerState.Stopping, button, label);
                    worker.CancelAsync();
                } else {
                    updateWorkerState(WorkerState.Running, button, label);
                    worker.RunWorkerAsync();
                }
            }
        }

        private void updateWorkerState(WorkerState state, Button button, Label label) {
            switch (state) {
                case WorkerState.Running:
                    button.Text = "Stop";
                    button.Enabled = true;
                    label.Text = "Running";
                    break;
                case WorkerState.Stopping:
                    button.Text = "Stop";
                    button.Enabled = false;
                    label.Text = "Stopping...";
                    break;
                case WorkerState.Stopped:
                    button.Text = "Start";
                    button.Enabled = true;
                    label.Text = "Stopped";
                    break;
            }
        }

        private void configurationMenuItem_Click(object sender, EventArgs e) {
            ConfigDialog dlg = new ConfigDialog();
            dlg.Owner = this;
            Config.Instance.initDialog(dlg);
            if (dlg.ShowDialog() == DialogResult.OK) {
                Config.Instance.updateFromDialog(dlg);
            }
        }
    }
}

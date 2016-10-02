using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace SmartFighter {
    public partial class App : Form {
        private Connector connector;
        private BackgroundWorker apiWorker;
        private BackgroundWorker hookWorker;

        public App() {
            InitializeComponent();
            Logger logger = Logger.Instance;
            logger.setApp(this);

            Config.load();

            connector = new Connector();
            hookWorker = new BackgroundWorker();
            hookWorker.WorkerSupportsCancellation = true;
            hookWorker.DoWork += connector.run;
            hookWorker.RunWorkerCompleted += (sender, args) => onConnectorExit((bool)args.Result);
            connectorLabel.Text = "Running";
            hookWorker.RunWorkerAsync();

            apiWorker = new BackgroundWorker();
            apiWorker.WorkerSupportsCancellation = true;
            apiWorker.DoWork += ApiQueue.run;
            apiWorker.RunWorkerCompleted += (sender, args) => onApiQueueExit();
            apiLabel.Text = "Running";
            apiWorker.RunWorkerAsync();
        }

        private void onConnectorExit(bool isSuccess) {
            connectorLabel.Text = "Stopped";
            if (!isSuccess) {
                appendToLogs("Connector ended with errors.");
            }
        }

        private void onApiQueueExit() {
            apiLabel.Text = "Stopped";
        }

        public void appendToLogs(string message) {
            if (InvokeRequired) {
                Invoke(new Action<string>(appendToLogs), new object[] { message });
                return;
            }
            logBox.AppendText(message);
        }

        private void connectorButton_Click(object sender, EventArgs e) {
            if (hookWorker.CancellationPending) {
                connectorLabel.Text = "Stopping...";
            } else if (hookWorker.IsBusy) {
                connectorLabel.Text = "Stopping...";
                hookWorker.CancelAsync();
            } else {
                connectorLabel.Text = "Running";
                hookWorker.RunWorkerAsync();
            }
        }

        private void apiButton_Click(object sender, EventArgs e) {
            if (apiWorker.CancellationPending) {
                apiLabel.Text = "Stopping...";
            } else if (apiWorker.IsBusy) {
                apiLabel.Text = "Stopping...";
                apiWorker.CancelAsync();
            } else {
                apiLabel.Text = "Running";
                apiWorker.RunWorkerAsync();
            }
        }

        private void configurationMenuItem_Click(object sender, EventArgs e) {
            ConfigDialog dlg = new ConfigDialog();
            dlg.Owner = this;
            Config.initDialog(dlg);
            if (dlg.ShowDialog() == DialogResult.OK) {
                Config.updateFromDialog(dlg);
            }
        }
    }
}

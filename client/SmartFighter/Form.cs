using System;
using System.ComponentModel;
using System.Windows.Forms;


namespace SmartFighter {
    public partial class App : Form {
        private GameState gameState;
        private Connector connector;
        private BackgroundWorker apiWorker;
        private BackgroundWorker connectorWorker;
        private Nfc nfcReader;
        private BackgroundWorker nfcWorker;
        private Input inputReader;
        private BackgroundWorker inputWorker;
        private Overlay overlay;
        private Timer overlayTimer;
        private bool overlayEnabled = true;
        private int? playerSelection = null;
        private bool sfvHasFocus = true;

        private enum WorkerState {
            Stopped,
            Running,
            Stopping,
        };

        public App() {
            InitializeComponent();

            Logger logger = Logger.Instance;
            logger.LogEvent += appendToLogs;

            Config.load();
            overlay = new Overlay();
            overlay.Owner = this;
            overlayTimer = new Timer();
            overlayTimer.Interval = 5000;
            overlayTimer.Tick += (sender, args) => stopOverlayDisconnect();

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

            gameState = new GameState();
            gameState.ScoresUpdatedEvent += updateOverlayScores;

            connector = new Connector(gameState);
            connector.server.GameModeChangedEvent += onGameModeChanged;
            connector.SFVFocusChangedEvent += onFocusChanged;
            connectorWorker = new BackgroundWorker();
            connectorWorker.WorkerSupportsCancellation = true;
            connectorWorker.DoWork += connector.run;
            connectorWorker.RunWorkerCompleted += (sender, args) => onConnectorExit((bool)args.Result);
            updateWorkerState(WorkerState.Running, connectorButton, connectorLabel);
            connectorWorker.RunWorkerAsync();
        }

        private void onPlayer1Button() {
            if (InvokeRequired) {
                Invoke(new Action(onPlayer1Button));
                return;
            }
            if (!overlayEnabled) {
                return;
            }
            if (playerSelection == 1) {
                gameState.player1Id = null;
                overlay.player1Name.Text = "";
                overlay.hideScores();
            }
            playerSelection = 1;
            if (gameState.player1Id != null) {
                overlay.setDisconnectPlayer1();
                overlayTimer.Start();
            } else {
                overlayTimer.Stop();
                overlay.setScanPlayer1();
            }
        }

        private void onPlayer2Button() {
            if (InvokeRequired) {
                Invoke(new Action(onPlayer2Button));
                return;
            }
            if (!overlayEnabled) {
                return;
            }
            if (playerSelection == 2) {
                gameState.player2Id = null;
                overlay.player2Name.Text = "";
                overlay.hideScores();
            }
            playerSelection = 2;
            if (gameState.player2Id != null) {
                overlay.setDisconnectPlayer2();
                overlayTimer.Start();
            } else {
                overlayTimer.Stop();
                overlay.setScanPlayer2();
            }
        }

        private void onCardRead(string uid) {
            if (InvokeRequired) {
                Invoke(new Action<string>(onCardRead), uid);
                return;
            }
            if (!overlayEnabled) {
                return;
            }
            Api.Player player = Api.getPlayer(uid);
            if (player == null) {
                return;
            }
            if (playerSelection == 1 && gameState.player2Id != uid) {
                gameState.player1Id = uid;
                overlay.player1Name.Text = player.name;
                if (gameState.player2Id == null) {
                    playerSelection = 2;
                    overlay.setScanPlayer2();
                } else {
                    stopOverlayDisconnect();
                    gameState.resetScores();
                    updateOverlayScores();
                    overlay.showScores();
                }
            } else if (playerSelection == 2 && gameState.player1Id != uid) {
                gameState.player2Id = uid;
                overlay.player2Name.Text = player.name;
                if (gameState.player1Id == null) {
                    playerSelection = 1;
                    overlay.setScanPlayer1();
                } else {
                    stopOverlayDisconnect();
                    gameState.resetScores();
                    updateOverlayScores();
                    overlay.showScores();
                }
            }
        }

        private void onGameModeChanged() {
            if (InvokeRequired) {
                Invoke(new Action(onGameModeChanged));
                return;
            }
            if (!gameState.isInVersus()) {
                gameState.player1Id = null;
                gameState.player2Id = null;
                overlay.player1Name.Text = "";
                overlay.player2Name.Text = "";
                playerSelection = null;
                overlay.hideScores();
            }
            updateOverlayVisibility();
        }

        private void onFocusChanged(bool hasFocus) {
            if (InvokeRequired) {
                Invoke(new Action<bool>(onFocusChanged), hasFocus);
                return;
            }
            sfvHasFocus = hasFocus;
            updateOverlayVisibility();
        }

        private void updateOverlayVisibility() {
            if (sfvHasFocus && gameState.isInVersus()) {
                overlay.Show();
            } else {
                overlay.Hide();
            }
        }

        private void stopOverlayDisconnect() {
            overlayTimer.Stop();
            playerSelection = null;
            overlay.hideInfoLabels();
        }

        private void updateOverlayScores() {
            if (InvokeRequired) {
                Invoke(new Action(updateOverlayScores));
                return;
            }
            overlay.player1Score.Text = gameState.player1Score.ToString();
            overlay.player2Score.Text = gameState.player2Score.ToString();
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

        private void registerAPlayerToolStripMenuItem_Click(object sender, EventArgs e) {
            RegisterDialog dlg = new RegisterDialog(nfcReader);
            dlg.Owner = this;
            overlayEnabled = false;
            if (dlg.ShowDialog() == DialogResult.OK) {
                if (Api.createPlayer(dlg.cardId, dlg.nameValue.Text)) {
                    Logger.Instance.log("Player {0} registered.", dlg.nameValue.Text);
                }
            }
            overlayEnabled = true;
        }
    }
}

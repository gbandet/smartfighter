using SharpDX.DirectInput;
using System;
using System.ComponentModel;
using System.Threading;


namespace SmartFighter {
    public delegate void Player1ButtonHandler();
    public delegate void Player2ButtonHandler();

    class Input {
        public event Player1ButtonHandler Player1ButtonEvent;
        public event Player2ButtonHandler Player2ButtonEvent;

        public void run(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;

            if (Config.Instance.player1Joystick == null || Config.Instance.player1Joystick == "" ||
                Config.Instance.player2Joystick == null || Config.Instance.player2Joystick == "") {
                Logger.Instance.log("Joystick buttons are not configured.");
                return;
            }

            var directInput = new DirectInput();
            var joysticks = new Joystick[2] {
                new Joystick(directInput, new Guid(Config.Instance.player1Joystick)),
                new Joystick(directInput, new Guid(Config.Instance.player2Joystick)),
            };
            var buttons = new int[2] { Config.Instance.player1Button, Config.Instance.player2Button };
            var isPressed = new bool[2] { true, true };

            foreach (var joystick in joysticks) {
                joystick.Acquire();
            }
            
            while (!worker.CancellationPending) {
                for (int i = 0; i < 2; i++) {
                    var joystickState = new JoystickState();
                    joysticks[i].GetCurrentState(ref joystickState);
                    if (joystickState.Buttons[buttons[i]]) {
                        if (!isPressed[i]) {
                            Logger.Instance.log("Joystick {0} has been pushed.", i);
                            if (i == 0) {
                                Player1ButtonEvent();
                            } else {
                                Player2ButtonEvent();
                            }
                            isPressed[i] = true;
                        }
                    } else {
                        isPressed[i] = false;
                    }
                }
                Thread.Sleep(100);
            }
            foreach (var joystick in joysticks) {
                joystick.Unacquire();
            }
        }
    }
}

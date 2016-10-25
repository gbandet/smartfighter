using System;
using System.Collections;
using System.ComponentModel;
using System.Threading;


namespace SmartFighter {
    class ApiQueue {
        private enum EntryType {
            CreateGame,
            UpdateRounds,
        };

        private static Queue queue =  Queue.Synchronized(new Queue());

        public static void registerGame(string id, string player1Id, string player2Id, int result, DateTime date) {
            queue.Enqueue(new object[] { EntryType.CreateGame, id, player1Id, player2Id, result, date });
        }

        public static void registerRounds(string id, Api.Round[] rounds) {
            queue.Enqueue(new object[] { EntryType.UpdateRounds, id, rounds });
        }

        public static void run(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;
            while (!worker.CancellationPending) {
                while (queue.Count > 0 && !worker.CancellationPending) {
                    object[] values = (object[])queue.Peek();
                    bool success = true;
                    switch ((EntryType)values[0]) {
                        case EntryType.CreateGame:
                            success = Api.createGame((string)values[1], (string)values[2], (string)values[3], (int)values[4], (DateTime)values[5]);
                            break;
                        case EntryType.UpdateRounds:
                            success = Api.updateRounds((string)values[1], (Api.Round[]) values[2]);
                            break;
                    }
                    if (success) {
                        queue.Dequeue();
                    } else {
                        break;
                    }
                }
                for (int i = 0; i < 5; i++) {
                    if (worker.CancellationPending) {
                        return;
                    }
                    Thread.Sleep(1000);
                }
            }
        }
    }
}

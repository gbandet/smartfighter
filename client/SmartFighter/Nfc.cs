using PCSC;
using PCSC.Iso7816;
using System;
using System.ComponentModel;
using System.Threading;


namespace SmartFighter {
    public delegate void NfcCardHandler(string uid);

    public class Nfc {
        public event NfcCardHandler NfcCardEvent;

        private static readonly IContextFactory contextFactory = ContextFactory.Instance;

        public static string[] GetReaderNames() {
            using (var context = contextFactory.Establish(SCardScope.System)) {
                return context.GetReaders();
            }
        }

        public void run(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;
            if (Config.Instance.nfcReaderName == "") {
                return;
            }

            while (!worker.CancellationPending) {
                using (var monitor = new SCardMonitor(contextFactory, SCardScope.System)) {
                    monitor.CardInserted += cardInserted;
                    monitor.Start(Config.Instance.nfcReaderName);
                    while (!worker.CancellationPending) {
                        Thread.Sleep(500);
                    }
                }
            }
        }

        private void cardInserted(object sender, CardStatusEventArgs args) {
            var uid = readUID(args.ReaderName);
            if (uid == null) {
                Logger.Instance.log("Card ID not read.");
            } else {
                uid = uid.Replace("-", "").ToLower();
                Logger.Instance.log("Card ID: {0}", uid);
                NfcCardEvent(uid);
            }
        }

        private string readUID(string readerName) {
            using (var context = contextFactory.Establish(SCardScope.System)) {
                using (var reader = new SCardReader(context)) {
                    var status = reader.Connect(readerName, SCardShareMode.Shared, SCardProtocol.Any);
                    if (status != SCardError.Success) {
                        Logger.Instance.log("Could not connect to reader {0}:\n{1}",
                            readerName,
                            SCardHelper.StringifyError(status));
                        return null;
                    }

                    var apdu = new CommandApdu(IsoCase.Case2Short, reader.ActiveProtocol) {
                        CLA = 0xFF,
                        Instruction = InstructionCode.GetData,
                        P1 = 0x00,
                        P2 = 0x00,
                        Le = 0
                    };

                    status = reader.BeginTransaction();
                    if (status != SCardError.Success) {
                        Logger.Instance.log("Could not begin transaction.");
                        return null;
                    }

                    var receiveBuffer = new byte[256];
                    status = reader.Transmit(SCardPCI.GetPci(reader.ActiveProtocol), apdu.ToArray(), new SCardPCI(), ref receiveBuffer);

                    if (status != SCardError.Success) {
                        Logger.Instance.log("Error: " + SCardHelper.StringifyError(status));
                        return null;
                    }

                    var responseApdu = new ResponseApdu(receiveBuffer, IsoCase.Case2Short, reader.ActiveProtocol);
                    if (!responseApdu.HasData) {
                        return null;
                    }
                    reader.EndTransaction(SCardReaderDisposition.Leave);
                    reader.Disconnect(SCardReaderDisposition.Reset);
                    return BitConverter.ToString(responseApdu.GetData());
                }
            }
        }
    }
}

using PCSC;
using PCSC.Iso7816;
using System;
using System.ComponentModel;
using System.Threading;


namespace SmartFighter {
    class Nfc {
        private static readonly IContextFactory contextFactory = ContextFactory.Instance;

        public static string[] GetReaderNames() {
            using (var context = contextFactory.Establish(SCardScope.System)) {
                return context.GetReaders();
            }
        }

        public static void run(object sender, DoWorkEventArgs args) {
            BackgroundWorker worker = sender as BackgroundWorker;

            while (!worker.CancellationPending) {
                using (var monitor = new SCardMonitor(contextFactory, SCardScope.System)) {
                    monitor.CardInserted += cardInserted;
                    monitor.Start(Config.nfcReaderName);
                    while (!worker.CancellationPending) {
                        Thread.Sleep(500);
                    }
                }
            }
        }

        private static void cardInserted(object sender, CardStatusEventArgs args) {
            var uid = readUID(args.ReaderName);
            Logger.Instance.log("Card UID: {0}", uid);
        }

        private static string readUID(string readerName) {
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

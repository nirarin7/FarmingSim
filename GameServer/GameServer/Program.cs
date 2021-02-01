using System;
using System.Threading;
using GameServer.Server;

namespace GameServer {
    class Program {
        private static bool _isRunning;

        static void Main(string[] args) {
            Console.Title = "Game Server";
            _isRunning = true;

            Thread mainThread = new Thread(MainThread);
            mainThread.Start();

            Server.GameServer.Start(4, 8080);
        }

        private static void MainThread() {
            Console.WriteLine($"Main thead started, running at {Constants.TicPerSec} ticks per second");
            DateTime nextLoop = DateTime.Now;
            while (_isRunning) {
                while (nextLoop < DateTime.Now) {
                    GameLogic.GameLogic.Update();
                    nextLoop = nextLoop.AddMilliseconds(Constants.MsPerTick);

                    if (nextLoop > DateTime.Now) {
                        Thread.Sleep(nextLoop - DateTime.Now);
                    }
                }
            }
        }
    }
}
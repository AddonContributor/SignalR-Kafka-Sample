using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using StockProcessor.Models;
using StockTickR.Clients;

namespace StockSimulator {
    class Program {
        static void Main (string[] args) {
            var source = new CancellationTokenSource ();
            var stockSimulator = new StockSimulator ();
            try {
                var service = new StockCollectionService ();
                var token = source.Token;
                service.StartAsync (token).GetAwaiter ().GetResult ();
            } catch (Exception ex) {
                source.Cancel ();
                Console.WriteLine ("[Error] " + DateTime.Now + " " + ex.Message + ", \n" + ex.StackTrace);
            } finally {
                source.Cancel ();
            }
        }
    }
}
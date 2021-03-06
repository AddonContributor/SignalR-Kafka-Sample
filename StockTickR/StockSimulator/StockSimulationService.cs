using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using StockProcessor.Models;
using StockTickR.Clients;

namespace StockSimulator {
    public class StockCollectionService : BackgroundService {
        protected override Task ExecuteAsync (CancellationToken stoppingToken) {
            var stockSimulator = new StockSimulator ();
            var stockClient = new StockClient ();
            var observable = stockSimulator.StocksStream (TimeSpan.FromSeconds (3), stoppingToken)
                .Where (stocks => stocks.Any ())
                .DistinctUntilChanged ()
                .Do (stocks => stockClient.AddRange (stocks))
                .Catch<IEnumerable<Stock>, Exception> (ex => {
                    Console.WriteLine ("[Error] " + DateTime.Now + " Catch: " + ex.Message + " : " + ex.StackTrace);
                    return Observable.Empty<IEnumerable<Stock>> ();
                });
            using (var stocks = observable.Subscribe ()) {
                Console.WriteLine (DateTime.Now + " Press any key to unsubscribe");
                observable.Wait ();
            }
            return Task.CompletedTask;
        }
    }
}
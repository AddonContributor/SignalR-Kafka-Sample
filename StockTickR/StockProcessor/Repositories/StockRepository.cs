using System.Linq;
using Microsoft.EntityFrameworkCore;
using StockProcessor.Models;
using StockProcessor.Repositories.Core;
using StockProcessor.Repositories.Interfaces;

namespace StockProcessor.Repositories {
    public class StockRepository : Repository<Stock, int>, IStockRepository {

        readonly StockDbContext _context;

        private Serilog.ILogger _logger {
            get;
        }

        public StockRepository (StockDbContext stockContext, Serilog.ILogger logger) : base (stockContext, logger) {
            _context = stockContext;
            _logger = logger;
        }

        public Stock GetStockBySymbol (string symbol) {
            _logger.Debug ("Get by symbol: " + symbol);
            return _context.Stocks
                .Where (b => b.Symbol == symbol)
                .Select (b => b)
                .ToList ()
                .FirstOrDefault ();
        }

        public Stock Insert (Stock stock) {
            _logger.Debug ("Insert " + stock);
            _context.Add (stock);
            return stock;
        }

        public void Update (Stock stock) {
            _logger.Debug ("Update " + stock.Symbol + " id = " + stock.Id);
            _context.Stocks.Update (stock);
            _context.Entry (stock).State = EntityState.Modified;
        }

        public void Delete (int id) {
            _logger.Debug ("Delete " + id);
            _context.Remove (Get (id));
        }
    }
}
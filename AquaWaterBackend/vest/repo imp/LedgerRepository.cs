using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VestEngine.Domain.Enums;

namespace VestEngine.Infastructure.Repositories.Implementation
{
    public class LedgerRepository : GenericRepository<Ledger>, ILedgerRepository
    {
        public LedgerRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<List<Ledger>> GetAllBusinessLedger(Guid businessId)
        {
            var businessLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.BusinessId == businessId).ToListAsync();
            return businessLedgers;
        }

        public async Task<List<Ledger>> GetLedgersByLedgerTypeId(Guid ledgerTypeId, Guid businessId)
        {
            var ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.LedgerTypeId == ledgerTypeId && x.BusinessId == businessId).ToListAsync();
            return ledgers;
        }
        public override async Task<Ledger> GetARecordAsync(Guid id)
        {
            var ledgers = await _dbSet.Include(x => x.LedgerType).FirstOrDefaultAsync(x => x.Id == id);
            return ledgers;
        }

        public async Task<Ledger> GetBusinessLedgerByLedgerName(string ledgerName, Guid businessId)
        {
            return await _dbSet.Include(x => x.LedgerType).FirstOrDefaultAsync(x => x.AccountName == ledgerName && x.BusinessId == businessId);
        }

        public async Task<List<Ledger>> GetLedgerGenericSearchAsync(string request, Guid businessId)
        {
            return await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(request)
        || x.AccountNumber == request
        && x.BusinessId == businessId)
      .ToListAsync();
        }

        public async Task<Ledger> GetLedgerByGLCodeOrGLId(string request, Guid businessId)
        {
            var ledgers = await _dbSet.FirstOrDefaultAsync(x => x.Id.ToString() == request || x.AccountNumber == request && x.BusinessId == businessId);
            return ledgers;
        }
        public async Task<IList<Ledger>> GetLedgersByGLCodeOrGLId(string request, Guid businessId)
        {
            var ledgers = await _dbSet.Where(x => x.Id == Guid.Parse(request) || x.AccountNumber == request && x.BusinessId == businessId).ToListAsync();
            return ledgers;
        }

        public async Task<List<Ledger>> GetChartOfAccountByFiltering(Guid businessId, string productId, string productType, string accountName, string accountNumber)
        {
            var ledgers = new List<Ledger>();
            var count = 0;
            var paramsArray = new string[] { productId, productType, accountName, accountNumber };
            foreach (var item in paramsArray)
            {
                if (!string.IsNullOrEmpty(item)) count++;
            }
            if (count == 4) ledgers = await FourConditonalLedgerFilter(businessId, productId, productType, accountName, accountNumber);
            else if(count == 3) ledgers =  await ThreeConditonalLedgerFilter(businessId,productId, productType, accountName, accountNumber); 
            else if(count == 2) ledgers = await TwoConditionalLedgerFilter(businessId, productId, productType, accountName, accountNumber);
            else ledgers = await OneConditionalLedgerFilter(businessId, productId, productType,accountName, accountNumber);
            return ledgers;
        }

        private async Task<List<Ledger>> FourConditonalLedgerFilter(Guid businessId, string productId, string productType, string accountName, string accountNumber)
        {
            var ledgers = new List<Ledger>();
            if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => (int)x.ProductType == int.Parse(productType) && x.Id.ToString() == productId);
                if(product != null)
                {
                    var ledger = await _dbSet.Include(x => x.LedgerType).FirstOrDefaultAsync(x => x.AccountNumber == accountNumber && x.AccountName.Contains(accountName));
                    if (product.DefaultLedgerId == ledger.Id || product.InterestLedgerId == ledger.Id || product.ChargeLedgerId == ledger.Id)
                    {
                        ledgers.Add(ledger);
                    }
                }
               
            }
            return ledgers;
        }
        private async Task<List<Ledger>> ThreeConditonalLedgerFilter(Guid businessId, string productId, string productType, string accountName, string accountNumber)
        {
            var ledgers = new List<Ledger>();
            if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => (int)x.ProductType == int.Parse(productType) && x.Id.ToString() == productId && x.BusinessId == businessId);
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName)).ToListAsync();
                
            }
            else if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => (int)x.ProductType == int.Parse(productType) && x.Id.ToString() == productId && x.BusinessId == businessId);
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountNumber == accountNumber).ToListAsync(); 
                if (product != null)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }

            }
            else if (string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                var products = await _dbContext.Products.Where(x => (int)x.ProductType == int.Parse(productType) && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName) && x.AccountNumber == accountNumber).ToListAsync();
                foreach (var product in products)
                {

                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            else
            {
                var products = await _dbContext.Products.Where(x => x.Id.ToString() == productId && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName) && x.AccountNumber == accountNumber).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            return ledgers;
        }

        private async Task<List<Ledger>> TwoConditionalLedgerFilter(Guid businessId, string productId, string productType, string accountName, string accountNumber)
        {
            var ledgers = new List<Ledger>();
            var products = new List<Product>();
            if (!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => (int)x.ProductType == int.Parse(productType) && x.Id.ToString() == productId && x.BusinessId == businessId);
                if (product != null)
                {
                    ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToListAsync();
                }
            }
            else if (!string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                products = await _dbContext.Products.Where(x => x.Id.ToString() == productId && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName)).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            else if (!string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                products = await _dbContext.Products.Where(x => x.Id.ToString() == productId && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountNumber == accountNumber).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            else if (string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                products = await _dbContext.Products.Where(x => (int)x.ProductType == int.Parse(productType) && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName)).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            else if (string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                products = await _dbContext.Products.Where(x => (int)x.ProductType == int.Parse(productType) && x.BusinessId == businessId).ToListAsync();
                var filteredLedgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountNumber == accountNumber).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = filteredLedgers.Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToList();
                }
            }
            else if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountNumber == accountNumber && x.AccountName.Contains(accountName)).ToListAsync();
            }
            return ledgers;
        }
        private async Task<List<Ledger>> OneConditionalLedgerFilter(Guid businessId, string productId, string productType, string accountName, string accountNumber)
        {
            var ledgers = new List<Ledger>();
            var products = new List<Product>();
            if (!string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id.ToString() == productId && x.BusinessId == businessId);
                if(product != null)
                {
                    ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToListAsync();
                }

            }
            else if (string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                products = await _dbContext.Products.Where(x => (int)x.ProductType == int.Parse(productType) && x.BusinessId == businessId).ToListAsync();
                foreach (var product in products)
                {
                    ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.Id == product.DefaultLedgerId || x.Id == product.InterestLedgerId || x.Id == product.ChargeLedgerId).ToListAsync();
                }
            }
            else if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && !string.IsNullOrEmpty(accountName) && string.IsNullOrEmpty(accountNumber))
            {
                ledgers = await _dbSet.Include(x => x.LedgerType).Include(x => x.LedgerType).Where(x => x.AccountName.Contains(accountName)
                        && x.BusinessId == businessId).ToListAsync();
            }
            else if (string.IsNullOrEmpty(productId) && string.IsNullOrEmpty(productType) && string.IsNullOrEmpty(accountName) && !string.IsNullOrEmpty(accountNumber))
            {
                ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountNumber == accountNumber
                       && x.BusinessId == businessId).ToListAsync();
            }
            return ledgers;
        }

    public async Task<List<Transaction>> GetBusinessLedgersByLedgerNameAndProductName(string AccountName, Guid businessId, int productType, string productName)
    {
      var products = new HashSet<Product>();
      var ledgers = await _dbSet.Include(x => x.LedgerType).Where(x => x.AccountName.Contains(AccountName) && x.BusinessId == businessId).ToListAsync();
      var transactions = new List<Transaction>();
      foreach (var ledger in ledgers)
      {
        if (productType == 4)
        {
          products.Add(_dbContext.Products.First(x => x.DefaultLedgerId == ledger.Id || x.ChargeLedgerId == ledger.Id ||
          x.InterestLedgerId == ledger.Id || x.Name == productName));
        }
        else
        {
          products.Add(_dbContext.Products.First(x => x.DefaultLedgerId == ledger.Id || x.ChargeLedgerId == ledger.Id ||
          x.InterestLedgerId == ledger.Id && (int)x.ProductType == productType || x.Name == productName));
        }
      }
      foreach (var product in products)
      {
        transactions.AddRange(await _dbContext.Transactions.Where(x => x.LedgerId == product.DefaultLedgerId).ToListAsync());
        transactions.AddRange(await _dbContext.Transactions.Where(x => x.LedgerId == product.ChargeLedgerId).ToListAsync());
        transactions.AddRange(await _dbContext.Transactions.Where(x => x.LedgerId == product.InterestLedgerId).ToListAsync());
      }
      return transactions;
    }
  }
}

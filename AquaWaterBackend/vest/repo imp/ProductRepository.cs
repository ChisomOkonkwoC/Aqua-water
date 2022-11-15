using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Implementation
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }
        public async Task<Product> GetProductByNameAsync(string productName)
        {
            var product = await _dbSet.Include(x => x.Country).FirstOrDefaultAsync(x => x.Name == productName);
            return product;
        }
        public async Task<List<Product>> GetProductsByCodeAsync(int code)
        {
            var product = await _dbSet.Include(x => x.Country).Where(x => x.ProductCode == code).ToListAsync();
            return product;
        }
        public async Task<List<Product>> GetProductByTypeAsync(int type, Guid businessId)
        {
            var product = await _dbSet.Include(x => x.Country).Where(x => (int)x.ProductType == type && x.BusinessId == businessId).ToListAsync();
            return product;
        }
        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            var product = await _dbSet.Include(x => x.Country).FirstOrDefaultAsync(x => x.Id == id);
            return product;
        }
        public async Task<IEnumerable<Product>> GetAllBusinessProduct(Guid businessId)
        {
            var businessProducts = await _dbSet.Include(x => x.Country).Where(x => x.BusinessId == businessId).ToListAsync();
            return businessProducts;
        }

        public async Task<IEnumerable<Product>> GetProductByLedgerId(List<Ledger> ledgers)
        {
            var ledgersIdArray = ledgers.Select(x => x.Id).ToArray();
            var products = new List<Product>();
            foreach (var ledgerId in ledgersIdArray)
            {
                var product = await _dbContext.Products.Include(x => x.Country).FirstOrDefaultAsync(x => x.DefaultLedgerId == ledgerId
                || x.ChargeLedgerId == ledgerId || x.InterestLedgerId == ledgerId);
                if (product != null)
                {
                    products.Add(product);
                }

            }
            return products;
        }
        public async Task<Product> GetProductByLedgerId(Guid ledgerId)
        {

            var product = await _dbContext.Products.Include(x => x.Country).FirstOrDefaultAsync(x => x.DefaultLedgerId == ledgerId
            || x.ChargeLedgerId == ledgerId || x.InterestLedgerId == ledgerId);
            return product;
        }
        public async Task<Product> GetProductByProductNameByProductTypeAndBusinessId(string productName, int productType, Guid businessId)
        {
            return await _dbSet.Include(x => x.Country).FirstOrDefaultAsync(x => x.Name == productName && (int)x.ProductType == productType && x.BusinessId == businessId);
        }
        public async Task<List<Product>> GetProductGenericSearchAsync(string request, Guid businessId)
        {
            return await _dbSet.Include(x => x.Country).Where(x => x.Name.Contains(request)
        || x.MinDeposit.ToString() == request
        || x.MaxDeposit.ToString() == request
        || x.InterestRate.ToString() == request
        //|| x.Tax.Equals(request)
        || x.Status.ToString() == request
        && x.BusinessId == businessId)
        .ToListAsync();
        }

    public async Task<List<Transaction>> GetProductsByName(string name, int productType, Guid businessId)
    {
      var transactions = new List<Transaction>();
      var products = new List<Product>();
      if (productType == 4)
      {
        products = await _dbSet.Include(x => x.Country).Where(x => x.Name.Contains(name) && x.BusinessId == businessId).ToListAsync();
      }
      else
      {
        products = await _dbSet.Include(x => x.Country).Where(x => x.Name.Contains(name) && (int)x.ProductType == productType && x.BusinessId == businessId).ToListAsync();
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
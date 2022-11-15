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
	public class AccountRepository : GenericRepository<Account>, IAccountRepository
	{

		public AccountRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
		public override async Task<Account> GetARecordAsync(Guid id)
		{
			var ledgers = await _dbSet.Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == id);
			return ledgers;
		}
		public async Task<IEnumerable<Customer>> GetAllAccountsByBusinessId(Guid businessId)
		{
			return await _dbContext.Customers.Where(x => x.BusinessId == businessId).Include(x => x.Accounts).ToListAsync();
		}
		public async Task<IList<Account>> GetAccountsByProductId(Guid productId)
		{
			return await _dbSet.Where(x => x.ProductId == productId).ToListAsync();
		}

		public async Task<List<Account>> GetAccountByNameOrAccountNumber(string request)
		{
			return await _dbSet.Include(x => x.Customer).Where(x => x.AccountNumber == request || x.AccountName.Contains(request)).ToListAsync();
		}

		public async Task<Account> GetAccountByProductIdAndCustomerId(Guid productId, Guid customerId)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.ProductId == productId && x.CustomerId == customerId);
		}
		public async Task<List<Account>> GetAccountsByCustomerId(Guid customerId)
		{
			return await _dbSet.Include(x => x.Product).Where(x => x.CustomerId == customerId).ToListAsync();

		}
		public async Task<List<Transaction>> GetAccountByName(string request, Guid businessId, int productType)
		{
			var accounts = new List<Account>();
			var transactions = new List<Transaction>();
			if (productType == 4)
			{
				accounts = await _dbSet.Include(x => x.Customer).Include(x => x.Product)
					.Where(x => x.AccountName.Contains(request) && x.Customer.BusinessId == businessId).ToListAsync();
			}
			else
			{
				accounts = await _dbSet.Include(x => x.Customer).Include(x => x.Product)
					.Where(x => x.AccountName.Contains(request) && x.Customer.BusinessId == businessId && (int)x.Product.ProductType == productType).ToListAsync();
			}
			foreach (var account in accounts)
			{
				transactions.AddRange(await _dbContext.Transactions.Where(x => x.AccountId == account.Id).ToListAsync());
			}
			return transactions;
		}

		public async Task<List<Transaction>> GetAccountByNameAndProductName(string request, Guid businessId, int productType, string productName)
		{
			var accounts = new List<Account>();
			var transactions = new List<Transaction>();
			if (productType == 4)
			{
				accounts = await _dbSet.Include(x => x.Customer).Include(x => x.Product)
					.Where(x => x.AccountName.Contains(request) && x.Customer.BusinessId == businessId && x.Product.Name == productName).ToListAsync();
			}
			else
			{
				accounts = await _dbSet.Include(x => x.Customer).Include(x => x.Product)
					.Where(x => x.AccountName.Contains(request) && x.Customer.BusinessId == businessId && (int)x.Product.ProductType == productType && x.Product.Name == productName)
					.ToListAsync();
			}
			foreach (var account in accounts)
			{
				transactions.AddRange(await _dbContext.Transactions.Where(x => x.AccountId == account.Id).ToListAsync());
			}
			return transactions;
		}

	}
}

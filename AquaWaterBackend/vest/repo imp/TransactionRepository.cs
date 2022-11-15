using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
	{

		public TransactionRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
		public async Task<Transaction> GetTransactionByIdOrReference(string request)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).FirstOrDefaultAsync(x => x.Id.ToString() == request || x.Reference == request);
		}
		public override async Task<Transaction> GetARecordAsync(Guid Id)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).FirstOrDefaultAsync(x => x.Id == Id);
		}
		public async Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).Where(x => x.AccountId == accountId).ToListAsync();
		}
		public async Task<List<Transaction>> GetTransactionsByLedgerId(Guid ledgerId)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).Include(x => x.Ledger).Where(x => x.LedgerId == ledgerId).ToListAsync();
		}
		public async Task<List<Transaction>> GetTransactionsByTransactioTypeId(Guid transactionTypeId)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).Where(x => x.LedgerId == transactionTypeId).ToListAsync();
		}

		public async Task<List<Transaction>> GetTransactionByDateRange(DateTime startDate, DateTime endDate)
		{
			return await _dbSet.Include(x => x.TransactionType).Include(x => x.Account).Where(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate).Distinct().ToListAsync();
		}
	}
}

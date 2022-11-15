using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class TransactionTypeRepository : GenericRepository<TransactionType>, ITransactionTypeRepository
	{
		public TransactionTypeRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}

		public async Task<List<TransactionType>> GetTransactionTypesByBusinessId(Guid businessId)
		{
			var transactionTypes = await _dbSet.Where(x => x.Name == "Deposit" || x.Name == "Withdrawal").ToListAsync();
			transactionTypes.AddRange(await _dbSet.Where(x => x.BusinessId == businessId).ToListAsync());
			return transactionTypes;
		}
		public async Task<List<TransactionType>> GetDefaultTransactionTypes()
		{
			return await _dbSet.Where(x => x.Name == "Deposit" || x.Name == "Withdrawal").ToListAsync();
		}
	}
}

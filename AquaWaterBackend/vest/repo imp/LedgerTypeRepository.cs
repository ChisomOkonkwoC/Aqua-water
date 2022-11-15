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
	public class LedgerTypeRepository : GenericRepository<LedgerType>, ILedgerTypeRepository
	{
		public LedgerTypeRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}

		public async Task<LedgerType> GetLedgerTypeByGlCodeOrType(string request)
		{
			return await _dbSet.Include(x => x.ledgers).FirstOrDefaultAsync(x => x.Code == request || x.Type == request);
		}
		public async Task<LedgerType> GetLedgerTypeByName(string name)
		{
			return await _dbSet.FirstOrDefaultAsync(x => x.Name == name);
		}
	}
}

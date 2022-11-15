using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class BusinessRepository : GenericRepository<Business>, IBusinessRepository
	{
		public BusinessRepository(AppDbContext appContext) : base(appContext)
		{

		}
		public override async Task<Business> GetARecordAsync(Guid id)
		{
			return await _dbSet.Include(x => x.Customers).FirstOrDefaultAsync(x => x.Id == id);
			;
		}
	}
}

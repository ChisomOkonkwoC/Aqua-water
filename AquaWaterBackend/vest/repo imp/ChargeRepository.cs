using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
namespace VestEngine.Infastructure.Repositories.Implementation
{
    public class ChargeRepository : GenericRepository<Charge>, IChargeRepository
    {
        public ChargeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<IEnumerable<Charge>> GetChargesByProductType(int productType, Guid businessId)
        {
            return  await _dbSet.Where(x => (int)x.ProductType == productType && x.BusinessId == businessId).ToListAsync();
        }

        public async Task<Charge> GetChargeByIdAndBusinessId(Guid id, Guid businessId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.BusinessId == businessId);
        }
        public async Task<IEnumerable<Charge>> GetChargesByBusinessId(Guid businessId)
        {
          return await _dbSet.Where(x => x.BusinessId == businessId).ToListAsync();
        }
  }
}
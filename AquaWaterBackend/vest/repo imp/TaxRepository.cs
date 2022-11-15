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
    public class TaxRepository : GenericRepository<Tax>, ITaxRepository
    {

        public TaxRepository(AppDbContext appDbContext) : base(appDbContext)
        {
            
        }

        public async Task<Tax> GetTaxByProductType(int productType, Guid businessId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => (int)x.ProductType == productType && x.BusinessId == businessId);
        }

        public async Task<Tax> GetTaxByIdAndBusinessId(Guid id, Guid businessId)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.BusinessId == businessId);
        }
        public async Task<IEnumerable<Tax>> GetTaxesByBusinessId(Guid businessId)
        {
          return await _dbSet.Where(x => x.BusinessId == businessId).ToListAsync();
        }
  }
}

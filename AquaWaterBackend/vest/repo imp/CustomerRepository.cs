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
  public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
  {
    public CustomerRepository(AppDbContext appDbContext) : base(appDbContext)
    {
    }

    public override async Task<Customer> GetARecordAsync(Guid id)
    {
      var ledgers = await _dbSet.Include(x => x.Country).Include(x => x.Accounts).FirstOrDefaultAsync(x => x.Id == id);
      return ledgers;
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersOfBusiness(Guid businessId)
    {
      return await _dbSet.Include(x => x.Country).Where(x => x.BusinessId == businessId).ToListAsync();
    }
    public async Task<List<Customer>> GetCustomerDetailsAsync(string request, Guid businessId)
    {
      return await _dbSet.Include(x => x.Country).Where(x => x.Id.ToString() == request || x.FirstName.Contains(request)
      || x.LastName.Contains(request)
      || x.Email.Contains(request)
      || (x.FirstName + " " + x.LastName).Contains(request)
      || (x.LastName + " " + x.FirstName).Contains(request) && x.BusinessId == businessId)
  .ToListAsync();
    }

    public async Task<Customer> GetCustomerByEmailAndBusinessId(string email, Guid businessId)
    {
      return await _dbSet.Include(x => x.Country).FirstOrDefaultAsync(x => x.Email == email && x.BusinessId == businessId);
    }

    /*	public async Task<IEnumerable<Customer>> GetCustomerByUserIdAndBusinessId(string Guid businessId)
        {
            return await _dbSet.Where(x => x.UserId == userId && x.BusinessId == businessId).ToListAsync();
        }
*/
    /*public async Task<Customer> GetCustomerByUserId(string userId)
{
  return await _dbSet.FirstOrDefaultAsync(x => x.UserId == userId);
}*/
  }
}

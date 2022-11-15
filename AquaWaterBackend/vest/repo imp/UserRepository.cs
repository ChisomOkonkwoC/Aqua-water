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
	public class UserRepository : GenericRepository<User>, IUserRepository
	{
		public UserRepository(AppDbContext appContext) : base(appContext)
		{
		}

		public async Task<List<User>> GetUserDetailsAsync(string request)
		{
			return await _dbSet.Where(x => x.FirstName.Contains(request)
		|| x.LastName.Contains(request)
		|| x.Email.Contains(request)
		|| (x.FirstName + " " + x.LastName).Contains(request)
		|| (x.LastName + " " + x.FirstName).Contains(request))
	.ToListAsync();
		}
	}
}

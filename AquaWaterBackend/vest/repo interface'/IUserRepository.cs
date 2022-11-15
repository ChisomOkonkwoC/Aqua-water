using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface IUserRepository : IGenericRepository<User>
	{
	 Task<List<User>> GetUserDetailsAsync(string request);
	}
}
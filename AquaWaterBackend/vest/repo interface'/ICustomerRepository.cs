using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface ICustomerRepository : IGenericRepository<Customer>
	{
		Task<IEnumerable<Customer>> GetAllCustomersOfBusiness(Guid businessId);
		//Task<IEnumerable<Customer>> GetCustomerByUserIdAndBusinessId(string userId, Guid businessId);
		Task<Customer> GetCustomerByEmailAndBusinessId(string email, Guid businessId);
		//Task<Customer> GetCustomerByUserId(string userId);
		Task<List<Customer>> GetCustomerDetailsAsync(string request, Guid businessId);
	}
}
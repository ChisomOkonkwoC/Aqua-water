using VestEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<IEnumerable<Customer>> GetAllAccountsByBusinessId(Guid businessId);
        Task<IList<Account>> GetAccountsByProductId(Guid productId);
        Task<List<Account>> GetAccountByNameOrAccountNumber(string request);
        Task<Account> GetAccountByProductIdAndCustomerId(Guid productId, Guid customerId);
        Task<List<Account>> GetAccountsByCustomerId(Guid customerId);
        Task<List<Transaction>> GetAccountByName(string request, Guid businessId, int productType);
        Task<List<Transaction>> GetAccountByNameAndProductName(string request, Guid businessId, int productType, string productName);
  }
}
}
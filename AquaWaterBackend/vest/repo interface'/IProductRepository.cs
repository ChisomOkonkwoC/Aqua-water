using VestEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface IProductRepository : IGenericRepository<Product>
	{
		Task<Product> GetProductByIdAsync(Guid id);
		Task<Product> GetProductByNameAsync(string productName);
		Task<List<Product>> GetProductsByCodeAsync(int code);
		Task<List<Product>> GetProductByTypeAsync(int type, Guid businessId);
		Task<IEnumerable<Product>> GetAllBusinessProduct(Guid businessId);
		Task<IEnumerable<Product>> GetProductByLedgerId(List<Ledger> ledgers);
		Task<Product> GetProductByProductNameByProductTypeAndBusinessId(string productName, int productType, Guid businessId);
		Task<List<Product>> GetProductGenericSearchAsync(string request, Guid businessId);
		Task<Product> GetProductByLedgerId(Guid ledgerId);
		Task<List<Transaction>> GetProductsByName(string name, int productType, Guid businessId);
	}
}
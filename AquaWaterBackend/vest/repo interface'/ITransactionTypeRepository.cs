using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface ITransactionTypeRepository : IGenericRepository<TransactionType>
	{
		Task<List<TransactionType>> GetTransactionTypesByBusinessId(Guid businessId);
		Task<List<TransactionType>> GetDefaultTransactionTypes();
	}
}
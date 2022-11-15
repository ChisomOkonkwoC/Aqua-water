using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface ITransactionRepository : IGenericRepository<Transaction>
	{
		Task<Transaction> GetTransactionByIdOrReference(string request);
		Task<List<Transaction>> GetTransactionsByAccountId(Guid accountId);
		Task<List<Transaction>> GetTransactionsByLedgerId(Guid ledgerId);
		Task<List<Transaction>> GetTransactionsByTransactioTypeId(Guid transactionTypeId);
		Task<List<Transaction>> GetTransactionByDateRange(DateTime startDate, DateTime endDate);
	}
}
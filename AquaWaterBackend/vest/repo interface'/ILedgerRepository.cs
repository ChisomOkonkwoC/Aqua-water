using VestEngine.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface ILedgerRepository : IGenericRepository<Ledger>
	{
		Task<List<Ledger>> GetAllBusinessLedger(Guid businessId);
		Task<List<Ledger>> GetLedgersByLedgerTypeId(Guid ledgerTypeId, Guid businessId);
		Task<Ledger> GetBusinessLedgerByLedgerName(string ledgerName, Guid businessId);
		Task<List<Ledger>> GetLedgerGenericSearchAsync(string request, Guid businessId);
		Task<Ledger> GetLedgerByGLCodeOrGLId(string request, Guid businessId);
		Task<IList<Ledger>> GetLedgersByGLCodeOrGLId(string request, Guid businessId);
		Task<List<Ledger>> GetChartOfAccountByFiltering(Guid businessId, string productId, string productType, string accountName, string accountNumber);
	}
}
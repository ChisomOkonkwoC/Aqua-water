using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface ILedgerTypeRepository : IGenericRepository<LedgerType>
	{
		Task<LedgerType> GetLedgerTypeByGlCodeOrType(string request);
		Task<LedgerType> GetLedgerTypeByName(string name);
	}
}
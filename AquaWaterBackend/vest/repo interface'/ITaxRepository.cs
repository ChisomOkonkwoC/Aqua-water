using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
    public interface ITaxRepository : IGenericRepository<Tax>
    {
        Task<Tax> GetTaxByProductType(int productType, Guid businessId);
        Task<Tax> GetTaxByIdAndBusinessId(Guid id, Guid businessId);
        Task<IEnumerable<Tax>> GetTaxesByBusinessId(Guid businessId);
    }
}
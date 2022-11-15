using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;
namespace VestEngine.Infastructure.Repositories.Interfaces
{
    public interface IChargeRepository : IGenericRepository<Charge>
    {
        Task<IEnumerable<Charge>> GetChargesByProductType(int productType, Guid businessId);
        Task<Charge> GetChargeByIdAndBusinessId(Guid id, Guid businessId);
        Task<IEnumerable<Charge>> GetChargesByBusinessId(Guid businessId);
    }
}
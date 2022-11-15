using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
  public interface IGenericRepository<T> where T : class
  {
    IQueryable<T> Table { get; }
    IQueryable<T> TableNoTracking { get; }
    Task<bool> AddAsync(T entity);
    Task<bool> AddRangeAsync(IEnumerable<T> entities);
    Task<bool> DeleteAsync(T entity);
    Task<bool> DeleteRangeAsync(IEnumerable<T> entities);
    Task<IEnumerable<T>> GetAllRecordAsync();
    Task<T> GetARecordAsync(Guid Id);
    Task<T> GetARecordAsync(string Id);
    Task<bool> UpdateAsync(T entity);
    Task<bool> UpdateRangeAsync(IEnumerable<T> entities);
  }
}

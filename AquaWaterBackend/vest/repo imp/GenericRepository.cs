using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class GenericRepository<T> : IGenericRepository<T> where T : class
	{
		protected AppDbContext _dbContext;
		public DbSet<T> _dbSet;

		public GenericRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
			_dbSet = _dbContext.Set<T>();
		}

		public async Task<bool> AddAsync(T entity)
		{
			try
			{
				await _dbSet.AddAsync(entity);
				return await SaveAsync();
			}
			catch
			{
				throw;
			}

		}

		public async Task<bool> AddRangeAsync(IEnumerable<T> entities)
		{
			_dbSet.AddRange(entities);
			return await SaveAsync();
		}

		public async Task<bool> DeleteAsync(T entity)
		{
			_dbSet.Remove(entity);
			return await SaveAsync();
		}

		public async Task<bool> DeleteRangeAsync(IEnumerable<T> entities)
		{
			_dbSet.RemoveRange(entities);
			return await SaveAsync();
		}

		public async Task<IEnumerable<T>> GetAllRecordAsync()
		{
			return await _dbSet.ToListAsync();
		}

		public async Task<T> GetARecordAsync(string Id)
		{
			return await _dbSet.FindAsync(Id);
		}

		public virtual async Task<T> GetARecordAsync(Guid Id)
		{
			return await _dbSet.FindAsync(Id);
		}

		public virtual async Task<bool> UpdateAsync(T entity)
		{
			_dbSet.Update(entity);
			return await SaveAsync();
		}

		public async Task<bool> UpdateRangeAsync(IEnumerable<T> entities)
		{
			_dbContext.Set<T>().UpdateRange(entities);
			return await SaveAsync();
		}
		protected virtual DbSet<T> Entities
		{
			get
			{
				if (_dbSet == null)
				{
					_dbSet = _dbContext.Set<T>();
				}
				return _dbSet;
			}
		}
		public virtual IQueryable<T> Table
		{
			get
			{
				return Entities;
			}
		}
		public virtual IQueryable<T> TableNoTracking
		{
			get
			{
				return Entities.AsNoTracking();
			}
		}

		/*	public virtual async Task<IEnumerable<T>> Table()
			{
				return await Entities.ToListAsync();
			}
			public virtual async Task<IEnumerable<T>> TableNoTracking()
			{

				return await Entities.AsNoTracking().ToListAsync();

			}*/
		private protected async Task<bool> SaveAsync()
		{
			return await _dbContext.SaveChangesAsync() > 0;
		}
	}
}
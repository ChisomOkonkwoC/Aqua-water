using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class CountryRepository : GenericRepository<Country>, ICountryRepository
	{
		public CountryRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
	}
}

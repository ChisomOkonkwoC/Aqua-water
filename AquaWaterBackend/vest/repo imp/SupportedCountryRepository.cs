using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class SupportedCountryRepository : GenericRepository<SupportedCountry>, ISupportedCountryRepository
	{
		public SupportedCountryRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}

	}
}
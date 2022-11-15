using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VestEngine.Domain.Entities;
using VestEngine.Infastructure.Data;
using VestEngine.Infastructure.Repositories.Interfaces;

namespace VestEngine.Infastructure.Repositories.Implementation
{
	public class MemberRepository : GenericRepository<Member>, IMemberRepository
	{
		public MemberRepository(AppDbContext appDbContext) : base(appDbContext)
		{
		}
		public async Task<Member> GetMemberByUserId(string userId)
		{
			return await _dbSet.Include(x => x.Business).FirstOrDefaultAsync(x => x.UserId == userId);
		}
	}
}

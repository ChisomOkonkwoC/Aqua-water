using System.Threading.Tasks;
using VestEngine.Domain.Entities;

namespace VestEngine.Infastructure.Repositories.Interfaces
{
	public interface IMemberRepository : IGenericRepository<Member>
	{
		 Task<Member> GetMemberByUserId(string userId);
	}
}
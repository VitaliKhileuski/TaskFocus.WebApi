using System.Threading.Tasks;
using TaskFocus.Data.Entities;

namespace TaskFocus.Data.Interfaces;

public interface IUserRepository : IBaseRepository<UserEntity>
{
    Task<bool> IsUserExistedByEmail(string email);
}
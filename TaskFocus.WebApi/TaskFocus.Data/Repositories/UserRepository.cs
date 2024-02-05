using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskFocus.Data.Entities;
using TaskFocus.Data.Interfaces;

namespace TaskFocus.Data.Repositories;

public class UserRepository : BaseRepository<UserEntity>, IUserRepository
{
    protected sealed override IQueryable<UserEntity> CollectionWithIncludes { get; set; }
    
    public UserRepository(Context context) : base(context)
    {
        CollectionWithIncludes = context.Users;
    }

    public async Task<bool> IsUserExistedByEmail(string email)
    {
        return await CollectionWithIncludes.AnyAsync(x => x.Email == email.ToLower());
    }
}
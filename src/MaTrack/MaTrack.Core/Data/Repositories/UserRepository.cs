using MaTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Core.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }
}

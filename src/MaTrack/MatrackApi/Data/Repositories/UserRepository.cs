using MaTrack.Core.Entities;
using MatrackApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Core.Data.Repositories
{
    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IUserRepository : IBaseRepository<UserEntity>
    {
    }
}

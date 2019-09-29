using MaTrack.Core.Entities;
using MatrackApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Core.Data.Repositories
{
    public class AdminRepository : BaseRepository<AdminEntity>, IAdminRepository
    {
        public AdminRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IAdminRepository : IBaseRepository<AdminEntity>
    {
    }
}

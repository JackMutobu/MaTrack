using MaTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Core.Data.Repositories
{
    public class AdminRepository : BaseRepository<AdminEntity>, IAdminRepository
    {
        public AdminRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IAdminRepository : IBaseRepository<AdminEntity>
    {
    }
}

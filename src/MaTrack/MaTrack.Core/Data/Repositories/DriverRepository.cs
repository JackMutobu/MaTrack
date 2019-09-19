using MaTrack.Core.Data;
using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Shared.Data.Repositories
{
    public class DriverRepository : BaseRepository<DriverEntity>, IDriverRepository
    {
        public DriverRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IDriverRepository : IBaseRepository<DriverEntity>
    {
    }
}

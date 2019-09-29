using MaTrack.Core.Data;
using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using MatrackApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Shared.Data.Repositories
{
    public class DriverRepository : BaseRepository<DriverEntity>, IDriverRepository
    {
        public DriverRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IDriverRepository : IBaseRepository<DriverEntity>
    {
    }
}

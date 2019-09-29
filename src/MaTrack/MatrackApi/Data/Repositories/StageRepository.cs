using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using MatrackApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Shared.Data.Repositories
{
    public class StageRepository : BaseRepository<StageEntity>, IStageRepository
    {
        public StageRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IStageRepository : IBaseRepository<StageEntity>
    {
    }
}

using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;

namespace MatrackApi.Data.Repositories
{
    public class TripRepository : BaseRepository<TripEntity>, ITripRepository
    {
        public TripRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface ITripRepository : IBaseRepository<TripEntity>
    {
    }
}

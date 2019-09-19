using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Shared.Data.Repositories
{
    public class VehicleRepository : BaseRepository<VehicleEntity>, IVehicleRepository
    {
        public VehicleRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IVehicleRepository : IBaseRepository<VehicleEntity>
    {
    }
}

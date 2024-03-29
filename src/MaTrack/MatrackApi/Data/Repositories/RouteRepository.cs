﻿using MaTrack.Core.Data.Repositories;
using MaTrack.Core.Entities;
using MatrackApi.Data;
using Microsoft.EntityFrameworkCore;

namespace MaTrack.Shared.Data.Repositories
{
    public class RouteRepository : BaseRepository<RouteEntity>, IRouteRepository
    {
        public RouteRepository(MatrackApiDbContext dbContext) : base(dbContext)
        {
        }
    }

    public interface IRouteRepository : IBaseRepository<RouteEntity>
    {
    }
}

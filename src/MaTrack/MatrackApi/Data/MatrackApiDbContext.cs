using MaTrack.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace MatrackApi.Data
{
    public class MatrackApiDbContext:DbContext
    {
        public MatrackApiDbContext(DbContextOptions<MatrackApiDbContext> options) : base(options) { }
        public DbSet<AdminEntity> Admins { get; set; }
        public DbSet<DriverEntity> Drivers { get; set; }
        public DbSet<StageEntity> Stages { get; set; }
        public DbSet<RouteEntity> Routes { get; set; }
        public DbSet<VehicleEntity> Vehicles { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<RouteStageEntity>()
        .HasKey(rs => new { rs.RouteId, rs.StageId });
            modelBuilder.Entity<RouteStageEntity>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.RouteStages)
                .HasForeignKey(rs => rs.RouteId);
            modelBuilder.Entity<RouteStageEntity>()
               .HasOne(rs => rs.Stage)
               .WithMany(r => r.RouteStages)
               .HasForeignKey(rs => rs.StageId);
            modelBuilder.Entity<VehicleRouteEntity>()
       .HasKey(rs => new { rs.RouteId, rs.VeicleId });
            modelBuilder.Entity<VehicleRouteEntity>()
                .HasOne(rs => rs.Route)
                .WithMany(r => r.VehicleRoutes)
                .HasForeignKey(rs => rs.RouteId);
            modelBuilder.Entity<VehicleRouteEntity>()
               .HasOne(vr => vr.Vehicle)
               .WithMany(v => v.VehicleRoutes)
               .HasForeignKey(vr => vr.VeicleId);
        }
    }
}

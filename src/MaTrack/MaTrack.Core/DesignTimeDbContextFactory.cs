using MaTrack.Core.Data;
using Microsoft.EntityFrameworkCore.Design;
using System.Diagnostics;
using System.IO;

namespace MaTrack.Core
{
    public class DesignTimeDbContextFactory: IDesignTimeDbContextFactory<MaTrackDbContext>
    {
        public MaTrackDbContext CreateDbContext(string[] args)
        {
            Debug.WriteLine(Directory.GetCurrentDirectory() + @"\Config.db");

            return new MaTrackDbContext(Directory.GetCurrentDirectory() + @"\Config.db");
        }
    }
}

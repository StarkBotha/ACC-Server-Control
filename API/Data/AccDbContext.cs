using api.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class AccDbContext : DbContext
    {
        public AccDbContext(DbContextOptions<AccDbContext> options)
            : base(options)
        {

        }

        DbSet<Track> Tracks { get; set; }
        DbSet<SessionType> SessionTypes { get; set; }
    }
}
using GameWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Models;

namespace GameWebApi.EntityFramework
{
    public class EFGameDBContext : DbContext
    {
        public EFGameDBContext(DbContextOptions<EFGameDBContext> options) : base(options)
        {

        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<GameGenre> GameGenres { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             base.OnModelCreating(modelBuilder);
        }
    }
}

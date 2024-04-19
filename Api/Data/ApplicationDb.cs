using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data
{
    public class ApplicationDb: DbContext
    {
        public ApplicationDb(DbContextOptions<ApplicationDb> options): base(options)
        {
        }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<Artists> Artists { get; set; }

        public DbSet<ArtistAlbumBridge> ArtistAlbumBridge { get; set; }
        public DbSet<Album> Albums { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ArtistAlbumBridge>()
                .HasKey(x => new { x.AlbumId, x.ArtistId });
            modelBuilder.Entity<Artists>()
                .HasMany(x=>x.Albums)
                .WithOne(x=>x.Artist)
                .HasForeignKey(x=>x.ArtistId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Album>()
                .HasMany(x => x.Artists)
                .WithOne(x => x.Album)
                .HasForeignKey(x => x.AlbumId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

using LabelSongsAPI.Models;
using LabelSongsAPI.Models.Relations;
using Microsoft.EntityFrameworkCore;
namespace LabelSongsAPI.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Label> Label { get; set; }
        public DbSet<Composer> Composers { get; set; }
        public DbSet<Song> Songs { get; set; }
        public DbSet<SongComposer> SongComposers { get; set; }
        public DbSet<SongCategory> SongCategories { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reviewer> Reviewers { get; set; }

        protected override void OnModelCreating(ModelBuilder model)
        {
            model.Entity<SongCategory>()
                .HasKey(c => new {c.IdSong, c.IdCategory});
            model.Entity<SongCategory>()
                .HasOne(c => c.Song)
                .WithMany(c => c.SongCategories)
                .HasForeignKey(c => c.IdCategory);
            model.Entity<SongCategory>()
                .HasOne(c => c.Category)
                .WithMany(c => c.SongCategories)
                .HasForeignKey(c => c.IdCategory);

            model.Entity<SongComposer>()
                .HasKey(c => new { c.IdSong, c.IdComposer });
            model.Entity<SongComposer>()
                .HasOne(c => c.Song)
                .WithMany(c => c.SongComposers)
                .HasForeignKey(c => c.IdComposer);
            model.Entity<SongComposer>()
                .HasOne(c => c.Composer)
                .WithMany(c => c.SongComposers)
                .HasForeignKey(c => c.IdComposer);
        }
    }
}

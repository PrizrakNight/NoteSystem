using Microsoft.EntityFrameworkCore;

namespace NoteSystem.DAL.EfCore
{
    public class NoteSystemDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<Notebook> Notebooks { get; set; }

        public NoteSystemDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notebook>()
                .HasMany(n => n.Notes)
                .WithOne(n => n.Notebook)
                .HasForeignKey(n => n.NotebookId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

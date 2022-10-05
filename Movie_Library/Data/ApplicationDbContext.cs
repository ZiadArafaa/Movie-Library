using CRUD_Operations.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUD_Operations.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options ):base(options) { }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Genre>().Property(p=>p.Id).UseIdentityColumn();

            modelBuilder.Entity<Genre>().HasIndex(p => p.Name).IsUnique();
            modelBuilder.Entity<Movie>().HasIndex(p=>p.Name).IsUnique();
           

            modelBuilder.Entity<Genre>().HasMany<Movie>().WithOne(p=>p.Genre)
                .HasForeignKey(m=>m.GenreId).HasPrincipalKey(g=>g.Id)
                .OnDelete(DeleteBehavior.Restrict);


        }
        public DbSet<Movie> movies { get; set; }
        public DbSet<Genre> genres { get; set; }
        
    }
}

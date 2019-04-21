using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CookBook.Data.Models
{
    public partial class CookbookContext : DbContext
    {
        public CookbookContext()
        {
        }

        public CookbookContext(DbContextOptions<CookbookContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Recipe> Recipe { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=.;Database=Cookbook;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(150);

                entity.Property(e => e.UserId).HasMaxLength(50);

                entity.Property(e => e.PhotoUrl).HasMaxLength(50);
            });
        }
    }
}

using blog_backend.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace blog_backend.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Additional configurations can be added here
            builder.Entity<Category>(entity =>
            {
                entity.HasIndex(x => x.Slug).IsUnique();
                entity.Property(x => x.Name).IsRequired().HasMaxLength(150);
                entity.Property(x => x.Slug).IsRequired().HasMaxLength(180);
                entity.Property(x => x.CreatedAt).IsRequired();
            });
        }
    }
}

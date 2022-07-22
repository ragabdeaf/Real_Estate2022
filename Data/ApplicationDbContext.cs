using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Real_Estate.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Post>()
                    .HasOne(e => e.region)
                    .WithMany()
                    .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<govarnate> govarnate { get; set; }
        public DbSet<region> region { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<Post> post { get; set; }
        public DbSet<PostFav> postFavs { get; set; }
        public DbSet<PostType> PostType { get; set; }
        public DbSet<postRequest> PostRequests { get; set; }
    }
}
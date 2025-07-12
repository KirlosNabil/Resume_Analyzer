using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Resume_Analyzer.DataAccess.Models;

namespace Resume_Analyzer.DataAccess
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        public DbSet<Resume> Resumes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(u => u.Resume)
                .WithOne(r => r.User)
                .HasForeignKey<Resume>(r => r.UserId);
        }
    }
}

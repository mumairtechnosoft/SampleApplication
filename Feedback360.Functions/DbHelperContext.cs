using Feedback360.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Feedback360.Functions
{
    public class DbHelperContext : DbContext
    {
        public DbHelperContext(DbContextOptions<DbHelperContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.UseOpenIddict();
        }

        public DbSet<Feedback> Feedbacks { get; set; }
    }
}

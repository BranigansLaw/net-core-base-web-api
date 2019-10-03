using Microsoft.EntityFrameworkCore;
using prioritizemeServices.Core.Data;

namespace prioritizemeServices.Database
{
    /// <summary>
    /// The DB context of this application
    /// </summary>
    public class PrioritizeMeDbContext : DbContext
    {
        /// <summary>
        /// Create an instance of the class
        /// </summary>
        /// <param name="options">The application DB options</param>
        public PrioritizeMeDbContext(DbContextOptions<PrioritizeMeDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// The database stored <see cref="Person"/>s
        /// </summary>
        public DbSet<Person> People { get; set; }

        /// <inheritdoc />
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

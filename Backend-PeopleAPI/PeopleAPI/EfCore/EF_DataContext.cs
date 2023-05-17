using Microsoft.EntityFrameworkCore;

namespace PeopleAPI.EfCore
{
    public class EF_DataContext : DbContext

    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Addd the Postgres Extension for UUID generation
            modelBuilder.HasPostgresExtension("uuid-ossp");
        }
       
    }
}

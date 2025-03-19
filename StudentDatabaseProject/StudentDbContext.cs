using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace StudentDatabaseProject
{
    internal class StudentDbContext : DbContext
    {
        public DbSet<Student> Students { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(new ConfigurationBuilder()
                                            .AddJsonFile("appSettings.json")
                                            .Build()
                                            .GetSection("ConnectionStrings")["StudentDb"]);
        }
    }
}

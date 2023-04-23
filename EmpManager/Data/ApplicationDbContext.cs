using Microsoft.EntityFrameworkCore;
using EmpManager.Models;

namespace EmpManager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        // Neccesary for migrating table to database
        public DbSet<Employee> Employees { get; set; }

        // Default Method for seeding data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Michael", LastName = "Trout", Shift = 2, IsClockedIn = false },
                new Employee { Id = 2, FirstName = "Malcolm", LastName = "McCormick", Shift = 3, IsClockedIn = false},
                new Employee { Id = 3, FirstName = "Tim", LastName = "Henson", Shift = 1, IsClockedIn = false }
                );
        }
    }
}

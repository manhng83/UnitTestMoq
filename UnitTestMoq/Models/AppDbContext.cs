using Microsoft.EntityFrameworkCore;
using UnitTestMoq.Domain.Customers;

namespace UnitTestMoq.Models
{
    public partial class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<UnitTestMoq.Domain.Customers.Customer> Customer { get; set; }
    }
}
using dotNet6_Mvc_Crud_App.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotNet6_Mvc_Crud_App.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
    }
}

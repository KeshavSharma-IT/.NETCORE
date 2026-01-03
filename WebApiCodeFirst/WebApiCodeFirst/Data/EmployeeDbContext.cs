using Microsoft.EntityFrameworkCore;
using WebApiCodeFirst.Models;

namespace WebApiCodeFirst.Data
{
    public class EmployeeDbContext    :DbContext
    {
            public EmployeeDbContext(DbContextOptions options): base(options)
            {
            
            }
         public DbSet<Employee> employees { get; set; }
    }
}

using EmployeeAPI.DomainObject;
using Microsoft.EntityFrameworkCore;

namespace EmplooyeeWebAPI.BusinessFacade
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options )  : base (options)
        {


        }
        public DbSet<EmployeeDomainObject>Employees { get; set; }
    }
}

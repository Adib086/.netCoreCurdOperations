using CurdOperation.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace CurdOperation.Data
{
    public class MVCDemoDbcontext : DbContext
    {
        public MVCDemoDbcontext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> employees { get; set; }
    }
}

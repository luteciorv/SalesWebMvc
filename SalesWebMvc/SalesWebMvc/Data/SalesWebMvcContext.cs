using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Models
{
    public class SalesWebMvcContext : DbContext
    {
        public SalesWebMvcContext (DbContextOptions<SalesWebMvcContext> options)
            : base(options)
        {
        }

        // DbSet's das entidades
        public DbSet<Department> Department { get; set; } // Departamento
        public DbSet<Seller> Seller { get; set; } // Vendedor
        public DbSet<SalesRecord> SalesRecord { get; set; } // Registro de vendas
    }
}

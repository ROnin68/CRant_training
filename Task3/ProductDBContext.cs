using System.Data.Entity;

namespace Task3
{
    public class ProductDBContext : DbContext
    {
        public DbSet<ComparableProduct> Products { get; set; }
    }
}
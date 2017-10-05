using Entities;
using System.Data.Entity;

namespace DB
{

    public class ProductDBContext : DbContext
    {
        const string connName = "DBConnection";
        public ProductDBContext()
            : base(connName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ProductDBContext>());
        }
        public DbSet<Product> Products { get; set; }
    }
}
using System.Data.Entity;
using Task5.Library.Entities;

namespace Task5.Library.DB
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
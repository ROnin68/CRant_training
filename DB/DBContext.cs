using Entities;
using System.Data.Entity;

namespace DB
{
    public class DBContext : DbContext
    {
        const string connName = "DBConnection";
        public DBContext()
            : base(connName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DBContext>());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
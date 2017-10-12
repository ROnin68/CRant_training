using System.Data.Entity;
using Task5.Library.Entities;
using Task5.Library.Entities.Configurations;

namespace Task5.Library.DB
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
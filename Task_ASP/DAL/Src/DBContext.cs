using System.Data.Entity;
using Task_ASP.DAL.Entities;
using Task_ASP.DAL.Entities.Configurations;

namespace Task_ASP.DAL
{
    public class DBContext : DbContext
    {
        const string connName = "ASP_DBConnection";
        public DBContext()
            : base(connName)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<DBContext>());
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        #region "Configurations"
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ProductConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new OrderConfiguration());
            modelBuilder.Configurations.Add(new OrderDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }
        #endregion "Configurations"
    }
}
using DB;
using Entities;
using System.Data.Entity;

namespace BusinessLogic
{
    public class Task4DBContext : ProductDBContext
    {
        public Task4DBContext()
            : base("DBConnection")
        { }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
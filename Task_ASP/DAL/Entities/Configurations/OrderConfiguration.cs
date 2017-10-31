using System.Data.Entity.ModelConfiguration;

namespace Task_ASP.DAL.Entities.Configurations
{
    class OrderConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderConfiguration()
        {
            ToTable("Orders");
            HasKey(o => o.ID);

            HasRequired(o => o.Client)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.ClientID);

            HasMany(o => o.OrderDetails)
                .WithRequired(od => od.Order)
                .HasForeignKey(od => od.OrderID);
        }
    }
}

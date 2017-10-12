using System.Data.Entity.ModelConfiguration;

namespace Task5.Library.Entities.Configurations
{
    class OrderDetailConfiguration : EntityTypeConfiguration<OrderDetail>
    {
        public OrderDetailConfiguration()
        {
            ToTable("OrderDetails");
            HasKey(o => o.ID);

            HasRequired(od => od.Product)
                .WithMany()
                .HasForeignKey(od => od.ProductID);

            HasRequired(od => od.Order)
                .WithMany(o => o.OrderDetails)
                .HasForeignKey(od => od.OrderID);
        }
    }
}

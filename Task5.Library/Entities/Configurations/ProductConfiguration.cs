using System.Data.Entity.ModelConfiguration;

namespace Task5.Library.Entities.Configurations
{
    class ProductConfiguration : EntityTypeConfiguration<Product>
    {
        public ProductConfiguration()
        {
            ToTable("Products");
            HasKey(p => p.ID);
        }
    }
}

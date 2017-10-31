using System.Data.Entity.ModelConfiguration;

namespace Task_ASP.DAL.Entities.Configurations
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

using System.Data.Entity.ModelConfiguration;

namespace Task_ASP.DAL.Entities.Configurations
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            ToTable("Clients");
            HasKey(c => c.ID);

            HasMany(c => c.Orders)
                .WithRequired(o => o.Client)
                .HasForeignKey(o => o.ClientID);

        }
    }
}

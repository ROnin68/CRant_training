namespace Task5.Library.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Add_SP_ClientOrdersCost : DbMigration
    {
        public override void Up()
        {
            CreateStoredProcedure("ClientOrdersCost", AddProcedureScript);
        }

        public override void Down()
        {
            DropStoredProcedure("ClientOrdersCost");
        }

        private const string AddProcedureScript =
        @"SELECT 
          c.Name as Name, 
          SUM(p.Price * od.ProductQuantity) as OrderCost 
          FROM 
            Clients c, Orders o, OrderDetails od, Products p 
          WHERE 
            od.OrderID = o.ID and 
            od.ProductID = p.ID and 
            o.ClientID = c.ID 
          GROUP BY 
            c.ID, c.Name 
          Order BY 
            c.ID";

    }
}

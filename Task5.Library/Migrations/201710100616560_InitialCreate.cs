namespace Task5.Library.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Name = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Orders",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    ClientID = c.Int(nullable: false),
                    DateCreated = c.DateTime(nullable: false),
                    Status = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Clients", t => t.ClientID, cascadeDelete: true)
                .Index(t => t.ClientID);

            CreateTable(
                "dbo.OrderDetails",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    OrderID = c.Int(nullable: false),
                    ProductID = c.Int(nullable: false),
                    ProductQuantity = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.OrderID)
                .Index(t => t.ProductID);

            CreateTable(
                "dbo.Products",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    Code = c.String(),
                    Name = c.String(),
                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    GroupID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID);
        }

        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "ProductID", "dbo.Products");
            DropForeignKey("dbo.OrderDetails", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.Orders", "ClientID", "dbo.Clients");
            DropIndex("dbo.OrderDetails", new[] { "ProductID" });
            DropIndex("dbo.OrderDetails", new[] { "OrderID" });
            DropIndex("dbo.Orders", new[] { "ClientID" });
            DropTable("dbo.Products");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}

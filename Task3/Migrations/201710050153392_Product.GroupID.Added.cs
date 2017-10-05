namespace Task3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProductGroupIDAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "GroupID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "GroupID");
        }
    }
}

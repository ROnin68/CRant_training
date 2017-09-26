namespace Task3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGroupID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComparableProducts", "GroupID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComparableProducts", "GroupID");
        }
    }
}

namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Client_IsDeleted_Field : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Clients", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Clients", "IsDeleted");
        }
    }
}

namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsoftDelete2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "IsDeleted");
        }
    }
}

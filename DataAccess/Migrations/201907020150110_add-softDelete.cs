namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addsoftDelete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuotes", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuotes", "IsDeleted");
        }
    }
}

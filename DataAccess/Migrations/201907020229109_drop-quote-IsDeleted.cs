namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dropquoteIsDeleted : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Quotes", "IsDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "IsDeleted", c => c.Boolean(nullable: false));
        }
    }
}

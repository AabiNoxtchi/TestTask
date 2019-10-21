namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeNameCorrectAuthorName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuotes", "CorrectAuthorName", c => c.String());
            DropColumn("dbo.UserQuotes", "WriteAuthorName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserQuotes", "WriteAuthorName", c => c.String());
            DropColumn("dbo.UserQuotes", "CorrectAuthorName");
        }
    }
}

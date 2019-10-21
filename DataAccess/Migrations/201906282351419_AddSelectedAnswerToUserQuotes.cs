namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddSelectedAnswerToUserQuotes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserQuotes", "SelectedAnswer", c => c.String());
            AddColumn("dbo.UserQuotes", "WriteAuthorName", c => c.String());
            AddColumn("dbo.UserQuotes", "ShownAnswer", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.UserQuotes", "ShownAnswer");
            DropColumn("dbo.UserQuotes", "WriteAuthorName");
            DropColumn("dbo.UserQuotes", "SelectedAnswer");
        }
    }
}

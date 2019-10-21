namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        AuthorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.AuthorId, cascadeDelete: true)
                .Index(t => t.AuthorId);
            
            CreateTable(
                "dbo.UserQuotes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Boolean(nullable: false),
                        UserId = c.Int(nullable: false),
                        QuoteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quotes", t => t.QuoteId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.QuoteId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserQuotes", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserQuotes", "QuoteId", "dbo.Quotes");
            DropForeignKey("dbo.Quotes", "AuthorId", "dbo.Authors");
            DropIndex("dbo.UserQuotes", new[] { "QuoteId" });
            DropIndex("dbo.UserQuotes", new[] { "UserId" });
            DropIndex("dbo.Quotes", new[] { "AuthorId" });
            DropTable("dbo.Users");
            DropTable("dbo.UserQuotes");
            DropTable("dbo.Quotes");
            DropTable("dbo.Authors");
        }
    }
}

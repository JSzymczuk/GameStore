namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig005 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            AddColumn("dbo.Orders", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsDeleted");
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id");
        }
    }
}

namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig003 : DbMigration
    {
        public override void Up()
        {
            //AddColumn("dbo.Products", "ReleaseDate", c => c.DateTime(nullable: false));
            //AddColumn("dbo.Products", "IsDeleted", c => c.Boolean(nullable: false));
            //DropColumn("dbo.Products", "Released");

            DropForeignKey("Comments", "ArticleId");
            DropColumn("Comments", "ArticleId");

            AddColumn("Comments", "ArticleId", c => c.Guid(true));
            AddForeignKey("Comments", "ArticleId", "Articles");
        }
        
        public override void Down()
        {
            //AddColumn("dbo.Products", "Released", c => c.DateTime(nullable: false));
            //DropColumn("dbo.Products", "IsDeleted");
            //DropColumn("dbo.Products", "ReleaseDate");

            DropForeignKey("Comments", "ArticleId");
            DropColumn("Comments", "ArticleId");

            AddColumn("Comments", "ArticleId", c => c.Guid());
            AddForeignKey("Comments", "ArticleId", "Articles");
        }
    }
}

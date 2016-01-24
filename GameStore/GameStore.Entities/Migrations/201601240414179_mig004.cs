namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig004 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropIndex("dbo.Comments", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            CreateTable(
                "dbo.GalleryImages",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ImageLocation = c.String(),
                        ImageThumb = c.String(),
                        ProductId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            AddColumn("dbo.AspNetUsers", "NewsletterEnrolled", c => c.Boolean(nullable: false));
            AddColumn("dbo.PegiRatings", "SortIndex", c => c.Int(nullable: false));
            AlterColumn("dbo.Comments", "ProductId", c => c.Guid());
            AlterColumn("dbo.Comments", "ArticleId", c => c.Guid());
            CreateIndex("dbo.Comments", "ProductId");
            CreateIndex("dbo.Comments", "ArticleId");
            AddForeignKey("dbo.Comments", "ProductId", "dbo.Products", "Id");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
            DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
            DropForeignKey("dbo.GalleryImages", "ProductId", "dbo.Products");
            DropIndex("dbo.GalleryImages", new[] { "ProductId" });
            DropIndex("dbo.Comments", new[] { "ArticleId" });
            DropIndex("dbo.Comments", new[] { "ProductId" });
            AlterColumn("dbo.Comments", "ArticleId", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "ProductId", c => c.Guid(nullable: false));
            DropColumn("dbo.PegiRatings", "SortIndex");
            DropColumn("dbo.AspNetUsers", "NewsletterEnrolled");
            DropTable("dbo.GalleryImages");
            CreateIndex("dbo.Comments", "ArticleId");
            CreateIndex("dbo.Comments", "ProductId");
            AddForeignKey("dbo.Comments", "ArticleId", "dbo.Articles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
    }
}

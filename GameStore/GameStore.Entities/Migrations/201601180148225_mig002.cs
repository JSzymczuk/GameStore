namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig002 : DbMigration
    {
        public override void Up()
        {
            
        }

        public override void Down()
        {
            
        }
        //public override void Up()
        //{
        //    DropForeignKey("dbo.PegiContent", "RatingId", "dbo.PegiRatings");
        //    DropIndex("dbo.PegiContent", new[] { "RatingId" });
        //    CreateTable(
        //        "dbo.PegiContentPegiRatings",
        //        c => new
        //            {
        //                PegiContent_Id = c.Guid(nullable: false),
        //                PegiRating_Id = c.Guid(nullable: false),
        //            })
        //        .PrimaryKey(t => new { t.PegiContent_Id, t.PegiRating_Id })
        //        .ForeignKey("dbo.PegiContent", t => t.PegiContent_Id, cascadeDelete: true)
        //        .ForeignKey("dbo.PegiRatings", t => t.PegiRating_Id, cascadeDelete: true)
        //        .Index(t => t.PegiContent_Id)
        //        .Index(t => t.PegiRating_Id);

        //    DropColumn("dbo.PegiContent", "RatingId");
        //}

        //public override void Down()
        //{
        //    AddColumn("dbo.PegiContent", "RatingId", c => c.Guid(nullable: false));
        //    DropForeignKey("dbo.PegiContentPegiRatings", "PegiRating_Id", "dbo.PegiRatings");
        //    DropForeignKey("dbo.PegiContentPegiRatings", "PegiContent_Id", "dbo.PegiContent");
        //    DropIndex("dbo.PegiContentPegiRatings", new[] { "PegiRating_Id" });
        //    DropIndex("dbo.PegiContentPegiRatings", new[] { "PegiContent_Id" });
        //    DropTable("dbo.PegiContentPegiRatings");
        //    CreateIndex("dbo.PegiContent", "RatingId");
        //    AddForeignKey("dbo.PegiContent", "RatingId", "dbo.PegiRatings", "Id", cascadeDelete: true);
        //}
    }
}

namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Articles", "Image", c => c.String());
            AddColumn("dbo.Articles", "ImageThumb", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Articles", "ImageThumb");
            DropColumn("dbo.Articles", "Image");
        }
    }
}

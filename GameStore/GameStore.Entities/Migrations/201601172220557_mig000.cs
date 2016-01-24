namespace GameStore.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    //public partial class mig000 : DbMigration
    //{
    //    public override void Up()
    //    {
    //        CreateTable(
    //            "dbo.Addresses",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Region = c.Int(nullable: false),
    //                    City = c.String(),
    //                    Street = c.String(),
    //                    House = c.String(),
    //                    Local = c.String(),
    //                    PostalCode = c.String(),
    //                    Post = c.String(),
    //                    AdditionalInfo = c.String(),
    //                    UserId = c.String(maxLength: 128),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "dbo.Orders",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    OrderDate = c.DateTime(nullable: false),
    //                    Paid = c.Boolean(nullable: false),
    //                    BeginDate = c.DateTime(),
    //                    SentDate = c.DateTime(),
    //                    AdditionalInfo = c.String(),
    //                    UserId = c.String(maxLength: 128),
    //                    AddressId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Addresses", t => t.AddressId, cascadeDelete: true)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId)
    //            .Index(t => t.UserId)
    //            .Index(t => t.AddressId);
            
    //        CreateTable(
    //            "dbo.OrderedProducts",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Quantity = c.Int(nullable: false),
    //                    Price = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    ProductId = c.Guid(nullable: false),
    //                    OrderId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
    //            .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
    //            .Index(t => t.ProductId)
    //            .Index(t => t.OrderId);
            
    //        CreateTable(
    //            "dbo.Products",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Name = c.String(),
    //                    Publisher = c.String(),
    //                    Description = c.String(),
    //                    ReqMinimal = c.String(),
    //                    ReqRecommended = c.String(),
    //                    Released = c.DateTime(nullable: false),
    //                    Language = c.String(),
    //                    Rating = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    BasePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
    //                    DiscountPrice = c.Decimal(precision: 18, scale: 2),
    //                    CoverLarge = c.String(),
    //                    CoverList = c.String(),
    //                    CoverThumb = c.String(),
    //                    Remaining = c.Int(nullable: false),
    //                    CollectorEdition = c.Boolean(nullable: false),
    //                    GenreId = c.Guid(nullable: false),
    //                    PlatformId = c.Guid(nullable: false),
    //                    PegiRatingId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.GameGenres", t => t.GenreId, cascadeDelete: true)
    //            .ForeignKey("dbo.PegiRatings", t => t.PegiRatingId, cascadeDelete: true)
    //            .ForeignKey("dbo.ProductCategories", t => t.PlatformId, cascadeDelete: true)
    //            .Index(t => t.GenreId)
    //            .Index(t => t.PlatformId)
    //            .Index(t => t.PegiRatingId);
            
    //        CreateTable(
    //            "dbo.Comments",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Title = c.String(),
    //                    Content = c.String(),
    //                    Added = c.DateTime(nullable: false),
    //                    UserId = c.String(maxLength: 128),
    //                    ProductId = c.Guid(nullable: false),
    //                    ArticleId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId)
    //            .ForeignKey("dbo.Articles", t => t.ArticleId, cascadeDelete: true)
    //            .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
    //            .Index(t => t.UserId)
    //            .Index(t => t.ProductId)
    //            .Index(t => t.ArticleId);
            
    //        CreateTable(
    //            "dbo.Articles",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Title = c.String(),
    //                    ShortInfo = c.String(),
    //                    Content = c.String(),
    //                    Added = c.DateTime(nullable: false),
    //                    AuthorId = c.String(maxLength: 128),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.AuthorId)
    //            .Index(t => t.AuthorId);
            
    //        CreateTable(
    //            "dbo.AspNetUsers",
    //            c => new
    //                {
    //                    Id = c.String(nullable: false, maxLength: 128),
    //                    Email = c.String(maxLength: 256),
    //                    EmailConfirmed = c.Boolean(nullable: false),
    //                    PasswordHash = c.String(),
    //                    SecurityStamp = c.String(),
    //                    PhoneNumber = c.String(),
    //                    PhoneNumberConfirmed = c.Boolean(nullable: false),
    //                    TwoFactorEnabled = c.Boolean(nullable: false),
    //                    LockoutEndDateUtc = c.DateTime(),
    //                    LockoutEnabled = c.Boolean(nullable: false),
    //                    AccessFailedCount = c.Int(nullable: false),
    //                    UserName = c.String(nullable: false, maxLength: 256),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
    //        CreateTable(
    //            "dbo.AspNetUserClaims",
    //            c => new
    //                {
    //                    Id = c.Int(nullable: false, identity: true),
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                    ClaimType = c.String(),
    //                    ClaimValue = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "dbo.AspNetUserLogins",
    //            c => new
    //                {
    //                    LoginProvider = c.String(nullable: false, maxLength: 128),
    //                    ProviderKey = c.String(nullable: false, maxLength: 128),
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                })
    //            .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .Index(t => t.UserId);
            
    //        CreateTable(
    //            "dbo.Ratings",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Stars = c.Short(nullable: false),
    //                    UserId = c.String(maxLength: 128),
    //                    ProductId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId)
    //            .Index(t => t.UserId)
    //            .Index(t => t.ProductId);
            
    //        CreateTable(
    //            "dbo.AspNetUserRoles",
    //            c => new
    //                {
    //                    UserId = c.String(nullable: false, maxLength: 128),
    //                    RoleId = c.String(nullable: false, maxLength: 128),
    //                })
    //            .PrimaryKey(t => new { t.UserId, t.RoleId })
    //            .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
    //            .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
    //            .Index(t => t.UserId)
    //            .Index(t => t.RoleId);
            
    //        CreateTable(
    //            "dbo.GameGenres",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Name = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.PegiRatings",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Name = c.String(),
    //                    IconLink = c.String(),
    //                })
    //            .PrimaryKey(t => t.Id);
            
    //        CreateTable(
    //            "dbo.PegiContent",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Name = c.String(),
    //                    Description = c.String(),
    //                    IconLink = c.String(),
    //                    RatingId = c.Guid(nullable: false),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.PegiRatings", t => t.RatingId, cascadeDelete: true)
    //            .Index(t => t.RatingId);
            
    //        CreateTable(
    //            "dbo.ProductCategories",
    //            c => new
    //                {
    //                    Id = c.Guid(nullable: false, identity: true, defaultValueSql: "newsequentialid()"),
    //                    Name = c.String(),
    //                    NameShort = c.String(),
    //                    ParentId = c.Guid(),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .ForeignKey("dbo.ProductCategories", t => t.ParentId)
    //            .Index(t => t.ParentId);
            
    //        CreateTable(
    //            "dbo.AspNetRoles",
    //            c => new
    //                {
    //                    Id = c.String(nullable: false, maxLength: 128),
    //                    Name = c.String(nullable: false, maxLength: 256),
    //                })
    //            .PrimaryKey(t => t.Id)
    //            .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
    //    }
        
    //    public override void Down()
    //    {
    //        DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
    //        DropForeignKey("dbo.Products", "PlatformId", "dbo.ProductCategories");
    //        DropForeignKey("dbo.ProductCategories", "ParentId", "dbo.ProductCategories");
    //        DropForeignKey("dbo.Products", "PegiRatingId", "dbo.PegiRatings");
    //        DropForeignKey("dbo.PegiContent", "RatingId", "dbo.PegiRatings");
    //        DropForeignKey("dbo.OrderedProducts", "ProductId", "dbo.Products");
    //        DropForeignKey("dbo.Products", "GenreId", "dbo.GameGenres");
    //        DropForeignKey("dbo.Comments", "ProductId", "dbo.Products");
    //        DropForeignKey("dbo.Comments", "ArticleId", "dbo.Articles");
    //        DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Ratings", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Ratings", "ProductId", "dbo.Products");
    //        DropForeignKey("dbo.Orders", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Comments", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Articles", "AuthorId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.Addresses", "UserId", "dbo.AspNetUsers");
    //        DropForeignKey("dbo.OrderedProducts", "OrderId", "dbo.Orders");
    //        DropForeignKey("dbo.Orders", "AddressId", "dbo.Addresses");
    //        DropIndex("dbo.AspNetRoles", "RoleNameIndex");
    //        DropIndex("dbo.ProductCategories", new[] { "ParentId" });
    //        DropIndex("dbo.PegiContent", new[] { "RatingId" });
    //        DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
    //        DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
    //        DropIndex("dbo.Ratings", new[] { "ProductId" });
    //        DropIndex("dbo.Ratings", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
    //        DropIndex("dbo.AspNetUsers", "UserNameIndex");
    //        DropIndex("dbo.Articles", new[] { "AuthorId" });
    //        DropIndex("dbo.Comments", new[] { "ArticleId" });
    //        DropIndex("dbo.Comments", new[] { "ProductId" });
    //        DropIndex("dbo.Comments", new[] { "UserId" });
    //        DropIndex("dbo.Products", new[] { "PegiRatingId" });
    //        DropIndex("dbo.Products", new[] { "PlatformId" });
    //        DropIndex("dbo.Products", new[] { "GenreId" });
    //        DropIndex("dbo.OrderedProducts", new[] { "OrderId" });
    //        DropIndex("dbo.OrderedProducts", new[] { "ProductId" });
    //        DropIndex("dbo.Orders", new[] { "AddressId" });
    //        DropIndex("dbo.Orders", new[] { "UserId" });
    //        DropIndex("dbo.Addresses", new[] { "UserId" });
    //        DropTable("dbo.AspNetRoles");
    //        DropTable("dbo.ProductCategories");
    //        DropTable("dbo.PegiContent");
    //        DropTable("dbo.PegiRatings");
    //        DropTable("dbo.GameGenres");
    //        DropTable("dbo.AspNetUserRoles");
    //        DropTable("dbo.Ratings");
    //        DropTable("dbo.AspNetUserLogins");
    //        DropTable("dbo.AspNetUserClaims");
    //        DropTable("dbo.AspNetUsers");
    //        DropTable("dbo.Articles");
    //        DropTable("dbo.Comments");
    //        DropTable("dbo.Products");
    //        DropTable("dbo.OrderedProducts");
    //        DropTable("dbo.Orders");
    //        DropTable("dbo.Addresses");
    //    }
    //}
}

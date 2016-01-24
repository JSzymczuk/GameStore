using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    public class GameStoreDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Address>           Addresses           { get; set; }
        public DbSet<Article>           Articles            { get; set; }
        public DbSet<Comment>           Comments            { get; set; }
        public DbSet<GalleryImage>      GalleryImages       { get; set; }
        public DbSet<GameGenre>         GameGenres          { get; set; }
        public DbSet<Order>             Orders              { get; set; }
        public DbSet<OrderedProduct>    OrderedProducts     { get; set; }
        public DbSet<PegiContent>       PegiContent         { get; set; }
        public DbSet<PegiRating>        PegiRates           { get; set; }
        public DbSet<Product>           Products            { get; set; }
        public DbSet<ProductCategory>   ProductCategories   { get; set; }
        public DbSet<Rating>            Rates               { get; set; }

        public GameStoreDbContext() : base("DefaultConnection", throwIfV1Schema: false) { }

        public static GameStoreDbContext Create()
        {
            return new GameStoreDbContext();
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Publisher { get; set; }
        public String Description { get; set; }
        public String ReqMinimal { get; set; }
        public String ReqRecommended { get; set; }
        public DateTime ReleaseDate { get; set; }
        public String Language { get; set; }
        public Decimal Rating { get; set; }
        public Decimal BasePrice { get; set; }
        public Decimal? DiscountPrice { get; set; }
        public String CoverLarge { get; set; }
        public String CoverList { get; set; }
        public String CoverThumb { get; set; }
        public Int32 Remaining { get; set; }
        public Boolean CollectorEdition { get; set; }
        public Boolean IsDeleted { get; set; }

        public Guid GenreId { get; set; }
        public Guid PlatformId { get; set; }
        public Guid PegiRatingId { get; set; }

        [ForeignKey("GenreId")]
        public virtual GameGenre Genre { get; set; }
        [ForeignKey("PlatformId")]
        public virtual ProductCategory Platform { get; set; }
        [ForeignKey("PegiRatingId")]
        public virtual PegiRating PegiRating { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Rating> Rates { get; set; }
        public virtual ICollection<OrderedProduct> OrderedProducts { get; set; }
    }
}
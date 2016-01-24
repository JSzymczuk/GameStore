using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    public class Comment
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String Content { get; set; }
        public DateTime Added { get; set; }

        public String UserId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ArticleId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }

    public class Rating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Int16 Stars { get; set; }

        public string UserId { get; set; }
        public Guid ProductId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
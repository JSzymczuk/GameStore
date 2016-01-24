using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    [Table("PegiContent")]
    public class PegiContent
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String IconLink { get; set; }

        public virtual ICollection<PegiRating> Rates { get; set; }
    }

    public class PegiRating
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String IconLink { get; set; }
        public Int32 SortIndex { get; set; }

        public virtual ICollection<PegiContent> Content { get; set; }
        public virtual ICollection<Product> Products { get; set; }

        public PegiRating()
        {
            Content = new List<PegiContent>();
            Products = new List<Product>();
        }
    }
}
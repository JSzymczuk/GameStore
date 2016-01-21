using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    [Table("ProductCategories")]
    public class ProductCategory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String NameShort { get; set; }

        public Guid? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual ProductCategory Parent { get; set; }

        public virtual ICollection<ProductCategory> Children { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }

    public class GameGenre
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
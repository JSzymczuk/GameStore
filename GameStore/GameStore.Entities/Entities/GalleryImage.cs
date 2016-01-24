using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class GalleryImage
    {
        public Guid Id { get; set; }
        public String ImageLocation { get; set; }
        public String ImageThumb { get; set; }
        public Guid ProductId { get; set; }

        [Key, ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}

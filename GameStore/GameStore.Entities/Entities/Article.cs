using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Entities
{
    public class Article
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public String Title { get; set; }
        public String ShortInfo { get; set; }
        public String Image { get; set; }
        public String ImageThumb { get; set; }
        public String Content { get; set; }
        public DateTime Added { get; set; }

        public String AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual AppUser Author { get; set; }
    }
}

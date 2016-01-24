using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    public class OrderedProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Int32 Quantity { get; set; }
        public Decimal Price { get; set; }

        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }

    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public DateTime OrderDate { get; set; }
        public Boolean Paid { get; set; }
        public Boolean IsDeleted { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? SentDate { get; set; }
        public String AdditionalInfo { get; set; }

        public String UserId { get; set; }
        public Guid AddressId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }
        [ForeignKey("AddressId")]
        public virtual Address Address { get; set; }

        public virtual ICollection<OrderedProduct> Products { get; set; }
    }
}
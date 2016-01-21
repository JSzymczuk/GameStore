using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace GameStore.Entities
{
    public enum Region
    {
        [Description("dolnośląskie")]
        Dolnoslaskie,
        [Description("kujawsko-pomorskie")]
        KujawskoPomorskie,
        [Description("lubelskie")]
        Lubelskie,
        [Description("lubuskie")]
        Lubuskie,
        [Description("łódzkie")]
        Lodzkie,
        [Description("małopolskie")]
        Malopolskie,
        [Description("mazowieckie")]
        Mazowieckie,
        [Description("opolskie")]
        Opolskie,
        [Description("podkarpackie")]
        Podkarpackie,
        [Description("podlaskie")]
        Podlaskie,
        [Description("pomorskie")]
        Pomorskie,
        [Description("śląskie")]
        Slaskie,
        [Description("świętokrzyskie")]
        Swietokrzyskie,
        [Description("warmińsko-mazurskie")]
        WarminskoMazurskie,
        [Description("wielkopolskie")]
        Wielkopolskie,
        [Description("zachodniopomorskie")]
        Zachodniopomorskie
    }

    [Table("Addresses")]
    public class Address
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Region Region { get; set; }
        public String City { get; set; }
        public String Street { get; set; }
        public String House { get; set; }
        public String Local { get; set; }
        public String PostalCode { get; set; }
        public String Post { get; set; }
        public String AdditionalInfo { get; set; }
        public Boolean IsDeleted { get; set; }

        public String UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual AppUser User { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.ViewModels
{
    public class AddressViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }

        [Display(Name = "Województwo")]
        [Required(ErrorMessage = "To pole nie może być puste")]
        public Region Region { get; set; }

        [Display(Name = "Miejscowość")]
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string City { get; set; }

        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Display(Name = "Numer domu")]
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string House { get; set; }

        [Display(Name = "Numer mieszkania")]
        public string Local { get; set; }

        [Display(Name = "Kod pocztowy")]
        [Required(ErrorMessage = "To pole nie może być puste")]
        [RegularExpression(@"([0-9]{2})\-?[-]?([0-9]{3})", ErrorMessage = "Zły format danych")]
        public string PostalCode { get; set; }

        [Display(Name = "Poczta")]
        [Required(ErrorMessage = "To pole nie może być puste")]
        public string Post { get; set; }

        [Display(Name = "Dodatkowe informacje")]
        public string AdditionalInfo { get; set; }

        public bool IsDeleted { get; set; }
    }
}
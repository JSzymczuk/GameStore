using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.ViewModels
{
    public class CreateImageViewModel
    {
        public Guid ProductId { get; set; }
        
        [Display(Name = "Prześlij nowy obraz do galerii")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [ValidatePhoto(ErrorMessage = "Obraz musi mieć rozszerzenie .jpg, .jpeg lub .png i rozmiar niewiększy niż 3MB.")]
        public HttpPostedFileBase Image { get; set; }

        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
    }

    public class ProductGalleryViewModel
    {
        public Guid ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductCategory { get; set; }
    }

    public class ManageGalleryViewModel
    {
        public ICollection<GalleryImage> Images { get; set; }

        public Guid ProductId { get; set; }

        [Display(Name = "Prześlij nowy obraz do galerii")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [ValidatePhoto(ErrorMessage = "Obraz musi mieć rozszerzenie .jpg, .jpeg lub .png i rozmiar niewiększy niż 3MB.")]
        public HttpPostedFileBase Image { get; set; }
    }
}
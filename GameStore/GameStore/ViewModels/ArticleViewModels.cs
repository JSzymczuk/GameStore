using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.ViewModels
{
    public class ArticleViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string ShortInfo { get; set; }
        public string Content { get; set; }
        public string DateString { get; set; }
        public string Image { get; set; }
        public string ImageThumb { get; set; }
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
    }

    public class ArticleCreateViewModel
    {
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Tytuł artykułu")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Streszczenie")]
        public string ShortInfo { get; set; }

        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Display(Name = "Treść")]
        public string Content { get; set; }

        [Display(Name = "Obraz")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [ValidatePhoto(ErrorMessage = "Obraz musi mieć rozszerzenie .jpg, .jpeg lub .png i rozmiar niewiększy niż 3MB.")]
        public HttpPostedFileBase Image { get; set; }

        public string ImageUrl { get; set; }
        public string ImageThumbUrl { get; set; }
        public DateTime DateAdded { get; set; }
        public string AuthorId { get; set; }
        public Guid Id { get; set; }
    }
}
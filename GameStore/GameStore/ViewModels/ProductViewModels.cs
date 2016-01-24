using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.ViewModels
{
    public enum ProductSortType
    {
        [Description("Nazwa rosnąco")]
        NameAscending,
		[Description("Nazwa malejąco")]
        NameDescending,
		[Description("Cena rosnąco")]
        PriceAscending,
		[Description("Cena malejąco")]
        PriceDescending,
		[Description("Ocena rosnąco")]
        RatingAscending,
		[Description("Ocena malejąco")]
        RatingDescending,
		[Description("Data premiery rosnąco")]
        ReleaseAscending,
		[Description("Data premiery malejąco")]
        ReleaseDescending
    }

    public class ProductFilterModel
    {
        public String               SearchKeyword       { get; set; }
        public ProductSortType      ProductSortType     { get; set; }
        public SelectList           ProductSortTypes    { get; set; }
        public List<SelectListItem> Platforms           { get; set; }
        public List<SelectListItem> Genres              { get; set; }
        public List<SelectListItem> PegiRatings         { get; set; }
        public Boolean              ListDisplayMode     { get; set; }

        public ProductFilterModel()
        {
            Genres = new List<SelectListItem>();
            Platforms = new List<SelectListItem>();
            PegiRatings = new List<SelectListItem>();
        }
    }

    public class ProductDetailsModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string CoverLarge { get; set; }
        public decimal Price { get; set; }
        public int BasePriceZl { get; set; }
        public string BasePriceGr { get; set; }
        public bool IsDiscounted { get; set; }
        public int DiscountPriceZl { get; set; }
        public string DiscountPriceGr { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateString { get; set; }
        public string Language { get; set; }
        public decimal Rating { get; set; }
        public int CommentsCount { get; set; }
        public int VotesCount { get; set; }
        public int DiscountPercentage { get; set; }
        public bool Available { get; set; }
        public string GenreName { get; set; }
        public Guid GenreId { get; set; }
        public string PlatformName { get; set; }
        public string ReqMinimal { get; set; }
        public string ReqRecommended { get; set; }
        public Guid PlatformId { get; set; }

        public Guid PegiID { get; set; }
    }

    public class ProductListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public string CoverList { get; set; }
        public decimal Price { get; set; }
        public int BasePriceZl { get; set; }
        public string BasePriceGr { get; set; }
        public bool IsDiscounted { get; set; }
        public int DiscountPriceZl { get; set; }
        public string DiscountPriceGr { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string ReleaseDateString { get; set; }
        public string Language { get; set; }
        public decimal Rating { get; set; }
        public int CommentsCount { get; set; }
        public int VotesCount { get; set; }
        public int DiscountPercentage { get; set; }
        public bool Available { get; set; }
        public string GenreName { get; set; }
        public Guid GenreId { get; set; }
        public string PlatformName { get; set; }
        public int Remaining { get; set; }
        
        public Guid PlatformId { get; set; }

        public Guid PegiID { get; set; }
    }

    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa produktu")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public String Name { get; set; }

        [Display(Name = "Wydawca")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public String Publisher { get; set; }

        [Display(Name = "Opis produktu")]
        public String Description { get; set; }

        [Display(Name = "Minimalne wymagania")]
        public String ReqMinimal { get; set; }

        [Display(Name = "Zalecane wymagania")]
        public String ReqRecommended { get; set; }

        [Display(Name = "Data wydania")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Język")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        public String Language { get; set; }

        [Display(Name = "Cena")]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [Range(0.01, 1000000, ErrorMessage = "Wartość musi być liczbą z przedziału 0.01 i 1 000 000")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Niepoprawny format.")]
        public Decimal BasePrice { get; set; }

        [Display(Name = "Cena po zniżce")]
        [Range(0.01, 1000000, ErrorMessage = "Wartość musi być liczbą z przedziału 0.01 i 1 000 000")]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Niepoprawny format.")]
        public Decimal? DiscountPrice { get; set; }

        [Display(Name = "Okładka")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "Pole jest wymagane.")]
        [ValidatePhoto(ErrorMessage = "Okładka musi mieć rozszerzenie .jpg, .jpeg lub .png i rozmiar niewiększy niż 3MB.")]
        public HttpPostedFileBase CoverLarge { get; set; }
        public String Url { get; set; }

        public String CoverList { get; set; }
        public String CoverThumb { get; set; }

        [Display(Name = "Liczba sztuk")]
        [Range(0, 1000000, ErrorMessage = "Wartość musi być liczbą z przedziału 0.01 i 1 000 000")]
        public Int32 Remaining { get; set; }

        [Display(Name = "Ocena")]
        public decimal Rating { get; set; }

        [Display(Name = "Edycja kolekcjonerska")]
        public Boolean CollectorEdition { get; set; }

        [Display(Name = "Gatunek")]
        public Guid GenreId { get; set; }
        public SelectList GenreSelectList { get; set; }

        [Display(Name = "Platforma")]
        public Guid PlatformId { get; set; }
        public SelectList PlatformSelectList { get; set; }

        [Display(Name = "Kategoria wiekowe")]
        public Guid PegiRatingId { get; set; }
        public SelectList PegiSelectList { get; set; }
    }

    public class IncreaseProductViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Kategoria")]
        public string ProductCategory { get; set; }

        [Display(Name = "Pozostało")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public int Remaining { get; set; }
    }

    public class CategoryViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa kategorii")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Name { get; set; }

        [Display(Name = "Skrót")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string NameShort { get; set; }

        public Guid? ParentId { get; set; }
        public SelectList Categories { get; set; }
    }

    public class CategoryListItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameShort { get; set; }
        public Guid? ParentId { get; set; }
        public string ParentName { get; set; }
        public bool CanBeDeleted { get; set; }
    }

    public class GenreViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Gatunek")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Name { get; set; }
    }

    public class ValidatePhotoAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            const int MAX_CONTENT_LENGTH = 3145728;

            string[] AllowedFileExtensions = new string[] { ".jpg", ".png", ".jpeg" };
            var file = value as HttpPostedFileBase;

            if (file == null) 
            { 
                return false; 
            }

            var extension = file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();

            if (file == null)
            {
                return false;
            }
            else if (!AllowedFileExtensions.Contains(extension))
            {
                ErrorMessage = "Akceptowalne są tylko pliki .jpg, .jpeg i .png.";
                return false;
            }
            else if (file.ContentLength > MAX_CONTENT_LENGTH)
            {
                ErrorMessage = "Rozmiar zdjęcia musi być niewiększy niż 3MB";
                return false;
            }
            else
            {
                return true;
            }
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(name);
        }
    }
}
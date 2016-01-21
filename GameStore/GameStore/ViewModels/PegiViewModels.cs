using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.ViewModels
{
    public class PegiContentViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Name { get; set; }

        [Display(Name = "Opis")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Description { get; set; }

        [Display(Name = "Obrazek")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string IconLink { get; set; }

        public bool CanBeDeleted { get; set; }
    }

    public class PegiRatingViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Name { get; set; }

        [Display(Name = "Obrazek")]
        [DataType(DataType.Upload)]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string IconLink { get; set; }

        [Display(Name = "Zawartość")]
        public List<SelectListItem> Content { get; set; }
    }

    public class PegiRatingListItem
    {
        public Guid Id { get; set; }

        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Name { get; set; }

        [Display(Name = "Obrazek")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string IconLink { get; set; }

        [Display(Name = "Zawartość")]
        public List<PegiContentViewModel> Content { get; set; }

        public bool CanBeDeleted { get; set; }
    }

    public class PegiIndexViewModel
    {
        public List<PegiRatingListItem> Rates { get; set; }
        public List<PegiContentViewModel> Content { get; set; }
    }
}
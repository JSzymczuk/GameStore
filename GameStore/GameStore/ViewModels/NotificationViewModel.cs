using System.ComponentModel.DataAnnotations;

namespace GameStore.ViewModels
{
    public class NotificationViewModel
    {
        [Display(Name = "Temat")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [Required(ErrorMessage = "To pole nie może być puste.")]
        public string Body { get; set; }
    }
}
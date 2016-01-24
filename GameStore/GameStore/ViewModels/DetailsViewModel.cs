using GameStore.Entities;
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class DetailsViewModel
    {
        public ProductDetailsModel Product { get; set; }
        public List<Comment> Comments { get; set; }
        public AddCommentViewModel AddComment { get; set; }
    }
}
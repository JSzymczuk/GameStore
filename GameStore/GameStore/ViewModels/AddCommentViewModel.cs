using System;

namespace GameStore.ViewModels
{
    public class AddCommentViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public string UserId { get; set; }
        public Guid ProductId { get; set; }
    }
}
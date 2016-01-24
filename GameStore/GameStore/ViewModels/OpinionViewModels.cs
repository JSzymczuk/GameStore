using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.ViewModels
{
    public class AddCommentViewModel
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public Guid ProductId { get; set; }
    }

    public class CommentListItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string DateString { get; set; }
        public Guid ProductId { get; set; }
    }

    public class RateViewModel
    {
        public int VotesCount { get; set; }
        public Guid ProductId { get; set; }
        public string Rating { get; set; }
        public string Message { get; set; }
    }
}
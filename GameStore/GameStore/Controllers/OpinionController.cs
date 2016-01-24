using GameStore.Entities;
using GameStore.Managers;
using GameStore.Helpers;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class OpinionController : Controller
    {
        public OpinionManager OpinionManager { get; private set; }
        public ProductManager ProductManager { get; private set; }

        public OpinionController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            OpinionManager = new OpinionManager(context);
            ProductManager = new ProductManager(context);
        }

        [HttpGet]
        public PartialViewResult Comments(Guid productId)
        {
            var list = new List<CommentListItem>();
            foreach (var comment in OpinionManager.Comments.Where(c => c.ProductId == productId).ToList())
            {
                list.Add(new CommentListItem 
                { 
                    Content = comment.Content,  
                    Id = comment.Id,
                    DateString = GeneralHelper.ToDisplayableDate(comment.Added),
                    ProductId = productId, 
                    Title = comment.Title, 
                    UserId = comment.UserId, 
                    UserName = comment.User.UserName 
                });
            }
            return PartialView("_Comments", list);
        }

        [HttpGet]
        public PartialViewResult AddComment(Guid productId)
        {
            return PartialView("_AddComment", new AddCommentViewModel 
            { 
                ProductId = productId, 
                UserId = AccountHelper.GetLoggedUserId() 
            });
        }

        [HttpPost]
        public ActionResult AddComment(AddCommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                OpinionManager.CreateComment(new Comment
                {
                    Added = DateTime.Now,
                    Content = model.Content,
                    ProductId = model.ProductId,
                    Title = model.Title,
                    UserId = AccountHelper.GetLoggedUserId()
                });
                OpinionManager.Save();
            }
            return RedirectToAction("Details", "Product", new { Id = model.ProductId });
        }

        [HttpGet]
        public PartialViewResult DeleteComment(Guid id, Guid productId)
        {
            OpinionManager.DeleteComment(id);
            OpinionManager.Save();

            return Comments(productId);
        }

        [HttpGet]
        public PartialViewResult Rate(Guid id, short? rate = null)
        {
            string message = null;
            if (rate.HasValue && rate > 0 && rate < 6)
            {
                string userId = AccountHelper.GetLoggedUserId();
                if (userId != null)
                {
                    if (!OpinionManager.IsRated(userId, id))
                    {
                        OpinionManager.Rate(userId, id, rate.Value);
                        OpinionManager.Save();

                        message = "Twój głos został oddany.";
                    }
                    else
                    {
                        message = "Już oceniłeś ten produkt.";
                    }
                }
                else
                {
                    message = "Zaloguj się, aby oddać głos.";
                }
            }

            var product = ProductManager.FindById(id);
            return PartialView("_Rate", new RateViewModel 
            { 
                ProductId = id,  
                Message = message, 
                Rating = String.Format("{0:0.0}", product.Rating), 
                VotesCount = product.Rates.Count  
            });
        }
    }
}
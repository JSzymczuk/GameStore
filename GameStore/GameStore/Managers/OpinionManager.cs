using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class OpinionManager : Manager
    {
        public IQueryable<Rating> Votes
        {
            get { return Context.Rates; }
        }

        public IQueryable<Comment> Comments 
        {
            get { return Context.Comments; }
        }

        public OpinionManager(GameStoreDbContext context) : base(context) { }

        public void CreateComment(Comment comment)
        {
            if (string.IsNullOrEmpty(comment.UserId) || string.IsNullOrEmpty(comment.Content)) { return; }
            comment.User = Context.Users.First(u => u.Id == comment.UserId);
            if (comment.User == null) { return; }
            comment.Added = DateTime.Now;   
            Context.Comments.Add(comment);
        }

        public void DeleteComment(Guid id)
        {
            Comment comment = Context.Comments.Find(id);
            Context.Comments.Remove(comment);
        }

        public void Rate(string userId, Guid productId, short rate)
        {
            Product recipe = Context.Products.First(r => r.Id == productId);
            if (recipe == null) { return; }
            int votes = recipe.Rates.Count;
            recipe.Rating = (recipe.Rating * votes + rate) / (votes + 1);
            Context.Rates.Add(new Rating 
            { 
                UserId = userId,
                ProductId = productId, 
                Stars = rate 
            });
            Context.Entry(recipe).State = EntityState.Modified;
        }

        public bool IsRated(string userId, Guid productId)
        {
            return Votes.Any(v => v.UserId == userId && v.ProductId == productId);
        }
    }
}
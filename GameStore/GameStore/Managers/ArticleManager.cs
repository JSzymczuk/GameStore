using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class ArticleManager : Manager
    {
        public ArticleManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<Article> Articles
        {
            get { return Context.Articles; }
        }

        public Article FindById(Guid id)
        {
            try { return Context.Articles.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public void Create(Article article)
        {
            if (article != null)
            {
                Context.Articles.Add(article);
            }
        }

        public void Update(Article article)
        {
            if (article != null)
            {
                Context.Entry(article).State = EntityState.Modified;
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                Article article = Context.Articles.Find(id);
                Context.Articles.Remove(article);
                return true;
            }
            catch (Exception) 
            { 
                return false;
            }
        }
    }
}
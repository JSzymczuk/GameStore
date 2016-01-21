using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class PegiManager : Manager
    {
        public PegiManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<PegiContent> PegiContent
        {
            get { return Context.PegiContent; }
        }

        public IQueryable<PegiRating> PegiRates
        {
            get { return Context.PegiRates; }
        }

        public PegiContent FindContentById(Guid id)
        {
            try { return Context.PegiContent.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public PegiRating FindRatingById(Guid id)
        {
            try 
            { 
                return Context.PegiRates.First(c => c.Id == id); 
            }
            catch (Exception) { return null; }
        }

        public void CreateContent(PegiContent pegi)
        {
            if (pegi != null)
            {
                Context.PegiContent.Add(pegi);
            }
        }

        public void UpdateContent(PegiContent pegi)
        {
            if (pegi != null)
            {
                Context.Entry(pegi).State = EntityState.Modified;
            }
        }

        public bool DeleteContent(Guid id)
        {
            try
            {
                PegiContent pegi = Context.PegiContent.Find(id);
                Context.PegiContent.Remove(pegi);
                return true;
            }
            catch (Exception) 
            { 
                return false;
            }
        }

        public void CreateRating(PegiRating pegi)
        {
            if (pegi != null)
            {
                Context.PegiRates.Add(pegi);
            }
        }

        public void UpdateRating(PegiRating pegi)
        {
            if (pegi != null)
            {
                Context.Entry(pegi).State = EntityState.Modified;
            }
        }

        public bool DeleteRating(Guid id)
        {
            try
            {
                PegiRating pegi = Context.PegiRates.Find(id);
                Context.PegiRates.Remove(pegi);
                return true;
            }
            catch (Exception) 
            { 
                return false;
            }
        }

        /*
        private void AddToRating

        public void AddToRating(PegiRating rating, PegiContent content)
        {
            if (content != null && rating != null)
            {
                rating.Content.Add(content);
                content.Rates.Add(rating);
            }
        }*/

    }
}
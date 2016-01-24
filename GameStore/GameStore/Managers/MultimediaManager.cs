using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class MultimediaManager : Manager
    {
        public MultimediaManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<GalleryImage> GalleryImages
        {
            get { return Context.GalleryImages; }
        }

        public GalleryImage FindById(Guid id)
        {
            try { return Context.GalleryImages.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public void Create(GalleryImage image)
        {
            if (image != null)
            {
                Context.GalleryImages.Add(image);
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                GalleryImage image = Context.GalleryImages.Find(id);
                Context.GalleryImages.Remove(image);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
using GameStore.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace GameStore.Managers
{
    public class ProductManager : Manager
    {
        public ProductManager(GameStoreDbContext context) : base(context) { }

        public IQueryable<Product> Products
        {
            get { return Context.Products; }
        }

        public IQueryable<ProductCategory> ProductCategories
        {
            get { return Context.ProductCategories; }
        }

        public IQueryable<GameGenre> Genres
        {
            get { return Context.GameGenres; }
        }

        public Product FindById(Guid id)
        {
            try { return Context.Products.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public IQueryable<Product> FindByName(string name)
        {
            return Context.Products.Where(c => c.Name.Contains(name));
        }

        public void Create(Product product)
        {
            if (product != null)
            {
                Context.Products.Add(product);
            }
        }

        public bool Delete(Guid id)
        {
            try
            {
                Product product = Context.Products.Find(id);
                product.IsDeleted = true;
                Context.Entry(product).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(Product product)
        {
            if (product != null)
            {
                Context.Entry(product).State = EntityState.Modified;
            }
        }
        
        public void CreateCategory(ProductCategory category)
        {
            if (category != null)
            {
                Context.ProductCategories.Add(category);
            }
        }

        public void UpdateCategory(ProductCategory category)
        {
            if (category != null)
            {
                Context.Entry(category).State = EntityState.Modified;
            }
        }

        public bool DeleteCategory(Guid id)
        {
            try
            {
                ProductCategory category = Context.ProductCategories.Find(id);
                Context.ProductCategories.Remove(category);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductCategory FindCategoryById(Guid? id)
        {
            try { return Context.ProductCategories.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }

        public void CreateGenre(GameGenre genre)
        {
            if (genre != null)
            {
                Context.GameGenres.Add(genre);
            }
        }

        public void UpdateGenre(GameGenre genre)
        {
            if (genre != null)
            {
                Context.Entry(genre).State = EntityState.Modified;
            }
        }

        public bool DeleteGenre(Guid id)
        {
            try
            {
                GameGenre genre = Context.GameGenres.Find(id);
                Context.GameGenres.Remove(genre);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GameGenre FindGenreById(Guid? id)
        {
            try { return Context.GameGenres.First(c => c.Id == id); }
            catch (Exception) { return null; }
        }
    }
}
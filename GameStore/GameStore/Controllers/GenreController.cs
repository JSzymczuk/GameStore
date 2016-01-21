using GameStore.Entities;
using GameStore.Managers;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class GenreController : Controller
    {
        public ProductManager ProductManager { get; private set; } 

        public GenreController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
        }

        public GenreController(ProductManager productManager)
        {
            ProductManager = productManager;
        }

        public ActionResult Index()
        {
            var model = new List<GenreViewModel>();
            var categories = ProductManager.Genres.ToList();
            foreach (var ctgr in categories)
            {
                model.Add(new GenreViewModel
                {
                     Id = ctgr.Id, 
                     Name = ctgr.Name
                });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(GenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var genre = new GameGenre
                {
                     Name = model.Name
                };
                ProductManager.CreateGenre(genre);
                ProductManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = ProductManager.FindGenreById(id.Value);
                var model = new GenreViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name
                };
                return View("Update", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(GenreViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new GameGenre
                {
                    Id = model.Id,
                    Name = model.Name
                };
                ProductManager.UpdateGenre(category);
                ProductManager.Save();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(Guid? id)
        {
            if (id.HasValue)
            {
                ProductManager.DeleteGenre(id.Value);
                ProductManager.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
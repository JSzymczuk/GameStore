using GameStore.Managers;
using GameStore.Models;
using GameStore.ViewModels;
using GameStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameStore.Entities;

namespace GameStore.Controllers
{
    public class CategoryController : Controller
    {
        public ProductManager ProductManager { get; private set; } 

        public CategoryController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
        }

        public CategoryController(ProductManager productManager)
        {
            ProductManager = productManager;
        }

        public ActionResult Index()
        {
            var model = new List<CategoryListItem>();
            var categories = ProductManager.ProductCategories.ToList();
            foreach (var ctgr in categories)
            {
                model.Add(new CategoryListItem
                {
                     Id = ctgr.Id, 
                     Name = ctgr.Name,
                     NameShort = ctgr.NameShort,
                     ParentId = ctgr.ParentId,
                     ParentName = ctgr.Parent == null ? string.Empty : ctgr.Parent.Name,
                     CanBeDeleted = ctgr.Children.Count == 0
                });
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new CategoryViewModel
            {
                 Categories = ProductManager.GetCategoriesSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new ProductCategory
                {
                     Name = model.Name,
                     NameShort = model.NameShort,
                     ParentId = model.ParentId
                };
                ProductManager.CreateCategory(category);
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
                var entity = ProductManager.FindCategoryById(id.Value);
                var model = new CategoryViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    NameShort = entity.NameShort,
                    ParentId = entity.ParentId,
                    Categories = ProductManager.GetCategoriesSelectList()
                };
                return View("Update", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = new ProductCategory
                {
                    Id = model.Id,
                    Name = model.Name,
                    NameShort = model.NameShort,
                    ParentId = model.ParentId
                };
                ProductManager.UpdateCategory(category);
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
                ProductManager.DeleteCategory(id.Value);
                ProductManager.Save();
            }
            return RedirectToAction("Index");
        }
    }
}
using GameStore.Entities;
using GameStore.Managers;
using GameStore.ViewModels;
using GameStore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class ProductController : Controller
    {
        public ProductManager ProductManager { get; private set; } 
        public PegiManager PegiManager { get; private set; } 

        public ProductController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
            PegiManager = new PegiManager(context);
        }

        public ProductController(ProductManager productManager, PegiManager pegiManager)
        {
            ProductManager = productManager;
            PegiManager = pegiManager;
        }

        public ActionResult Index()
        {
            var model = new List<ProductListItem>();
            foreach (var p in ProductManager.Products.ToList())
            {
                if (!p.IsDeleted)
                {
                    model.Add(p.ToListItem());
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel model = new ProductViewModel
            {
                ReleaseDate = DateTime.Now,
                GenreSelectList = ProductManager.GetGenreSelectList(), 
                PlatformSelectList = ProductManager.GetCategoriesSelectList(), 
                PegiSelectList = PegiManager.GetPegiSelectList()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                //dodaj domyslna okladke w razie nulla
                UploadFile(model.CoverLarge);

                ProductManager.Create(model.ToProduct());
                ProductManager.Save();
                
                return RedirectToAction("Index");
            }

            model.GenreSelectList = ProductManager.GetGenreSelectList(model.GenreId);
            model.PlatformSelectList = ProductManager.GetCategoriesSelectList(model.PlatformId);
            model.PegiSelectList = PegiManager.GetPegiSelectList(model.PegiRatingId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Update(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = ProductManager.FindById(id.Value);
                var model = entity.ToViewModel();

                model.GenreSelectList = ProductManager.GetGenreSelectList(model.GenreId);
                model.PlatformSelectList = ProductManager.GetCategoriesSelectList(model.PlatformId);
                model.PegiSelectList = PegiManager.GetPegiSelectList(model.PegiRatingId);

                return View("Update", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Update(ProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.CoverLarge == null)
                {
                    ProductManager.Update(model.ToProduct());
                }
                else
                {
                    ProductManager.Update(model.ToProduct(Url.Content("/Images/" + model.CoverLarge.FileName)));
                }
                ProductManager.Save();

                return RedirectToAction("Index");
            }

            model.GenreSelectList = ProductManager.GetGenreSelectList(model.GenreId);
            model.PlatformSelectList = ProductManager.GetCategoriesSelectList(model.PlatformId);
            model.PegiSelectList = PegiManager.GetPegiSelectList(model.PegiRatingId);

            return View(model);
        }

        [HttpGet]
        public ActionResult Increase(Guid? id)
        {
            if (id.HasValue)
            {
                var entity = ProductManager.FindById(id.Value);
                var model = new IncreaseProductViewModel
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    ProductCategory = entity.Platform.Name,
                    Remaining = entity.Remaining
                };
                return View("Increase", model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Increase(IncreaseProductViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entity = ProductManager.FindById(model.Id);
                entity.Remaining = model.Remaining;
                ProductManager.Update(entity);
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
                ProductManager.Delete(id.Value);
                ProductManager.Save();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public PartialViewResult InitializeFilter(string keyword, Guid? platformId, Guid? genreId)
        {
            var filter = new ProductFilterModel();

            filter.SearchKeyword = keyword ?? string.Empty;
            filter.ProductSortType = ProductSortType.NameAscending;
            filter.ProductSortTypes = GeneralHelper.SelectListForEnum(typeof(ProductSortType));

            foreach (var platf in ProductManager.ProductCategories.OrderBy(c => c.Name))
            {
                filter.Platforms.Add(new SelectListItem 
                { 
                    Value = platf.Id.ToString(), 
                    Text = platf.NameShort ?? platf.Name,
                    Selected = platf.Id == platformId 
                });
            }
            foreach (var genre in ProductManager.Genres.OrderBy(g => g.Name))
            {
                filter.Genres.Add(new SelectListItem
                {
                    Value = genre.Id.ToString(),
                    Text = genre.Name,
                    Selected = genre.Id == genreId
                });
            }
            foreach (var pegi in PegiManager.PegiRates.OrderBy(p => p.Name))
            {
                filter.PegiRatings.Add(new SelectListItem
                {
                    Value = pegi.Id.ToString(),
                    Text = pegi.Name,
                    Selected = false
                });
            }

            return PartialView("_FilterAndSort", filter);
        }

        [HttpPost]
        public PartialViewResult Search(ProductFilterModel filter)
        {
            var products = new List<ProductListItem>();
            bool isSelectedAnyPlatform = filter.Platforms.Any(p => p.Selected);
            bool isSelectedAnyGenre = filter.Genres.Any(g => g.Selected);
            bool isSelectedAnyPegi = filter.PegiRatings.Any(p => p.Selected);

            foreach (var prod in ProductManager.Products.ToList())
            {
                if (!prod.IsDeleted && prod.Name.Contains(filter.SearchKeyword))
                {
                    if (isSelectedAnyGenre && filter.Genres.All(g => 
                        g.Value != prod.GenreId.ToString())) { continue; }
                    if (isSelectedAnyPlatform && filter.Platforms.All(p => 
                        p.Value != prod.PlatformId.ToString())) { continue; }
                    if (isSelectedAnyPegi && filter.PegiRatings.All(p => 
                        p.Value != prod.PegiRatingId.ToString())) { continue; }
                    products.Add(prod.ToListItem());
                }
            }

            return PartialView("_SearchProductsResult", products.SortBy(filter.ProductSortType));
        }

        private bool UploadFile(HttpPostedFileBase image)
        {
            var path = Server.MapPath("/Images/" + image.FileName);
            try
            {
                image.SaveAs(path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
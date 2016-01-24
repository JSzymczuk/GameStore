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
        public OpinionManager OpinionManager { get; private set; }
        public ProductManager ProductManager { get; private set; } 
        public PegiManager PegiManager { get; private set; } 

        public ProductController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
            PegiManager = new PegiManager(context);
            OpinionManager = new OpinionManager(context);
        }

        public ProductController(ProductManager productManager, PegiManager pegiManager)
        {
            ProductManager = productManager;
            PegiManager = pegiManager;
        }

        public ActionResult Index(string keyword = "", Guid? platformId = null, Guid? genreId = null, Guid? pegiId = null)
        {
            ViewBag.Keyword = keyword;
            var filter = CreateFilter(keyword, platformId, genreId, pegiId);
            ViewBag.Filter = filter;
            var model = SearchProducts(filter);
            return View(model);
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
            return RedirectToAction("ProductDetails", model.ProductId);
        }

        public ActionResult DeleteComment(Guid Id, Guid ProductId)
        {
            OpinionManager.DeleteComment(Id);
            OpinionManager.Save();

            return RedirectToAction("ProductDetails", ProductId);
        }

        public ActionResult ProductDetails(Guid Id)
        {
            var product = ProductManager.Products.FirstOrDefault(x => x.Id == Id);
            var productViewModel = new ProductDetailsModel
            {
                Id = product.Id,
                Name = product.Name,
                Publisher = product.Publisher,
                Description = product.Description,
                CoverList = product.CoverList,
                ReleaseDate = product.ReleaseDate,
                Language = product.Language,
                DiscountPriceZl = product.DiscountPrice.HasValue ? (int)product.DiscountPrice.Value : (int)product.BasePrice,
                Rating = product.Rating,
                BasePriceZl = (int)product.BasePrice,
                IsDiscounted = product.DiscountPrice.HasValue,
                Price = product.DiscountPrice.HasValue ? product.DiscountPrice.Value : product.BasePrice,
                Available = product.Remaining > 0,
                GenreName = product.Genre.Name,
                GenreId = product.GenreId,
                PlatformName = product.Platform.Name,
                PlatformId = product.PlatformId,
                CommentsCount = product.Comments.Count,
                VotesCount = product.Rates.Count,
                ReleaseDateString = product.ReleaseDate.ToDisplayableDate(),
                PegiID = product.PegiRatingId
            };
            productViewModel.BasePriceGr = product.BasePrice.ToString().Split('.')[1];

            var comments = OpinionManager.Comments.Where(x => x.ProductId == product.Id).ToList();

            var addComment = new AddCommentViewModel
            {
                ProductId = product.Id
            };

            var model = new DetailsViewModel
            {
                Product = productViewModel,
                Comments = comments,
                AddComment = addComment
            };

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
        public PartialViewResult InitializeFilter(string keyword, Guid? platformId, Guid? genreId, Guid? pegiId)
        {
            return PartialView("_FilterAndSort", CreateFilter(keyword, platformId, genreId, pegiId));
        }

        [HttpPost]
        public PartialViewResult Search(ProductFilterModel filter)
        {
            return PartialView("_SearchProductsResult", SearchProducts(filter));
        }

        [NonAction]
        private ProductFilterModel CreateFilter(string keyword, Guid? platformId, Guid? genreId, Guid? pegiId)
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
                    Selected = pegi.Id == pegiId
                });
            }

            return filter;
        }
        
        [NonAction]
        private IEnumerable<ProductListItem> SearchProducts(ProductFilterModel filter)
        {
            var products = new List<ProductListItem>();

            bool isSelectedAnyPlatform = filter.Platforms != null && filter.Platforms.Any(p => p.Selected);
            bool isSelectedAnyGenre = filter.Genres != null && filter.Genres.Any(p => p.Selected);
            bool isSelectedAnyPegi = filter.PegiRatings != null && filter.PegiRatings.Any(p => p.Selected);

            IEnumerable<SelectListItem> selectedPlatforms = null;
            IEnumerable<SelectListItem> selectedPegis = null;
            IEnumerable<SelectListItem> selectedGenres = null;

            if (isSelectedAnyGenre) { selectedGenres = filter.Genres.Where(p => p.Selected == true); }
            if (isSelectedAnyPegi) { selectedPegis = filter.PegiRatings.Where(p => p.Selected == true); }
            if (isSelectedAnyPlatform) { selectedPlatforms = filter.Platforms.Where(p => p.Selected == true); }

            foreach (var prod in ProductManager.Products.ToList())
            {
                if (!prod.IsDeleted && prod.Name.Contains(filter.SearchKeyword != null ? filter.SearchKeyword : string.Empty))
                {
                    if (isSelectedAnyGenre && !selectedGenres.Any(g =>
                        g.Value == prod.GenreId.ToString())) { continue; }
                    if (isSelectedAnyPlatform && !selectedPlatforms.Any(p =>
                        p.Value == prod.PlatformId.ToString())) { continue; }
                    if (isSelectedAnyPegi && !selectedPegis.Any(p =>
                        p.Value == prod.PegiRatingId.ToString())) { continue; }
                    products.Add(prod.ToListItem());
                }
            }

            return products.SortBy(filter.ProductSortType);
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
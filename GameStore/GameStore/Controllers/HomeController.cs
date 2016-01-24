using GameStore.Entities;
using GameStore.Helpers;
using GameStore.Managers;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.Controllers
{
    public class HomeController : Controller
    {
        public ProductManager ProductManager { get; private set; }

        public HomeController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
        }

        public ActionResult Index()
        {
            #region preorders

            var preOrders = (from p in ProductManager.Products
                             where p.ReleaseDate > DateTime.Today
                             select new ProductListItem
                             {
                                 Id = p.Id,
                                 ReleaseDate = p.ReleaseDate,
                                 CoverList = p.CoverList,
                                 Name = p.Name,
                                 PlatformName = p.Platform.NameShort,
                                 PlatformId = p.PlatformId,
                                 Price = p.DiscountPrice.HasValue ? p.DiscountPrice.Value : p.BasePrice,
                             })
                             .Take(3)
                             .ToList();

            foreach (var item in preOrders)
            {
                item.ReleaseDateString = item.ReleaseDate.ToDisplayableDate();
                item.BasePriceZl = Convert.ToInt32(item.Price.ToString().Split('.')[0]);
                item.BasePriceGr = item.Price.ToString().Split('.')[1];
            }

            #endregion
            #region discount

            var discount = (from p in ProductManager.Products
                             where p.DiscountPrice.HasValue
                             select new ProductListItem
                             {
                                 Id = p.Id,
                                 ReleaseDate = p.ReleaseDate,
                                 CoverList = p.CoverList,
                                 Name = p.Name,
                                 PlatformName = p.Platform.NameShort,
                                 PlatformId = p.PlatformId,
                                 Price = p.DiscountPrice.HasValue ? p.DiscountPrice.Value : p.BasePrice,
                                 DiscountPercentage = (int)(100 - 100 * p.DiscountPrice.Value / p.BasePrice)
                             })
                             .Take(3)
                             .ToList();

            foreach (var item in discount)
            {
                item.ReleaseDateString = item.ReleaseDate.ToDisplayableDate();
                item.DiscountPriceZl = Convert.ToInt32(item.Price.ToString().Split('.')[0]);
                item.DiscountPriceGr = item.Price.ToString().Split('.')[1];
            }

            #endregion
            #region top10

            var top10 = (from p in ProductManager.Products
                             orderby p.Rating descending
                             select new ProductListItem
                             {
                                 Id = p.Id,
                                 ReleaseDate = p.ReleaseDate,
                                 CoverList = p.CoverList,
                                 Name = p.Name,
                                 PlatformName = p.Platform.NameShort,
                                 PlatformId = p.PlatformId,
                                 Price = p.DiscountPrice.HasValue ? p.DiscountPrice.Value : p.BasePrice,
                             })
                             .Take(10)
                             .ToList();

            foreach (var item in top10)
            {
                item.ReleaseDateString = item.ReleaseDate.ToDisplayableDate();
                item.BasePriceZl = Convert.ToInt32(item.Price.ToString().Split('.')[0]);
                item.BasePriceGr = item.Price.ToString().Split('.')[1];
            }

            #endregion

            var model = new MainPageViewModel
            {
                PreOrders = preOrders,
                Discount = discount,
                Top10 = top10
            };
            
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
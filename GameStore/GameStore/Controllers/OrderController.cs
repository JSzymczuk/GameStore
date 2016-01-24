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
    public class OrderController : Controller
    {
        private string cartCookieName = "Cart";
        private string cookieProductsField = "Products";

        public AccountManager AccountManager { get; private set; }
        public OrderManager OrderManager { get; private set; }

        public OrderController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            AccountManager = new AccountManager(context);
            OrderManager = new OrderManager(context);
        }

        [NonAction]
        private ICollection<OrderViewModel> GetUserOrders()
        {
            string userId = AccountHelper.GetLoggedUserId();
            var tempCollection = OrderManager.Orders.Where(o => o.UserId == userId).ToList();
            var model = new List<OrderViewModel>();

            foreach (var item in tempCollection)
            {
                model.Add(item.ToListItem());
            }

            return model.OrderByDescending(o => o.OrderDate).ToList();
        }

        public ActionResult Index()
        {
            return View(GetUserOrders());
        }

        [HttpGet]
        public ActionResult Create()
        {
            string userId = AccountHelper.GetLoggedUserId();

            List<CartItemViewModel> products;
            if (!CookieHelper.TryReadCookie<List<CartItemViewModel>>(
                cartCookieName, cookieProductsField, out products))
            {
                products = new List<CartItemViewModel>();
            }
            decimal total = 0;
            foreach (var item in products)
	        {
		         total += item.TotalPrice;
	        }

            return View(new MakeOrderViewModel
            {
                Addresses = AccountManager.GetUserAddressesSelectList(userId),
                Products = products,
                TotalPrice = total
            });
        }

        [HttpPost]
        public ActionResult Create(MakeOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                Order order = new Order
                {
                    OrderDate = DateTime.Now,
                    AddressId = model.SelectedAddress,
                    AdditionalInfo = model.AdditionalInfo,
                    IsDeleted = false,
                    Paid = false,
                    UserId = AccountHelper.GetLoggedUserId(),
                    Products = new List<OrderedProduct>()
                };

                List<CartItemViewModel> products;
                if (!CookieHelper.TryReadCookie<List<CartItemViewModel>>(
                    cartCookieName, cookieProductsField, out products))
                {
                    products = new List<CartItemViewModel>();
                }

                foreach (var item in products)
                {
                    order.Products.Add(new OrderedProduct
                    {
                        Order = order,
                        Price = item.Price,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity
                    });
                }

                OrderManager.Create(order);
                OrderManager.Save();
                CookieHelper.DeleteCookie(cartCookieName);

                return View("Index", GetUserOrders()); 
            }

            return View(model);
        }
        
        [HttpGet]
        public ActionResult Details(Guid id)
        {
            return View(OrderManager.FindById(id).ToViewModel());
        }

        [HttpGet]
        public PartialViewResult Cancel(Guid id)
        {
            OrderManager.Cancel(id);
            OrderManager.Save();
            return PartialView("_Orders", GetUserOrders());
        }

        [HttpGet]
        public PartialViewResult PayFor(Guid id)
        {
            OrderManager.PayFor(id);
            OrderManager.Save();
            return PartialView("_Orders", GetUserOrders());
        }

        [HttpGet]
        public PartialViewResult Send(Guid id)
        {
            OrderManager.Send(id);
            OrderManager.Save();
            return PartialView("_Orders", GetUserOrders());
        }
    }
}
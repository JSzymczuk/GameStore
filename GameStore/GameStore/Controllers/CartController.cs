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
    public class CartController : Controller
    {
        public ProductManager ProductManager { get; private set; }
        
        public CartController()
        {
            GameStoreDbContext context = new GameStoreDbContext();
            ProductManager = new ProductManager(context);
        }

        public CartController(ProductManager productManager)
        {
            ProductManager = productManager;
        }

        private string cartCookieName = "Cart";
        private string cookieProductsField = "Products";

        public ActionResult Index()
        {
            return View();
        }

        public PartialViewResult GetCart()
        {
            List<CartItemViewModel> products;
            if (!CookieHelper.TryReadCookie<List<CartItemViewModel>>(
                cartCookieName, cookieProductsField, out products))
            {
                products = new List<CartItemViewModel>();
            }
            return PartialView("_Cart", GetCart(products));
        }

        [NonAction]
        private CartViewModel GetCart(List<CartItemViewModel> products)
        {
            var cart = new CartViewModel
            {
                Products = products
            };
            decimal totalPrice = 0;
            int totalQuantity = 0;
            foreach (var item in products)
            {
                totalQuantity += item.Quantity;
                totalPrice += item.TotalPrice;
            }
            cart.TotalPrice = totalPrice;
            cart.TotalProducts = totalQuantity;
            return cart;
        }

        public PartialViewResult AddProduct(Guid id)
        {
            HttpCookie cookie;
            var product = ProductManager.FindById(id);
            decimal price = product.DiscountPrice.HasValue ? 
                    product.DiscountPrice.Value : product.BasePrice;
            var cartItem = new CartItemViewModel
            {
                ProductId = id,
                Quantity = 1,
                ProductName = product.Name,
                ProductCover = product.CoverList,
                PlatformName = product.Platform.Name, 
                Price = price, 
                TotalPrice = price
            };

            List<CartItemViewModel> cartItems;

            if (CookieHelper.CookieExists(cartCookieName, out cookie))
            {
                cartItems = CookieHelper.ReadCookie<List<CartItemViewModel>>(
                    cookie, cookieProductsField);

                if (cartItems == null)
                {
                    CookieHelper.DeleteCookie(cartCookieName);
                    cartItems = new List<CartItemViewModel> { cartItem };
                    CookieHelper.CreateCookie<List<CartItemViewModel>>(
                        cartCookieName, cookieProductsField, cartItems, new TimeSpan(0, 30, 0));
                }
                else
                {
                    if (!cartItems.Any(i => i.ProductId == id))
                    {
                        cartItems.Add(cartItem);
                    }
                    else
                    {
                        var increasedItem = cartItems.First(i => i.ProductId == id);
                        increasedItem.Quantity++;
                        increasedItem.TotalPrice += increasedItem.Price;
                    }
                    CookieHelper.UpdateCookie<List<CartItemViewModel>>(
                        cookie, cookieProductsField, cartItems);
                }
            }
            else
            {
                cartItems = new List<CartItemViewModel> { cartItem };
                CookieHelper.CreateCookie<List<CartItemViewModel>>(
                    cartCookieName, cookieProductsField, cartItems, new TimeSpan(0, 30, 0));
            }
            return PartialView("_Cart", GetCart(cartItems));
        }

        public PartialViewResult RemoveProduct(Guid id)
        {
            HttpCookie cookie;
            List<CartItemViewModel> cartItems;

            if (CookieHelper.CookieExists(cartCookieName, out cookie))
            {
                cartItems = CookieHelper.ReadCookie<List<CartItemViewModel>>(cookie, cookieProductsField);

                if (cartItems == null)
                {
                    CookieHelper.DeleteCookie(cartCookieName);
                }
                else
                {

                    CartItemViewModel item = cartItems.First(i => i.ProductId == id);
                    if (item != null)
                    {
                        cartItems.Remove(item);
                        CookieHelper.UpdateCookie<List<CartItemViewModel>>(
                            cookie, cookieProductsField, cartItems);
                    }
                }
            }
            else { cartItems = new List<CartItemViewModel>(); }

            return PartialView("_Cart", GetCart(cartItems));
        }

        public PartialViewResult DestroyCart()
        {
            CookieHelper.DeleteCookie(cartCookieName);
            return PartialView("_Cart", GetCart(new List<CartItemViewModel>()));
        }

        public PartialViewResult UpdateQuantity(Guid productId, int quantity)
        {
            List<CartItemViewModel> cartItems = null;
            var product = ProductManager.FindById(productId);

            if (!TryUpdateQuantity(productId, quantity, out cartItems))
            {
                // Tworzymy nowe ciasteczko, jeśli jeszcze nie ma, 
                // albo wystąpiły problemy z jedno odczytem.
                decimal price = product.DiscountPrice.HasValue ?
                        product.DiscountPrice.Value : product.BasePrice;
                if (product != null)
                {
                    cartItems = new List<CartItemViewModel>
                    {
                        new CartItemViewModel 
                        {
                            ProductId = productId,
                            Quantity = quantity,
                            ProductName = product.Name,
                            ProductCover = product.CoverList,
                            PlatformName = product.Platform.Name, 
                            Price = price, 
                            TotalPrice = quantity * price
                        }
                    };
                    CookieHelper.CreateCookie<ICollection<CartItemViewModel>>(
                        cartCookieName, cookieProductsField, cartItems, new TimeSpan(0, 30, 0));
                }
                else
                {
                    cartItems = CookieHelper.ReadCookie<List<CartItemViewModel>>(
                        cartCookieName, cookieProductsField) ?? new List<CartItemViewModel>();
                }
            }

            return PartialView("_Cart", GetCart(cartItems));
        }

        [NonAction]
        private bool TryUpdateQuantity(Guid productId, int quantity, 
            out List<CartItemViewModel> cartItems)
        {
            HttpCookie cookie;
            if (CookieHelper.CookieExists(cartCookieName, out cookie))
            {
                cartItems = CookieHelper.ReadCookie<List<CartItemViewModel>>(
                    cookie, cookieProductsField);
                if (cartItems == null)
                {
                    // Ciasteczko jest uszkodzone.
                    CookieHelper.DeleteCookie(cartCookieName);
                    return false;
                }
                CartItemViewModel item = cartItems.First(i => i.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = quantity;
                    item.TotalPrice = quantity * item.Price;
                }
                else
                {
                    var product = ProductManager.FindById(productId);
                    decimal price = product.DiscountPrice.HasValue ?
                        product.DiscountPrice.Value : product.BasePrice;
                    if (product != null)
                    {
                        cartItems.Add(new CartItemViewModel 
                        {
                            ProductId = productId,
                            Quantity = quantity,
                            ProductName = product.Name,
                            ProductCover = product.CoverList,
                            PlatformName = product.Platform.Name, 
                            Price = price, 
                            TotalPrice = quantity * price
                        });
                    }
                    else
                    {
                        return false;
                    }
                }
                CookieHelper.UpdateCookie<ICollection<CartItemViewModel>>(cookie, cookieProductsField, cartItems);
                return true;
            }
            cartItems = null;
            return false;
        }
    }
}
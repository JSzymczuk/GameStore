using GameStore.Entities;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.Helpers
{
    public static class OrderHelper
    {
        public static OrderViewModel ToListItem(this Order item)
        {
            var address = item.Address;
            var email = item.User.Email;
            var model = new OrderViewModel
            {
                AdditionalInfo = item.AdditionalInfo,
                AddressString = address.ToDisplayableString(),
                Id = item.Id,
                OrderDate = item.OrderDate.ToDisplayableDate(),
                UserEmail = email,
                UserId = item.UserId,
                IsPaid = item.Paid,
                IsSent = item.SentDate.HasValue,
                IsCancelled = item.IsDeleted,
                Products = new List<CartItemViewModel>()
            };

            if (model.IsSent)
            {
                model.SentDate = item.SentDate.Value.ToDisplayableDate();
                model.Status = "wysłano";
                model.CanBeCancelled = false;
                model.CanBeSent = false;
            }
            else if (model.IsCancelled)
            {
                model.Status = "anulowane";
                model.CanBeCancelled = false;
                model.CanBeSent = false;
            }
            else if (model.IsPaid)
            {
                model.Status = "opłacone";
                model.CanBeCancelled = true;
                model.CanBeSent = true;
            }
            else
            {
                model.Status = "zarejestrowane";
                model.CanBeCancelled = true;
                model.CanBeSent = false;
            }
            
            decimal total = 0;
            foreach (var prod in item.Products)
            {
                total += prod.Quantity * prod.Price;
            }
            model.TotalPrice = string.Format("{0:0.00}zł", total);

            return model;
        }

        public static OrderViewModel ToViewModel(this Order item)
        {
            var model = item.ToListItem();

            foreach (var prod in item.Products)
            {
                model.Products.Add(new CartItemViewModel 
                { 
                    ProductId = prod.ProductId, 
                    ProductName = prod.Product.Name, 
                    ProductCover = prod.Product.CoverThumb, 
                    PlatformName = prod.Product.Platform.NameShort, 
                    Quantity = prod.Quantity, Price = prod.Price, 
                    TotalPrice = prod.Quantity * prod.Price, 
                    ProductLang = prod.Product.Language, 
                    ProductPublisher = prod.Product.Publisher 
                });
            }

            return model;
        }
    }
}
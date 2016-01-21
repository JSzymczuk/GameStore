using GameStore.Entities;
using GameStore.Managers;
using GameStore.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

namespace GameStore.Helpers
{
    public static class AccountHelper
    {
        public static string GetLoggedUserId()
        {
            return System.Web.HttpContext.Current.User.Identity.GetUserId();
        }

        public static bool UserInRole(string roleName)
        {
            return System.Web.HttpContext.Current.User.IsInRole(roleName);
        }

        public static string GetDisplayableName(this Region region)
        {
            var memInfo = typeof(Region).GetMember(region.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
            return ((DescriptionAttribute)attributes[0]).Description;
        }

        public static ICollection<Address> GetUserAddresses(this AccountManager manager, string userId)
        {
            return manager.Addresses.Where(a => a.UserId == userId).ToList();
        }

        public static Address ToAddress(this AddressViewModel model)
        {
            return new Address
            { 
                AdditionalInfo = model.AdditionalInfo, 
                City = model.City, 
                House = model.House, 
                Id = model.Id,  
                IsDeleted = model.IsDeleted,
                Local = model.Local, 
                Post = model.Post, 
                PostalCode = model.PostalCode, 
                Region = model.Region, 
                Street = model.Street, 
                UserId = model.UserId 
            };
        }

        public static AddressViewModel ToAddressViewModel(this Address address)
        {
            return new AddressViewModel
            {
                AdditionalInfo = address.AdditionalInfo,
                City = address.City,
                House = address.House,
                Id = address.Id,
                IsDeleted = address.IsDeleted,
                Local = address.Local,
                Post = address.Post,
                PostalCode = address.PostalCode,
                Region = address.Region,
                Street = address.Street,
                UserId = address.UserId 
            };
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductCover { get; set; }
        public string ProductName { get; set; }
        public string PlatformName { get; set; }
        public string ProductPublisher { get; set; }
        public string ProductLang { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class CartViewModel
    {
        public List<CartItemViewModel> Products { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalProducts { get; set; }
    }

    public enum OrderStatus
    {
        [Description("zarejestrowane")]
        Initialised,
        [Description("opłacone")]
        Paid,
        [Description("wysłane")]
        Sent,
        [Description("zrealizowane")]
        Completed
    }

    public class OrderViewModel
    {
        [DisplayName("Numer zamówienia")]
        public Guid Id { get; set; }
        [DisplayName("Złożono dnia")]
        public string OrderDate { get; set; }
        [DisplayName("Nadano dnia")]
        public string SentDate { get; set; }
        [DisplayName("Kwota")]
        public string TotalPrice { get; set; }
        [DisplayName("Stan")]
        public string Status { get; set; }
        [DisplayName("Uwagi")]
        public String AdditionalInfo { get; set; }
        [DisplayName("Adres dostawy")]
        public string AddressString { get; set; }
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public bool IsCancelled { get; set; }
        public bool IsPaid { get; set; }
        public bool IsSent { get; set; }
        public bool CanBeSent { get; set; }
        public bool CanBeCancelled { get; set; }
        public List<CartItemViewModel> Products { get; set; }
    }

    public class MakeOrderViewModel
    {
        public List<CartItemViewModel> Products { get; set; }
        [DisplayName("Razem do zapłaty")]
        public decimal TotalPrice { get; set; }
        public SelectList Addresses { get; set; }
        [DisplayName("Adres dostawy")]
        public Guid SelectedAddress { get; set; }
        [DisplayName("Uwagi do zamówienia")]
        public String AdditionalInfo { get; set; }
    }
}
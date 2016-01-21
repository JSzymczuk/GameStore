using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace GameStore.ViewModels
{
    public class CartItemViewModel
    {
        public Guid ProductId { get; set; }
        public string ProductCover { get; set; }
        public string ProductName { get; set; }
        public string PlatformName { get; set; }
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

    public class OrderListItem
    {
        [DisplayName("Numer")]
        public Guid Id { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Złożono dnia")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Kwota")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Stan")]
        public OrderStatus Status { get; set; }
        [DisplayName("Uwagi")]
        public String AdditionalInfo { get; set; }
    }
    
    public class OrderViewModel
    {
        [DisplayName("Numer")]
        public Guid Id { get; set; }
        [DisplayName("Złożono dnia")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Kwota")]
        public decimal TotalPrice { get; set; }
        [DisplayName("Stan")]
        public OrderStatus Status { get; set; }
        [DisplayName("Uwagi")]
        public String AdditionalInfo { get; set; }
        public List<CartItemViewModel> Products { get; set; }
    }
}
using System.Collections.Generic;

namespace GameStore.ViewModels
{
    public class MainPageViewModel
    {
        public List<ProductListItem> PreOrders { get; set; }
        public List<ProductListItem> Discount { get; set; }
        public List<ProductListItem> Collectors { get; set; }
        public List<ProductListItem> Top10 { get; set; }
    }
}
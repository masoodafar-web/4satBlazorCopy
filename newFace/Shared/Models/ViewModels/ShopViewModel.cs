using newFace.Shared.Models.Education;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class ShopViewModel
    {
        public List<ShopHomeSlider> Sliders { get; set; }

        public List<ProductSummaryViewModels> SuggestionProducts { get; set; }

        public List<ProductSummaryViewModels> NewProducts        { get; set; }

        public List<ProductSummaryViewModels> BestSellerProducts { get; set; }
    }
}
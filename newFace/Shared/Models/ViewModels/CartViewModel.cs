using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Education;

namespace newFace.Shared.Models.ViewModels
{
    public class CartViewModel
    {
        List<Cart> _c = new List<Cart>();
        public List<Cart> Carts
        {
            get { return _c;}
            set { value = _c; }
        }
         
        public int CartCount { get; set; }


        public double TotalAmount { get; set; }

        public double TotalDiscount { get; set; }

        public double TotalPriceToPay { get; set; }

        public double TotalPoint { get; set; }

        public ApplicationUser User { get; set; }

        public bool PayFromCredit { get; set; }
    }
}
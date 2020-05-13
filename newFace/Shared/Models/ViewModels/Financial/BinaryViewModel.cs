using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class BinaryViewModel
    {
        public int UserGeneologyId { get; set; }

        public long SellCollect { get; set; }
        public bool IsEqualAmountBalance { get; set; }

        public long ModAmount { get; set; }
        public int BalanceCount { get; set; }
    }
}
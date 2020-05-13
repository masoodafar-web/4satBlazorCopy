using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class BraekViewModel
    {
        public int UserGeneologyId { get; set; }
        public int UserPercent { get; set; }
        public int? UserGeneologyParentId { get; set; }

        public long TotalSellCollect { get; set; }
        public bool Break { get; set; }
    }
}
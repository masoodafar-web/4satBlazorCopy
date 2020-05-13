using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class FavoriteVm
    {

        public int Id { get; set; }

        public bool? IsFaved { get; set; }

        public int? UserFavedCount { get; set; }
    }
}
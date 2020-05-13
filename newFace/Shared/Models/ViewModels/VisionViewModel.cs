using newFace.Shared.Models.Advice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class AdviceViewModel
    {
        [JsonIgnore]
        public ApplicationUser User { get; set; } = new ApplicationUser();

        [JsonIgnore]
        public Vision Vision { get; set; } = new Vision();

        public List<VisionViewModel> VisionViewModels { get; set; }
    }

    public class VisionViewModel
    {
        public Vision Vision { get; set; }
        public List<Skill> Skills { get; set; }
        public List<Category> Categorys { get; set; }
    }
  
}
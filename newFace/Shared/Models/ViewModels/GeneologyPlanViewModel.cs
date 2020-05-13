using newFace.Shared.Models.Financial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class GeneologyPlanViewModel
    {
        public GeneologyPlan GeneologyPlan { get; set; }

        public PlanUni PlanUni { get; set; }

        public List<PlanUniLevel> PlanUniLevels { get; set; }

    }
}
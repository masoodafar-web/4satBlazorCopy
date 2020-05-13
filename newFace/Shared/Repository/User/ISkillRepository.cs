using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;


namespace newFace.Shared.Repositories.User
{
    public interface ISkillRepository
    {
        Task<Result> AddSkill(SkillViewModel skillViewModel, VisionType? visionType);

        
    }
}

using System.Collections.Generic;
using newFace.Shared.Models;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;


namespace newFace.Shared.Repositories.Resource
{
    public interface IVisionRepository
    {
        void InsertVisionPriority(float percent, int skillId, string UserId, bool IsAdd);
        void ChangeVisionPriorityBySkillChange(float NewPercent, float OldPercent, string UserId, int SkillCatId);
        Result AddVision(Skill skill, List<int> categoryForFinancial, string UserIdForFinancial, VisionType visionType);
        AdviceViewModel FinancialAdviceSuggest(AdviceViewModel model);
        List<VisionViewModel> GetVisionVm(string UserId,VisionType? visionType);
        ResultPercent UpdatePercentForVision(string UserId, int VisioncatId, bool UpdateIsIt);
        Result ChangeVisionStatus(Enums.VisionStatus visionStatus, int visionId, string UserId);
        Result DeleteVisionForUserBySkillId(int skillCatId, string UserId);
        int CalculateSkillPercent(int VisioncatId, int SkillcatId, List<Category_Category> categoryCategories);
        UserGeneology FindUserGeneologyForFinancialVision(string userId, GeneologyTypeEnum type, SystemType systemType);
    }
  
}

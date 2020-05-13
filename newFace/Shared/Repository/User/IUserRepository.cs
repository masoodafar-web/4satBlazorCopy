using System;
using System.Collections.Generic;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels.UserProfileViewModel;

namespace newFace.Shared.Repositories.Resource
{
    public interface IUserRepository 
    {
        #region EditBasic&SocialNetwork
        //ResultUser GetAllUsers();
        UserNameResult GetAllUsersUserName(string text);

        ResultUser GetById(string id);
        ResultUser GetByUserName(string userName);

        //ResultUser GetByUserId(string id);
        ResultUser GetByToken(string id);
        //ResultUser GetByUserName(string UserName);
        Result IsEmailConfirm(string UserId);
        //Result CreateUser(ApplicationUser user);

        //Result DeleteUser(string id);

        Result EditBasicInfo(ApplicationUser User);

        Result EditSocialNetwork(ApplicationUser user);
        #endregion

   

        #region UserInfoComplatePrcent
        //تغییر درصد تکمیل پروفایل
        void ChangeUserInfoComplatePercent(string userId, double complatePercent);
        #endregion

        Result EditCredit(string UserId, double Credit);

        Result EditUser(ProfileEditViewModel pevm);

        bool RemoveFakeUsers();

        ResultPercent CreatePercentSkill(string UserId, int SkillCatId, List<ProductScale> productScales,List<ProductSeenInfo> AllProductSeenInfo, bool UpdateIsIt);
        Result InsertUserCategory(int categoryId, string UserId);

        CVViewModel GetCV(string userId);
    }



   
}

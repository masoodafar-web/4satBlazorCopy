using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.User;

namespace newFace.Server.Services.User
{
    public class SkillRepository : ISkillRepository
    {

        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IVisionRepository _visionRepository;
        private  IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;

        public SkillRepository(ISuggestionRepository suggestionRepository, ICategoryRepository categoryRepository, IVisionRepository visionRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _suggestionRepository = suggestionRepository;
            _CategoryRepository = categoryRepository;
            _visionRepository = visionRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
      
        public Task<Result> AddSkill(SkillViewModel skillViewModel, VisionType? visionType)
        {
            Result result = new Result();
            Result Res = new Result();
            var skill = skillViewModel.ConvertSkillViewModelToSkill();

            #region در صورتی که مهارت ورودی ثبت نشده بود ثبت شود
            var OldskillCheck = _unitOfWork.SkillGR.GetAllIncluding(f => f.UserId == skill.UserId && f.CategoryId == skill.CategoryId, i => i.Category, j => j.Level).ToList();
            if (!OldskillCheck.Any())
            {
                Res = _unitOfWork.SkillGR.Add(skill);
                if (Res.Statue == Enums.Statue.Success)
                {
                    skill = _unitOfWork.SkillGR.GetSingleIncluding(w => w.Id == skill.Id, i => i.Category, j => j.Level);
                    result.Statue = Enums.Statue.Success;
                    result.Messages.Add("مهارت با موفقیت ثبت شد.");

                    //ثبت تمام پدرهای این مهارت برای کاربر
                    #region UserCategory
                   _userRepository.InsertUserCategory(skill.CategoryId, skill.UserId);

                    #endregion

                    //درصد تکمیل پروفایل
                    #region CompltPercent
                    int Skill = _unitOfWork.SkillGR.FindBy(f => f.UserId == skillViewModel.UserId).Count();
                    if (Skill == 1)
                    {
                        _userRepository.ChangeUserInfoComplatePercent(skillViewModel.UserId, 16.66666666666667);
                    }
                    #endregion
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Messages.Add("مهارت با موفقیت ثبت نشد.");
                    return result.ToTask();
                }
            }
            else
            {
                skill = OldskillCheck.FirstOrDefault();
                result.Statue = Enums.Statue.Failure;
                result.Message = "مهارت وارد شده تکراری می باشد";
                return result.ToTask();
            }

            #endregion


            //پیشنهاد هدف

            #region suggestVision

            var VisionSaveResult = _visionRepository.AddVision(skill, null, null, visionType.Value);
            #endregion

            //پیشنهاد محصول

            #region suggestProduct

            if (VisionSaveResult.Statue == Enums.Statue.Success)
            {
                result.Messages.Add("اهداف مربوط با موفقیت ثبت شد.");

                var Result = _suggestionRepository.InsertToSuggestProduct(
                    _suggestionRepository.SkillProducts(skillViewModel.CategoryId), skillViewModel.UserId, null, skill.CategoryId, (int?)null);
                if (Result.Statue != Enums.Statue.Success)
                {
                    #region DeleteBeforOperator

                    var deletSkill = _unitOfWork.SkillGR.Delete(_unitOfWork.SkillGR.GetById(skill.Id));
                    result.Statue = Enums.Statue.Failure;
                    result.Messages.Add("پیشنهاد محصول با خطا مواجه شد .");

                    if (deletSkill.Statue == Enums.Statue.Success)
                    {
                        result.Messages.Add("مهارت مربوط حذف گردید");
                    }

                   var visionDeletResult= _visionRepository.DeleteVisionForUserBySkillId(skillViewModel.CategoryId, skillViewModel.UserId);
                   if (visionDeletResult.Statue==Enums.Statue.Success)
                   {
                       result.Messages.Add(visionDeletResult.Messages.FirstOrDefault());
                   }

                    #endregion
                }
                else
                {
                    result.Statue = Enums.Statue.Success;
                    result.Messages.Add("پیشنهاد محصول موفقیت ثبت شد.");
                }
            }
            else
            {
                var deletSkill = _unitOfWork.SkillGR.Delete(_unitOfWork.SkillGR.GetById(skill.Id));
                result.Statue = Enums.Statue.Failure;
                result.Messages.Add("پیشنهاد محصول با خطا مواجه شد .");

                if (deletSkill.Statue == Enums.Statue.Success)
                {
                    result.Messages.Add("مهارت مربوط حذف گردید");
                }
                return VisionSaveResult.ToTask();

            }


            #endregion



            result.Message="عملیات موفقیت آمیز بود.";
            return result.ToTask();
        }

    }


}
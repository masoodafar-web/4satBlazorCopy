using System.Collections.Generic;
using System.Linq;
using System;
using System.Data;
using newFace.Shared.Models;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Identity;

namespace newFace.Server.Services.Resource
{
    public class VisionRepository : IVisionRepository
    {
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly ICategoryRepository _CategoryRepository;
        private readonly IUserRepository _userRepository;
        private UserManager<ApplicationUser> UserManager;
        private IUnitOfWork _unitOfWork;

        public VisionRepository(ISuggestionRepository suggestionRepository, ICategoryRepository categoryRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _suggestionRepository = suggestionRepository;
            _CategoryRepository = categoryRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        

        public void InsertVisionPriority(float percent, int skillId, string UserId, bool IsAdd)
        {

            var visionsCategoryCategories = _unitOfWork.Category_CategoryGR.FindBy(f => f.ChildrenCatId == skillId).ToList();
            foreach (var categoryCategory in visionsCategoryCategories)
            {
                float Result = CalculateSkillPercent(categoryCategory.ParentCatId.Value, categoryCategory.ChildrenCatId, null) * percent / 100;
                var oldVision = _unitOfWork.VisionGR.FirstOrDefault(f =>
                    f.CategoryId == categoryCategory.ParentCatId && f.UserId == UserId);
                if (oldVision != null)
                {
                    if (IsAdd)
                    {
                        oldVision.Priority += Result;
                    }
                    else
                    {
                        oldVision.Priority -= Result;
                    }
                    //اگر صفر شد دو حالت دارد، یا صفر وارد شده همین الان یا با حذف کردن مهارت ،صفر میشود
                    if (oldVision.Priority == 0)
                    {
                        var userId = oldVision.UserId;
                        if (oldVision.VisionStatus == Enums.VisionStatus.Advice)
                        {
                            #region DeleteVisionBySomeTime
                            //اگر مهارت های ثبت شده برای هدف برای کاربر وجود نداشت آن هدفش حذف شود 
                            var thisVisionSkills = _CategoryRepository.FindOneLevelChildList(oldVision.CategoryId);
                            bool haveSkill = false;
                            foreach (var skill in thisVisionSkills)
                            {
                                if (_unitOfWork.SkillGR.Any(f => f.CategoryId == skill.Id && f.UserId == oldVision.UserId))
                                {
                                    haveSkill = true;
                                }
                            }

                            if (!haveSkill)
                            {
                                _unitOfWork.VisionGR.Delete(oldVision);
                            }
                            #endregion
                        }
                        else
                        {
                            var result = _unitOfWork.VisionGR.Update(oldVision);
                        }
                    }
                    else
                    {
                        var result = _unitOfWork.VisionGR.Update(oldVision);
                    }
                }


            }


        }

        public void ChangeVisionPriorityBySkillChange(float NewPercent, float OldPercent, string UserId,
            int SkillCatId)
        {

            var visionsCategoryCategories = _unitOfWork.Category_CategoryGR.FindBy(f => f.ChildrenCatId == SkillCatId).ToList();
            foreach (var item in visionsCategoryCategories)
            {
                var oldVision = _unitOfWork.VisionGR.FirstOrDefault(f =>
                    f.CategoryId == item.ParentCatId && f.UserId == UserId);
                if (oldVision != null)
                {
                    var oldPriority = CalculateSkillPercent(item.ParentCatId.Value, item.ChildrenCatId, null) * OldPercent / 100;
                    var newPriority = CalculateSkillPercent(item.ParentCatId.Value, item.ChildrenCatId, null) * NewPercent / 100;
                    if (oldPriority < newPriority)
                    {
                        var r = newPriority - oldPriority;
                        oldVision.Priority += (int)r;
                    }
                    else
                    {
                        var r = oldPriority - newPriority;
                        oldVision.Priority -= (int)r;
                    }

                    _unitOfWork.VisionGR.Update(oldVision);
                }

            }

        }

        public Result AddVision(Skill skill, List<int> categoryIdForFinancial, string UserIdForFinancial, VisionType visionType)
        {
            List<Category> Parent = new List<Category>();
            //زمانی که مالی بود
            if (visionType == VisionType.Financial)
            {
                foreach (var categoryId in categoryIdForFinancial)
                {
                    var category = _unitOfWork.CategoryGR.GetById(categoryId);
                    Parent.Add(category);
                }
                //برای حذف اهداف مالی پیشنهادی قبلی
                var OldAdvicesVision = _unitOfWork.VisionGR
                     .FindBy(f => f.UserId == UserIdForFinancial && f.VisionStatus == Enums.VisionStatus.Advice && f.VisionType == VisionType.Financial)
                     .ToList();
                var result =  _unitOfWork.VisionGR.DeleteRange(OldAdvicesVision);
            }
            //زمانی که غیر مالی بود
            else if (visionType != VisionType.Financial)
            {
                Parent = _CategoryRepository.FindOneLevelParentList(skill.CategoryId);
            }
            //اگر ارتقاء مهارت مورد نظر ثبت نشده بود
            if (visionType == VisionType.Person && !Parent.Any(w => w.CategoryType == CategoryTypeEnum.SkillUpgrade))
            {
                //افزودن کتگوری و ساختار درختی برای ارتقاء

                #region افزودن کتگوری برای ارتقاء

                var SkillUpgradeCat = new Category()
                {
                    CategoryType = CategoryTypeEnum.SkillUpgrade,
                    Img = skill.Category.Img,
                    Title = skill.Category.Title + "ارتقاء"
                };
                var catSaveResult = _unitOfWork.CategoryGR.Add(SkillUpgradeCat);

                #endregion

                #region ثبت ساختار درختی جدید 

                if (catSaveResult.Statue == Enums.Statue.Success)
                {
                    var Cat_CatResult = _unitOfWork.Category_CategoryGR.Add(new Category_Category()
                    {
                        ChildrenCatId = skill.CategoryId,
                        ParentCatId = SkillUpgradeCat.Id,
                        Percent = 100,
                        Priority = 0,
                    });
                    Parent.Add(SkillUpgradeCat);
                    if (Cat_CatResult.Statue != Enums.Statue.Success)
                        return Cat_CatResult;

                }
                else
                {
                    return catSaveResult;
                }

                #endregion

            }

            bool allVisionSaveResult = true;
            List<Vision> OldVision = new List<Vision>();
            foreach (var item in Parent)
            {
                //اگر مالی بود 
                if (visionType == VisionType.Financial)
                {
                    OldVision = _unitOfWork.VisionGR.FindBy(f => f.CategoryId == item.Id && f.UserId == UserIdForFinancial).ToList();
                }//وگرنه:)))
                else
                {
                    OldVision = _unitOfWork.VisionGR.FindBy(f => f.CategoryId == item.Id && f.UserId == skill.UserId).ToList();
                }

                if (!OldVision.Any())
                {
                    Result VisionResult = new Result();
                    ;
                    Vision Newvision = new Vision();
                    switch (visionType)
                    {
                        case VisionType.Work:
                            if (item.CategoryType == CategoryTypeEnum.Job)
                            {
                                Newvision = new Vision()
                                {
                                    CategoryId = item.Id,
                                    UserId = skill.UserId,
                                    Priority = 0,
                                    Percent = 0,
                                    VisionStatus = Enums.VisionStatus.Advice,
                                    VisionType = visionType
                                };
                                VisionResult = _unitOfWork.VisionGR.Add(Newvision);
                            }
                            else
                            {
                                //میکنیم Success برای اینکه ممکن است پدر این مهارت غیر از ارتقاء باشد و در نتیجه خروجی را 
                                VisionResult = new Result()
                                {
                                    Statue = Enums.Statue.Success,
                                };
                            }
                            break;
                        case VisionType.Person:
                            if (item.CategoryType == CategoryTypeEnum.SkillUpgrade)
                            {
                                Newvision = new Vision()
                                {
                                    CategoryId = item.Id,
                                    UserId = skill.UserId,
                                    Priority = 0,
                                    Percent = 0,
                                    VisionStatus = Enums.VisionStatus.Selected,
                                    VisionType = visionType
                                };
                                VisionResult = _unitOfWork.VisionGR.Add(Newvision);
                                _suggestionRepository.SuggestProductByVisionCatId(item.Id, skill.UserId);
                            }
                            else
                            {
                                //میکنیم Success برای اینکه ممکن است پدر این مهارت غیر از ارتقاء باشد و در نتیجه خروجی را 
                                VisionResult = new Result()
                                {
                                    Statue = Enums.Statue.Success,
                                };
                            }
                            break;
                        case VisionType.Financial:
                            Newvision = new Vision()
                            {
                                CategoryId = item.Id,
                                UserId = UserIdForFinancial,
                                Priority = 0,
                                Percent = 0,
                                VisionStatus = Enums.VisionStatus.Advice,
                                VisionType = visionType
                            };
                            VisionResult = _unitOfWork.VisionGR.Add(Newvision);

                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(visionType), visionType, null);
                    }


                    if (VisionResult.Statue != Enums.Statue.Success)
                    {
                        allVisionSaveResult = false;
                    }

                }

            }
            //زمانی که مالی بود
            if (visionType == VisionType.Financial)
            {
                foreach (var paretnCategory in Parent)
                {
                    var skillsOfVision = _CategoryRepository.FindAllChildList(paretnCategory.Id, true);
                    foreach (var SkillCategory in skillsOfVision)
                    {
                        var Skill = _unitOfWork.SkillGR.FirstOrDefault(
                             f => f.CategoryId == SkillCategory.Id && f.UserId == UserIdForFinancial);
                        if (Skill != null)
                        {
                            InsertVisionPriority(Skill.Percent, Skill.CategoryId, Skill.UserId, true);
                        }

                    }
                }
            }//زمانی که غیر مالی بود
            else
            {
                InsertVisionPriority(skill.Percent, skill.CategoryId, skill.UserId, true);
            }

            if (allVisionSaveResult)
            {
                return new Result()
                {
                    Statue = Enums.Statue.Success,
                    Message = "اهداف با موفقیت ثبت شد"
                };
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "متاسفانه اهداف با موفقیت ثبت نشد"
                };
            }

        }

        public List<VisionViewModel> GetVisionVm(string UserId, VisionType? visionType)
        {
            List<VisionViewModel> visionViewModel = new List<VisionViewModel>();
            List<Vision> visions = new List<Vision>();
            if (visionType == null)
            {
                visions = _unitOfWork.VisionGR.GetAllIncluding(w => w.UserId == UserId, i => i.Category, j => j.Users).ToList();

            }
            else
            {
                visions = _unitOfWork.VisionGR.GetAllIncluding(w => w.UserId == UserId && w.VisionType == visionType, i => i.Category, j => j.Users).ToList();

            }

            foreach (var vision in visions)
            {
                List<Skill> newskills = new List<Skill>();
                var categoriesSkill = _CategoryRepository.FindOneLevelChildList(vision.CategoryId);
                foreach (var skillCategory in categoriesSkill)
                {
                    var skill = _unitOfWork.SkillGR.FirstOrDefault(f =>
                        f.UserId == UserId && f.CategoryId == skillCategory.Id);
                    if (skill != null)
                    {
                        //اگر محصولی جدید وارد این مهارت شود که باعث بهم ریختگی درصد میشود و این مهارت  از حالت بروز بیرون می اید 
                        //آیا مهارت بروز است؟
                        if (skill.IsUpdate)
                        {
                            newskills.Add(skill);
                        }
                        else if (!skill.IsUpdate && skill.SkillType == SkillType.Adventitious)

                        {
                            //بروزرسانی درصد مهارت کاربر
                            var resultPercent = _userRepository.CreatePercentSkill(UserId, skillCategory.Id, null, null, true);

                            float oldPercent = skill.Percent;
                            skill.Percent = resultPercent.NewPercent;
                            skill.IsUpdate = true;
                            newskills.Add(skill);
                            //تغییر اولویت اهداف کاربر
                            ChangeVisionPriorityBySkillChange(resultPercent.NewPercent, oldPercent, skill.UserId,
                                skill.CategoryId);
                        }
                    }
                }

                if (!vision.IsUpdate)
                {
                    //بروزرسانی درصد ها زمانی که به هدف مهارتی اضافه میشود
                    var result = UpdatePercentForVision(vision.UserId, vision.CategoryId, true);
                }
                visionViewModel.Add(new VisionViewModel()
                {
                    Vision = vision,
                    Categorys = categoriesSkill,
                    Skills = newskills
                });
            }
            return visionViewModel?.OrderByDescending(o => o.Vision?.VisionStatus).ThenByDescending(o => o.Vision?.Priority).ToList();

        }

        public int CalculateSkillPercent(int VisioncatId, int SkillcatId, List<Category_Category> categoryCategories)
        {
            //اگر از ورودی دریافت نشد
            if (categoryCategories == null)
            {
                //پیدا کردن مهارت های زیر مجموعه هدف
                categoryCategories = _unitOfWork.Category_CategoryGR.FindBy(f => f.ParentCatId == VisioncatId).ToList();
            }
            else
            {
                categoryCategories = categoryCategories.Where(f => f.ParentCatId == VisioncatId).ToList();
            }

            //جمع عدد های تعیین شده برای مهارت های هدف
            var sumAllSkilPercent = categoryCategories.Sum(s => s.Percent);
            if (sumAllSkilPercent <= 0)
            {
                return 0;
            }
            //بدست آوردن درصد واقعی مهارت در گتگوری کتگوری 
            int thisSkillPercent = categoryCategories.FirstOrDefault(w => w.ChildrenCatId == SkillcatId).Percent / sumAllSkilPercent * 100;
            return thisSkillPercent;
        }
        public ResultPercent UpdatePercentForVision(string UserId, int VisioncatId, bool UpdateIsIt)
        {
            try
            {
                //پیدا کردن مهارت های زیر مجموعه هدف
                var categoryCategories = _unitOfWork.Category_CategoryGR.FindBy(f => f.ParentCatId == VisioncatId).ToList();
                //جمع عدد های تعیین شده برای مهارت های هدف
                var sumAllSkilPercent = categoryCategories.Sum(s => s.Percent);
                //تمام مهارت های ثبت شده برای کاربر بدون فیلتر البته تا اینجا:)))
                var userSkills = _unitOfWork.SkillGR.FindBy(f => f.UserId == UserId).ToList();
                float newPercent = 0;
                foreach (var item in categoryCategories)
                {
                    //بدست آوردن درصد واقعی مهارت در گتگوری کتگوری 
                    var thisSkillPercent = CalculateSkillPercent(VisioncatId, item.ChildrenCatId, categoryCategories);
                    //هر مهارت کاربری که با مهرت های زیرمجموعه هدف برابر بود را با درصد اصلی جمع میزند
                    newPercent += userSkills.FirstOrDefault(f => f.CategoryId == item.ChildrenCatId).Percent * thisSkillPercent / 100;
                }

                //اگر بیرون از متد قرار نیست آپدیت شود در همینجا هدف کاربر آپدیت شود
                if (UpdateIsIt)
                {
                    var Vision = _unitOfWork.VisionGR.FirstOrDefault(f => f.UserId == UserId && f.CategoryId == VisioncatId);
                    Vision.IsUpdate = true;
                    Vision.Percent = newPercent;
                    var updateResult = _unitOfWork.VisionGR.Update(Vision);
                    return new ResultPercent()
                    {
                        Statue = Enums.Statue.Success,
                        Message = updateResult.Message,
                        NewPercent = newPercent,
                        UpdateStatuse = updateResult.Statue
                    };
                }
                return new ResultPercent()
                {
                    Statue = Enums.Statue.Success,
                    NewPercent = newPercent,
                    UpdateStatuse = Enums.Statue.AccessDenied
                };
            }
            catch (Exception e)
            {
                return new ResultPercent()
                {
                    Statue = Enums.Statue.Success,
                    Message = e.Message,
                    NewPercent = 0,
                    UpdateStatuse = Enums.Statue.Null
                };
            }


        }
        public AdviceViewModel FinancialAdviceSuggest(AdviceViewModel model)
        {

            var user = UserManager.FindByIdAsync(model.User.Id).Result;
            #region اعمال تغییرات در جدول کاربر

            if (model != null && model.User != null)
            {
                bool isUserEdited = false;

                if (model.User.SolderStatusId != null)
                {
                    user.SolderStatusId = model.User.SolderStatusId;
                    isUserEdited = true;
                }

                if (model.User.EducationalStatusId != null)
                {
                    user.EducationalStatusId = model.User.EducationalStatusId;
                    isUserEdited = true;
                }

                if (model.User.HealthStatusId != null)
                {
                    user.HealthStatusId = model.User.HealthStatusId;
                    isUserEdited = true;
                }

                if (model.User.JobStatusId != null)
                {
                    user.JobStatusId = model.User.JobStatusId;
                    isUserEdited = true;
                }

                if (model.User.Age != null)
                {
                    user.Age = model.User.Age;
                    isUserEdited = true;
                }
                if (isUserEdited)
                {
                    UserManager.UpdateAsync(user);
                }

            }

            #endregion

            var financialAdvices = _unitOfWork.FinancialAdviceGR.GetAll()
                                               .Where(f => model.User.Age != null ? ((f.AgeStart != null ? f.AgeStart <= model.User.Age : f.Id != 0) && (f.AgeEnd != null ? model.User.Age <= f.AgeEnd : f.Id != 0)) : f.AgeStart == null && f.AgeEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.FreeTime != null ? ((f.FreeTimeStart != null ? f.FreeTimeStart <= model.Vision.FreeTime : f.Id != 0) && (f.FreeTimeEnd != null ? model.Vision.FreeTime <= f.FreeTimeEnd : f.Id != 0)) : f.FreeTimeStart == null && f.FreeTimeEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.AmountOfSavings != null ? ((f.AmountOfSavingsStart != null ? f.AmountOfSavingsStart <= model.Vision.AmountOfSavings : f.Id != 0) && (f.AmountOfSavingsEnd != null ? model.Vision.AmountOfSavings <= f.AmountOfSavingsEnd : f.Id != 0)) : f.AmountOfSavingsStart == null && f.AmountOfSavingsEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.Income != null ? ((f.IncomeStart != null ? f.IncomeStart <= model.Vision.Income : f.Id != 0) && (f.IncomeEnd != null ? model.Vision.Income <= f.IncomeEnd : f.Id != 0)) : f.IncomeStart == null && f.IncomeEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.InitialInvestment != null ? ((f.InitialInvestmentStart != null ? f.InitialInvestmentStart <= model.Vision.InitialInvestment : f.Id != 0) && (f.InitialInvestmentEnd != null ? model.Vision.InitialInvestment <= f.InitialInvestmentEnd : f.Id != 0)) : f.InitialInvestmentStart == null && f.InitialInvestmentEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.EarningsGoal != null ? ((f.EarningsGoalStart != null ? f.EarningsGoalStart <= model.Vision.EarningsGoal : f.Id != 0) && (f.EarningsGoalEnd != null ? model.Vision.EarningsGoal <= f.EarningsGoalEnd : f.Id != 0)) : f.EarningsGoalStart == null && f.EarningsGoalEnd == null);
            financialAdvices = financialAdvices.Where(f => model.Vision.MonthlyInterval != null ? ((f.MonthlyIntervalStart != null ? f.MonthlyIntervalStart <= model.Vision.MonthlyInterval : f.Id != 0) && (f.MonthlyIntervalEnd != null ? model.Vision.MonthlyInterval <= f.MonthlyIntervalEnd : f.Id != 0)) : f.MonthlyIntervalStart == null && f.MonthlyIntervalEnd == null);

            financialAdvices = financialAdvices.Where(f => model.User.SolderStatusId != null ? (f.SolderStatusId != null ? f.SolderStatusId <= model.User.SolderStatusId : f.Id != 0) : f.SolderStatusId == null);
            financialAdvices = financialAdvices.Where(f => model.User.EducationalStatusId != null ? (f.SolderStatusId != null ? f.EducationalStatusId <= model.User.EducationalStatusId : f.Id != 0) : f.EducationalStatusId == null);

            financialAdvices = financialAdvices.Where(f => model.User.HealthStatusId != null ? (f.HealthStatusId != null ? f.HealthStatusId == model.User.HealthStatusId : f.Id != 0) : f.HealthStatusId == null);
            financialAdvices = financialAdvices.Where(f => model.User.JobStatusId != null ? (f.JobStatusId != null ? f.JobStatusId == model.User.JobStatusId : f.Id != 0) : f.JobStatusId == null);


            AddVision(null, financialAdvices.Select(s => s.CategoryId).ToList(), user.Id, VisionType.Financial);

            var userVisions = GetVisionVm(user.Id, VisionType.Financial);

            AdviceViewModel adviceViewModel = new AdviceViewModel();
            adviceViewModel.VisionViewModels = userVisions;

            return adviceViewModel;
        }

        public Result ChangeVisionStatus(Enums.VisionStatus visionStatus, int visionId, string UserId)
        {
            if (visionId != 0 && !String.IsNullOrEmpty(UserId))
            {
                var OldVision = _unitOfWork.VisionGR.GetById(visionId);



                if (visionStatus == Enums.VisionStatus.Selected)
                {
                    #region Financial
                    if (OldVision.VisionType == VisionType.Financial)
                    {
                        UserGeneology directorOfEducation = new UserGeneology();
                        UserGeneology developmentManager = new UserGeneology();

                        switch (OldVision.Category.CategoryFinancialType)
                        {
                            case CategoryFinancialTypeEnum.Insurance:
                                directorOfEducation = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DirectorOfEducationInsurance, SystemType.Insurance);
                                developmentManager = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DevelopmentManagerInsurance, SystemType.Insurance);
                                break;
                            case CategoryFinancialTypeEnum.Exchange:
                                directorOfEducation = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DirectorOfEducationExchange, SystemType.Exchange);
                                developmentManager = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DevelopmentManagerExchange, SystemType.Exchange);
                                break;
                            case CategoryFinancialTypeEnum.Bank:
                                directorOfEducation = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DirectorOfEducationBank, SystemType.Bank);
                                developmentManager = FindUserGeneologyForFinancialVision(UserId, GeneologyTypeEnum.DevelopmentManagerBank, SystemType.Bank);
                                break;

                            default:
                                break;
                        }

                        if (directorOfEducation != null)
                        {
                            OldVision.DirectorOfEducationUserId = directorOfEducation.UserId;
                        }
                        if (developmentManager != null)
                        {
                            OldVision.DevelopmentManagerUserId = developmentManager.UserId;
                        }

                    }


                    #endregion




                    #region ChangeToSelected

                    OldVision.VisionStatus = Enums.VisionStatus.Selected;
                    var visionUpdate = _unitOfWork.VisionGR.Update(OldVision);

                    #endregion

                    if (visionUpdate.Statue == Enums.Statue.Success)
                    {
                        #region SuggestProduct

                        var resultSuggestProduct = _suggestionRepository.SuggestProductByVisionCatId(OldVision.CategoryId, UserId);
                        #endregion

                        return resultSuggestProduct;
                    }
                    else
                    {
                        return visionUpdate;
                    }

                }
                else
                {
                    OldVision.VisionStatus = Enums.VisionStatus.Advice;
                    var visionUpdate = _unitOfWork.VisionGR.Update(OldVision);
                    if (visionUpdate.Statue == Enums.Statue.Success)
                    {
                        if (OldVision.Priority == 0)
                        {
                            #region DeleteVisionBySomeTime
                            //اگر مهارت های ثبت شده برای هدف برای کاربر وجود نداشت آن هدفش حذف شود 
                            var thisVisionSkills = _CategoryRepository.FindOneLevelChildList(OldVision.CategoryId);
                            bool haveSkill = false;
                            foreach (var skill in thisVisionSkills)
                            {
                                if (_unitOfWork.SkillGR.Any(f => f.CategoryId == skill.Id && f.UserId == OldVision.UserId))
                                {
                                    haveSkill = true;
                                }
                            }

                            if (!haveSkill)
                            {
                                 _unitOfWork.VisionGR.Delete(OldVision);
                            }
                            #endregion
                        }

                        var suggestedProducts = _unitOfWork.SuggestedProductGR
                            .FindBy(f => f.VisionCatId == OldVision.CategoryId && f.UserId == OldVision.UserId).ToList();
                        var resultSuggestProduct = _unitOfWork.SuggestedProductGR.DeleteRange(suggestedProducts);
                        return resultSuggestProduct;
                    }
                    else
                    {
                        return visionUpdate;
                    }

                }
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "موارد ارسالی صحیح نمیباشد"
                };
            }

        }

        /// <summary>
        /// پیدا کردن سرپرست توسعه و اموزش پس از فعال کردن یک هدف حاصله از مشاوره مالی 
        /// @Aspkar
        /// </summary>
        public UserGeneology FindUserGeneologyForFinancialVision(string userId, GeneologyTypeEnum type, SystemType systemType)
        {
            var user = UserManager.FindByIdAsync(userId).Result;
            var maxCapacity = _unitOfWork.ProjectSettingGR.FirstOrDefault(p => true)?.MaxCapacity;
            if (maxCapacity == null)
            {
                maxCapacity = 15;
            }

            int? userCityId = user.CityId;
            int? userProvinceId = user.City.ProvinceId;

            if (userCityId == null)
            {
                //میثم گفت اگر شهر فرد خالی بود شهر را کرج پر کنم
                userCityId = _unitOfWork.CityGR.FirstOrDefault(c => c.Name.Contains("کرج"))?.Id ?? _unitOfWork.CityGR.FirstOrDefault(c => true)?.Id;

                userProvinceId = _unitOfWork.ProvinceGR.FirstOrDefault(c => c.Name.Contains("تهران"))?.Id ?? _unitOfWork.ProvinceGR.FirstOrDefault(c => true)?.Id;
            }

            var userGeneologys = _unitOfWork.UserGeneologyGR.GetAll().Where(u => u.IsInGeneology && u.Capacity != maxCapacity && u.Geneologytype.Type == type && u.Geneologytype.SystemType == systemType).OrderBy(u => u.Capacity).ToList();

            if (userGeneologys.Any())
            {
                //مسئولی که در شهر خودش هست
                var userGeneology = userGeneologys.FirstOrDefault(u => u.CityId == userCityId);

                if (userGeneology != null)
                {
                    return userGeneology;
                }

                return userGeneologys.FirstOrDefault(u => u.City.ProvinceId == userProvinceId);
            }

            //اگر هیچ کاربری جهت ارائه یافت نشد مدیر دیفالت را معرفی می کند
            return _unitOfWork.UserGeneologyGR.FirstOrDefault(u => u.IsInGeneology && u.Geneologytype.Type == type && u.Geneologytype.SystemType == systemType && u.IsDefault);

        }
        public Result DeleteVisionForUserBySkillId(int skillCatId, string UserId)
        {
            Result result = new Result();
            //یافتن پدر های این مهارت حذف شده به منظور حذف آنها از اهداف کاربر
            var parents = _CategoryRepository.FindOneLevelParentList(skillCatId);
            List<Vision> deletvisions = new List<Vision>();
            foreach (var item in parents)
            {//یافتن هدف
                var findVision = _unitOfWork.VisionGR.FirstOrDefault(f =>
                    f.CategoryId == item.Id && f.UserId == UserId);
                if (findVision != null)
                {
                    deletvisions.Add(findVision);
                }
            }
            //حذف تمام اهداف مربوط به مهارت حذف شده
            var deletedVisionRes =  _unitOfWork.VisionGR.DeleteRange(deletvisions);
            if (deletedVisionRes.Statue == Enums.Statue.Success)
            {

                result.Messages.Add("اهداف مربوط حذف گردید");
                return result;
            }
            else
            {
                return result;
            }
        }

    }
}
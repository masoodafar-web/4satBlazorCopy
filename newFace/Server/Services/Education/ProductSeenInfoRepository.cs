using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.User;
using newFace.Server.Services.Resource;
using Microsoft.AspNetCore.Identity;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Services.Education
{
    public class ProductSeenInfoRepository : IProductSeenInfoRepository
    {
        private readonly IProductScaleRepository _productScaleRepository;
        private readonly IUserRepository _UserRepository;
        private readonly ISkillRepository _skillRepository;
        private readonly ISuggestionRepository _suggestionRepository;
        private readonly IPointRepository _pointRepository;
        private readonly IVisionRepository _visionRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public ProductSeenInfoRepository(IProductScaleRepository productScaleRepository, IUserRepository userRepository, ISkillRepository skillRepository, ISuggestionRepository suggestionRepository, IPointRepository pointRepository, IVisionRepository visionRepository, IUnitOfWork unitOfWork)
        {
            _productScaleRepository = productScaleRepository;
            _UserRepository = userRepository;
            _skillRepository = skillRepository;
            _suggestionRepository = suggestionRepository;
            _pointRepository = pointRepository;
            _visionRepository = visionRepository;
            _unitOfWork = unitOfWork;
        }

        public Result Create(ProductSeenInfo ProductSeeninfo)
        {
            Result result = new Result();
            if (ProductSeeninfo.UserId == null)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا وارد شوید";
                return result;
            }

            if (ProductSeeninfo.ProductId == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه محصول را ارسال کنید";
                return result;
            }

            try
            {

                _unitOfWork.ProductSeenInfoGR.Add(ProductSeeninfo);
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public Result Edit(ProductSeenInfo ProductSeeninfo)
        {
            Result result = new Result();



            if (ProductSeeninfo.UserId == null)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا وارد شوید";
                return result;
            }

            if (ProductSeeninfo.ProductId == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه محصول را ارسال کنید";
                return result;
            }
            try
            {

                _unitOfWork.ProductSeenInfoGR.Update(ProductSeeninfo);
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public ResultProductSeeninfo GetById(int? Id)
        {
            ResultProductSeeninfo Result = new ResultProductSeeninfo();
            try
            {
                if (Id != null)
                {
                    ProductSeenInfo ProductSeeninfo = _unitOfWork.ProductSeenInfoGR.GetById(Id.Value);
                    if (ProductSeeninfo != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.ProductSeeninfo = ProductSeeninfo;
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "ارسال نشد";
                        return Result;
                    }

                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "آی دی دریافت نشد";
                    return Result;
                }

            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;

            }
        }

        public Result Delete(int? Id)
        {
            Result result = new Result();
            try
            {
                if (Id != null)
                {
                    _unitOfWork.ProductSeenInfoGR.Delete(_unitOfWork.ProductSeenInfoGR.GetById(Id.Value));
                    result.Statue = Enums.Statue.Success;
                    result.Message = "";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "آی دی دریافت نشد";
                    return result;
                }
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }


        public ResultProductSeeninfo GetAll()
        {
            ResultProductSeeninfo result = new ResultProductSeeninfo();
            try
            {

                List<ProductSeenInfo> ProductSeeninfoList = _unitOfWork.ProductSeenInfoGR.GetAll().ToList();
                if (ProductSeeninfoList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ProductSeeninfoes = ProductSeeninfoList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.ProductSeeninfoes = ProductSeeninfoList;
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public bool IsReadBefore(int productId, string userId)
        {

            return _unitOfWork.ProductSeenInfoGR.FindBy(p => p.ProductId == productId && p.UserId == userId && p.IsComplete).Any();

        }

        public double CompleteReadProduct(int productId, string userId, Enums.StatusTypeQuestion? StatusTypeQuestion)
        {
            try
            {
                int credit = 0, point = 0, totalCredit = 0;
                var productScales = _unitOfWork.ProductScaleGR.GetAllIncluding(null, i => i.Category, g => g.Levels).ToList();
                var UserSkill = _unitOfWork.SkillGR.FindBy(f => f.UserId == userId).ToList();
                var AllProductSeenInfo = _unitOfWork.ProductSeenInfoGR.FindBy(f => f.UserId == userId).ToList();
                //اگر کاربر قبلا محصول را نخوانده بود 
                if (!IsReadBefore(productId, userId))
                {

                    foreach (var productScale in productScales.Where(f => f.ProductId == productId).ToList())
                    {
                        //بدست آوردن لول کاربر
                        var userskillLvl = UserSkill.FirstOrDefault(w => w.CategoryId == productScale.CatId);
                        //محاسبه امتیاز و اعتبار بر حسب سازگاری لول کاربر و لول محصول
                        if (userskillLvl.LevelId == productScale.LevelId)
                        {
                            credit = productScale.Credit;
                            point = productScale.Point;

                        }
                        else
                        {
                            credit = 0;
                            point = productScale.Point / 2;
                        }
                        //اگر محصول ،آزمون نبود؟ از طریق آزمون تعیین سطح کامل شد امتیازی نمیگیرد
                        if (StatusTypeQuestion == Enums.StatusTypeQuestion.Rating && productScale.Product.Type != ProductType.Exam)
                        {
                            point = 0;
                        }
                        //تغییر اعتبار و امتیاز کاربر
                        var user = UserManager.FindByIdAsync(userId).Result;
                        user.Credit += credit;
                        user.Point += point;
                        var resultCredit = UserManager.UpdateAsync(user).Result;

                        if (resultCredit.Succeeded)
                        {
                            //تغییر وضعیت محصول به خوانده شده 
                            var productSeenInfo = AllProductSeenInfo.FirstOrDefault(p => p.ProductId == productId);
                            productSeenInfo.Credit = credit;
                            productSeenInfo.Point = point;
                            productSeenInfo.IsComplete = true;

                            var rr = _unitOfWork.ProductSeenInfoGR.Update(productSeenInfo);
                            if (rr.Statue == Enums.Statue.Success)
                            {
                                #region levelUp
                                // تمام محصولاتی که زیر مجموعه این مهارت هستند و لول آنها سازگار است
                                var AllProductsFromSkill = productScales.Where(w => w.CatId == userskillLvl.CategoryId && w.LevelId == userskillLvl.LevelId).ToList();
                                bool IsComplet = false;
                                foreach (var ProductsFromSkill in AllProductsFromSkill)
                                { //تمام محصولات مورد نظر در لیست خوانده شده های کاربر وجود دارد 
                                    IsComplet = AllProductSeenInfo.Where(w => w.ProductId == ProductsFromSkill.ProductId && w.IsComplete && w.Credit > 0)
                                        .Any();
                                }
                                #endregion

                                #region CreatPercent
                                //درصد جدید مهارت جاری
                                var resultPercent = _UserRepository.CreatePercentSkill(userId, userskillLvl.CategoryId, productScales,
                                    AllProductSeenInfo, false);
                                float UserPercent = resultPercent.NewPercent;
                                #endregion
                                var categorie = productScale.Category;
                                if (categorie != null)
                                {
                                    //ویرایش مهارت کاربر
                                    var skill = _unitOfWork.SkillGR.FirstOrDefault(a =>
                                        a.CategoryId == categorie.Id && a.UserId == userId);
                                    if (skill != null)
                                    {
                                        skill.Credit += (long)credit;
                                        skill.Point += (long)point;
                                        //اگر آزمون تعیین سطح بود مهارت آن ،فیلد >>|آزمون تعیین سطحش| برای کاربر1 شود یعنی این کاربر نباید بتواند 
                                        //دوباره آزمون تعیین سطح بدهد
                                        if (StatusTypeQuestion == Enums.StatusTypeQuestion.Rating)
                                        {
                                            skill.IsPassRatingExam = true;
                                        }
                                        if (IsComplet && skill.LevelId < 5)
                                        {
                                            skill.LevelId++;
                                            if (skill.LevelId == 2)
                                            {
                                                skill.Lvl2 = true;
                                            }
                                            else if (skill.LevelId == 3)
                                            {
                                                skill.Lvl3 = true;
                                            }
                                            else if (skill.LevelId == 4)
                                            {
                                                skill.Lvl4 = true;
                                            }

                                        }

                                        float OldPercent = skill.Percent;
                                        skill.Percent += UserPercent;
                                        skill.IsUpdate = true;
                                        _unitOfWork.SkillGR.Update(skill);
                                        _visionRepository.ChangeVisionPriorityBySkillChange(skill.Percent, OldPercent,
                                            skill.UserId, skill.CategoryId);
                                    }
                                }
                                //حذف محصولات کامل شده از محصولات پیشنهادی محظ اطمینان وگرنه هنگام خرید محصول این کار انجام میشود
                                var delete = _suggestionRepository.DeleteProductFromSuggestion(userId, productId);

                                totalCredit += credit;
                            }
                        }




                    }
                    return totalCredit;
                }
                //اگرقبلا خوانده بود 
                //تکراری خوانده
                else
                {
                    foreach (var productScale in productScales.Where(f => f.ProductId == productId).ToList())
                    {
                        //بدست آوردن لول کاربر

                        var userskillLvl = UserSkill.FirstOrDefault(w => w.CategoryId == productScale.CatId);
                        //اطلاعات ثبت شده قبلی
                        var oldProductSeenInfo = AllProductSeenInfo.FirstOrDefault(p => p.ProductId == productId);
                        //اگر قبلا اعتبار صفر گرفته بود 
                        if (oldProductSeenInfo.Credit == 0)
                        {
                            //اگر قبلا تقسیم بر 2 شده یعنی هم لول نبوده  و حالا باید بازم تقسیم بر 2 بشه تا کامل بشه وگرنه باید همه امتیاز یکجا بگیره
                            int div = oldProductSeenInfo.Point == productScale.Point / 2 ? 2 : 0;
                            //محاسبه امتیاز و اعتبار بر حسب سازگاری لول کاربر و لول محصول
                            if (userskillLvl.LevelId == productScale.LevelId)
                            {
                                credit = productScale.Credit;
                                point = productScale.Point / div;
                            }
                            else
                            {
                                credit = 0;
                                point = 0;
                            }
                        }

                        //اگر محصول ،آزمون نبود؟ از طریق آزمون تعیین سطح کامل شد امتیازی نمیگیرد
                        if (StatusTypeQuestion == Enums.StatusTypeQuestion.Rating && productScale.Product.Type != ProductType.Exam)
                        {
                            point = 0;
                        }
                        //تغییر اعتبار و امتیاز کاربر
                        var user = UserManager.FindByIdAsync(userId).Result;
                        user.Credit += credit;
                        user.Point += point;
                        var resultCredit = UserManager.UpdateAsync(user).Result;

                        if (resultCredit.Succeeded)
                        {
                            //تغییر مقادیر اعتبار و امتیاز 
                            var productSeenInfo = AllProductSeenInfo.FirstOrDefault(p => p.ProductId == productId);
                            productSeenInfo.Credit = credit;
                            productSeenInfo.Point = point;

                            var rr = _unitOfWork.ProductSeenInfoGR.Update(productSeenInfo);
                            if (rr.Statue == Enums.Statue.Success)
                            {
                                #region levelUp
                                // تمام محصولاتی که زیر مجموعه این مهارت هستند و لول آنها سازگار است
                                var AllProductsFromSkill = productScales.Where(w => w.CatId == userskillLvl.CategoryId && w.LevelId == userskillLvl.LevelId).ToList();
                                bool IsComplet = false;
                                foreach (var ProductsFromSkill in AllProductsFromSkill)
                                { //تمام محصولات مورد نظر در لیست خوانده شده های کاربر وجود دارد 
                                    IsComplet = AllProductSeenInfo.Where(w => w.ProductId == ProductsFromSkill.ProductId && w.IsComplete && w.Credit > 0)
                                        .Any();
                                }
                                #endregion

                                #region CreatPercent
                                //درصد جدید مهارت جاری
                                var resultPercent = _UserRepository.CreatePercentSkill(userId, userskillLvl.CategoryId, productScales,
                                    AllProductSeenInfo, false);
                                float UserPercent = resultPercent.NewPercent;
                                #endregion
                                var categorie = productScale.Category;
                                if (categorie != null)
                                {
                                    //ویرایش مهارت کاربر
                                    var skill = _unitOfWork.SkillGR.FirstOrDefault(a =>
                                        a.CategoryId == categorie.Id && a.UserId == userId);
                                    if (skill != null)
                                    {
                                        skill.Credit += (long)credit;
                                        skill.Point += (long)point;
                                        //اگر آزمون تعیین سطح بود مهارت آن ،فیلد >>|آزمون تعیین سطحش| برای کاربر1 شود یعنی این کاربر نباید بتواند 
                                        //دوباره آزمون تعیین سطح بدهد
                                        if (StatusTypeQuestion == Enums.StatusTypeQuestion.Rating)
                                        {
                                            skill.IsPassRatingExam = true;
                                        }
                                        if (IsComplet && skill.LevelId < 4)
                                        {
                                            skill.LevelId++;
                                            if (skill.LevelId == 2)
                                            {
                                                skill.Lvl2 = true;
                                            }
                                            else if (skill.LevelId == 3)
                                            {
                                                skill.Lvl3 = true;
                                            }
                                            else if (skill.LevelId == 4)
                                            {
                                                skill.Lvl4 = true;
                                            }

                                        }
                                        skill.Percent += UserPercent;
                                        skill.IsUpdate = true;
                                        _unitOfWork.SkillGR.Update(skill);
                                    }
                                }
                                //حذف محصولات کامل شده از محصولات پیشنهادی محظ اطمینان وگرنه هنگام خرید محصول این کار انجام میشود

                                var delete = _suggestionRepository.DeleteProductFromSuggestion(userId, productId);

                                totalCredit += credit;
                            }
                        }

                    }

                }
                return totalCredit;
            }
            catch (Exception)
            {
                return 0;
            }



        }


        public bool CompleteVideo(int videoId, string userId)
        {
            var videoSeenInfo = _unitOfWork.VideoSeenInfoGR.GetSingleIncluding(v => v.UserId == userId && v.VideoId == videoId, vv => vv.Video);

            if (videoSeenInfo != null)
            {

                videoSeenInfo.IsComplete = true;

                var result = _unitOfWork.VideoSeenInfoGR.Update(videoSeenInfo);

                var videoSeenInfosNotComplete = _unitOfWork.VideoSeenInfoGR.GetAllIncluding(v => v.Video.CourseId == videoSeenInfo.Video.Id && v.IsComplete == false && v.UserId == userId, vv => vv.Video).ToList();

                if (!videoSeenInfosNotComplete.Any())
                {
                    CompleteReadProduct(videoSeenInfo.Video.Courses.ProductId, userId, null);
                }

                return (result.Statue == Enums.Statue.Success);
            }
            else
            {
                return false;
            }


        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.User;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Models.ViewModels.UserProfileViewModel;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Identity;
using newFace.Shared;


namespace newFace.Server.Services
{
    public class UserRepository : IUserRepository
    {

     
        private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public UserRepository(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }
   

        //----------------------------------------------------

        #region EditBasic&SocialNetwork

        //----------------------------------------------------

        public ResultUser GetById(string id)
        {
            ResultUser Result = new ResultUser();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.Id == id);
                    if (user != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.User = user;
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.AccessDenied;
                        Result.Message = "کاربری با این داستان یافت نشد";
                        return Result;
                    }

                }
                else
                {
                    Result.Statue = Enums.Statue.AccessDenied;
                    Result.Message = "شناسه دریافت نشد";
                    return Result;
                }

            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.AccessDenied;
                Result.Message = e.Message;
                return Result;

            }
        }

        public ResultUser GetByUserName(string userName)
        {
            ResultUser Result = new ResultUser();
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.UserName == userName);
                    if (user != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.User = user;
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.AccessDenied;
                        Result.Message = "کاربری با این داستان یافت نشد";
                        return Result;
                    }

                }
                else
                {
                    Result.Statue = Enums.Statue.AccessDenied;
                    Result.Message = "شناسه دریافت نشد";
                    return Result;
                }

            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.AccessDenied;
                Result.Message = e.Message;
                return Result;

            }
        }

        //----------------------------------------------------

        public ResultUser GetByToken(string id)
        {
            ResultUser Result = new ResultUser();
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.SecurityStamp == id);
                    if (user != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.User = user;
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.AccessDenied;
                        Result.Message = "کاربری با این داستان یافت نشد";
                        return Result;
                    }

                }
                else
                {
                    Result.Statue = Enums.Statue.AccessDenied;
                    Result.Message = "داستان دریافت نشد";
                    return Result;
                }

            }
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.AccessDenied;
                Result.Message = e.Message;
                return Result;

            }
        }
        //public ResultUser GetByUserName(string UserName)
        //{
        //    ResultUser Result = new ResultUser();
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(UserName))
        //        {
        //            ApplicationUser user = UserManager.Users.Include(i => i.City.Province.Country).Include(i => i.Post).Include(i => i.Post.Select(p => p.Category)).Include(i => i.Post.Select(p => p.Levels)).Include(i => i.Post.Select(p => p.Comment)).Include(i => i.Post.Select(p => p.Comment.Select(u => u.CommentsChilds))).Include(i => i.Skills).Include(i => i.Skills.Select(j => j.Category)).FirstOrDefault(p => p.UserName == UserName);
        //            if (user != null)
        //            {
        //                Result.Statue = Enums.Statue.Success;
        //                Result.Message = "با موفقیت ارسال شد";
        //                Result.User = user;
        //                return Result;
        //            }
        //            else
        //            {
        //                Result.Statue = Enums.Statue.AccessDenied;
        //                Result.Message = "کاربری با این داستان یافت نشد";
        //                return Result;
        //            }

        //        }
        //        else
        //        {
        //            Result.Statue = Enums.Statue.AccessDenied;
        //            Result.Message = "داستان دریافت نشد";
        //            return Result;
        //        }

        //    }
        //    catch (System.Exception e)
        //    {
        //        Result.Statue = Enums.Statue.Failure;
        //        Result.Message = e.Message;
        //        return Result;

        //    }
        //}

        //----------------------------------------------------
        public Result IsEmailConfirm(string UserId)
        {
            Result Result = new Result();

            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "ای دی دریافت نشد";
                    return Result;
                }
                var isConfirmEmail = UserManager.Users.FirstOrDefault(p => p.Id == UserId).EmailConfirmed;

                Result.Statue = Enums.Statue.Success;
                Result.Message = "";
                return Result;
            }
            catch (Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;
            }
        }
        //----------------------------------------------------
      
  
        //public Result DeleteUser(string id)
        //{
        //    Result Result = new Result();

        //    try
        //    {
        //        if (string.IsNullOrEmpty(id))
        //        {
        //            Result.Statue = Enums.Statue.Failure;
        //            Result.Message = "ای دی دریافت نشد";
        //            return Result;
        //        }
        //        var user = UserManager.Users.FirstOrDefault(p => p.Id == id);

        //        if (user != null)
        //        {
        //            Result.Statue = Enums.Statue.Failure;
        //            Result.Message = "یافت نشد";
        //            return Result;
        //        }

        //        UserManager.Delete(user);
        //        Result.Statue = Enums.Statue.Success;
        //        Result.Message = "";
        //        return Result;
        //    }
        //    catch (Exception e)
        //    {
        //        Result.Statue = Enums.Statue.Failure;
        //        Result.Message = e.Message;
        //        return Result;
        //    }
        //}

        //----------------------------------------------------
        public Result EditBasicInfo(ApplicationUser user)
        {
            Result result = new Result();
            try
            {
                if (string.IsNullOrEmpty(user.Id))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = " ای دی کاربر دریافت نشد";
                }

                ApplicationUser userForEdit =UserManager.FindByIdAsync(user.Id).Result;

                if (userForEdit == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "کاربری با این مشخصات یافت نشد";
                    return result;
                }
                else
                {
                    //فقط فیلد های قسمت پروفایل
                    userForEdit.FullName = user.FullName;
                    userForEdit.UserName = user.UserName;
                    userForEdit.Address = user.Address;
                    userForEdit.BirthDate = user.BirthDate;
                    userForEdit.CityId = user.CityId==0?(int?)null: user.CityId;
                    userForEdit.GenderId = user.GenderId;
                    userForEdit.Img = user.Img;
                    userForEdit.JobStatusId = user.JobStatusId;
                    userForEdit.MaritalStatusId = user.MaritalStatusId;
                    userForEdit.PhoneNumber = user.PhoneNumber;
                    userForEdit.Phone = user.Phone;
                    userForEdit.NationalCode = user.NationalCode;
                    userForEdit.NickName = user.NickName;
                    userForEdit.PhoneNumber = user.PhoneNumber;
                    userForEdit.SolderStatusId = user.SolderStatusId;
                    userForEdit.AboutMe = user.AboutMe;
                    userForEdit.HealthStatusId = user.HealthStatusId;

                    var Res= UserManager.UpdateAsync(userForEdit).Result;
                    result.Statue = Res.Succeeded ? Enums.Statue.Success : Enums.Statue.Failure;
                    if (Res.Succeeded == true)
                    {
                        result.Message = "عملیات با موفقیت انجام شد.";
                    }
                    else
                    {
                        List<string> Errors = new List<string>();
                        foreach (var Error in Res.Errors)
                        {
                            if (Error.Description.StartsWith("Name"))
                            {
                                Errors.Add("نام کاربری تکراری است");
                            }
                            Errors.Add(Error.Description);
                        }
                        result.Message = String.Join(". ", Errors);
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }
        }
        //----------------------------------------------------
        public Result EditSocialNetwork(ApplicationUser user)
        {
            Result result = new Result();

            try
            {
                if (string.IsNullOrEmpty(user.Id))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = " ای دی کاربر دریافت نشد";
                }

                ApplicationUser userForEdit = UserManager.FindByIdAsync(user.Id).Result;

                if (userForEdit == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "کاربری با این مشخصات یافت نشد";
                }

                userForEdit.Email = user.Email;
                userForEdit.GooglePlus = user.GooglePlus;
                userForEdit.WebSite = user.WebSite;
                userForEdit.GitHub = user.GitHub;
                userForEdit.LinkedIn = user.LinkedIn;
                userForEdit.Instageram = user.Instageram;
                userForEdit.Telegram = user.Telegram;
                userForEdit.Twitter = user.Twitter;
                userForEdit.AboutMe = user.AboutMe;

                UserManager.UpdateAsync(userForEdit);

                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }

        }
        #endregion
        //----------------------------------------------------

        #region HelperMethod

        public Result EditCredit(string UserId, double Credit)
        {
            Result result = new Result();

            try
            {
                if (string.IsNullOrEmpty(UserId))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = " ای دی کاربر دریافت نشد";
                }

                ApplicationUser userForEdit = UserManager.FindByIdAsync(UserId).Result;

                if (userForEdit == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "کاربری با این مشخصات یافت نشد";
                }

                userForEdit.Credit = userForEdit.Credit + Credit;


                UserManager.UpdateAsync(userForEdit);

                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }
        }


        public Result EditUser(ProfileEditViewModel pevm)
        {
            Result result = new Result();

            if (string.IsNullOrEmpty(pevm.UserId))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه را ارسال کنید";
                return result;
            }

            var user = GetById(pevm.UserId).User;
            user.CityId = pevm.CityId;
            user.GenderId = pevm.GenderId;
            user.MaritalStatusId = pevm.MaritalStatusId;
            user.SolderStatusId = pevm.SolderStatusId;
            user.JobStatusId = pevm.JobStatusId;
            user.Address = pevm.Address;
            user.GenderId = pevm.GenderId;
            user.FullName = pevm.FullName;
            user.PhoneNumber = pevm.PhoneNumber;
            user.Phone = pevm.Phone;
            user.NickName = pevm.NickName;
            user.NationalCode = pevm.NationalCode;
            user.Email = pevm.Email;
            if (pevm.BirthDate != null)
            {
                try
                {

                    user.BirthDate = ConvertDate.ToDateTime(pevm.BirthDate);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }

            UserManager.UpdateAsync(user);
            result.Statue = Enums.Statue.Success;
            return result;

        }

        #endregion

        #region UserInfoComplatePrcent
        //تغییر درصد تکمیل پروفایل
        public void ChangeUserInfoComplatePercent(string userId, double complatePercent)
        {
            var beforUser = UserManager.FindByIdAsync(userId).Result;
            beforUser.UserInfoComplatePercent = beforUser.UserInfoComplatePercent + complatePercent;
            UserManager.UpdateAsync(beforUser);
        }

        #endregion


        public UserNameResult GetAllUsersUserName(string text)
        {
            UserNameResult result = new UserNameResult();
            try
            {


                if (!string.IsNullOrEmpty(text))
                {
                    var userListquery = UserManager.Users.Where(p => p.UserName.Contains(text) || p.FullName.Contains(text)).Select(p => new UserNameAndUserId
                    {
                        UserName = p.UserName,
                        UserFullName = p.FullName,
                        UserAbout = p.AboutMe,
                        UserImage = p.Img,
                        UserId = p.Id
                    }).ToList();
                    if (userListquery.Any())
                    {
                        result.Statue = Enums.Statue.Success;
                        result.Message = "با موفقیت ارسال شد";
                        result.UserNameAndUserIds = userListquery;
                        return result;
                    }
                    else
                    {
                        result.Statue = Enums.Statue.NullList;
                        result.Message = "موردی یافت نشد!!";
                        result.UserNameAndUserIds = userListquery;
                        return result;
                    }


                }
                else
                {
                        result.Statue = Enums.Statue.NullList;
                        result.Message = "موردی یافت نشد!!";
                        result.UserNameAndUserIds = new List<UserNameAndUserId>();
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

        //حذف کاربرانی که مدت زیادی است حساب ساختن ولی فعال نکردن
        public bool RemoveFakeUsers()
        {
            try
            {
                var yesterday = DateTime.Now.AddDays(-1);

                var fakeUsers = UserManager.Users.Where(u => !u.PhoneNumberConfirmed && u.CDate != null && u.CDate.Value < yesterday).ToList();
                foreach (var fakeUser in fakeUsers)
                {
                    var ugeneology = _unitOfWork.UserGeneologyGR.GetAllIncluding(g => g.UserId == fakeUser.Id || g.ParentId==fakeUser.Id).ToList();
                    var usersetting = _unitOfWork.UserSettingGR.GetAllIncluding(g => g.UserId == fakeUser.Id).ToList();


                    _unitOfWork.UserGeneologyGR.DeleteRange(ugeneology);
                    _unitOfWork.UserSettingGR.DeleteRange(usersetting);
                    UserManager.DeleteAsync(fakeUser);


                }

                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public ResultPercent CreatePercentSkill(string UserId, int SkillCatId, List<ProductScale> productScales, List<ProductSeenInfo> AllProductSeenInfo, bool UpdateIsIt)
        {
            try
            {
                if (productScales == null && AllProductSeenInfo == null)
                {
                    productScales = _unitOfWork.ProductScaleGR.GetAllIncluding(null, i => i.Category,g=>g.Levels).ToList();
                    AllProductSeenInfo = _unitOfWork.ProductSeenInfoGR.FindBy(f => f.UserId == UserId).ToList();
                }
                #region CreatPercent
                //یافتن تمام محصولاتی که برای این مهارت وجود دارد
                var FilteredProductScale = productScales.Where(w => w.CatId == SkillCatId).ToList();
                /// مجموع کل اعتبار محصولات
                double SumAllCreditSkill = FilteredProductScale.Sum(s => s.Credit);
                double readProduct = 0;
                foreach (var products in FilteredProductScale)
                {
                    //مجموعه اعتبار هایی که برای محصولات خوانده شده کاربر ثبت شده 
                    readProduct += AllProductSeenInfo.FirstOrDefault(f => f.UserId == UserId && f.ProductId == products.ProductId && f.IsComplete && f.Credit > 0) != null
                        ? AllProductSeenInfo.FirstOrDefault(f => f.UserId == UserId && f.ProductId == products.ProductId && f.IsComplete).Credit : 0;
                }
                //درصد گرفتن از جزء به کل 
                int newPercent = (int)(readProduct / SumAllCreditSkill * 100);
                //اگر بیرون از متد قرار نیست آپدیت شود در همینجا مهارت کاربر آپدیت شود
                if (UpdateIsIt)
                {
                    var skill = _unitOfWork.SkillGR.FirstOrDefault(f => f.UserId == UserId && f.CategoryId == SkillCatId);
                    skill.IsUpdate = true;
                    skill.Percent = newPercent;
                    var updateResult = _unitOfWork.SkillGR.Update(skill);
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

                #endregion
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

        public Result InsertUserCategory(int categoryId,string UserId)
        {
            List<UserCategory> userCategories=new List<UserCategory>();
            if (categoryId!=0 && !String.IsNullOrEmpty(UserId))
            {
                //یافتن تمام پدر های گتگوری ورودی
                var allParent = _categoryRepository.FindAllParentList(categoryId, true).ToList();
                foreach (var item in allParent)
                {
                    //اگر قبلا در دسته های ذخیره شده کاربر وجود نداشت 
                    if (!_unitOfWork.UserCategoryGR.Any(a => a.CategoryId == item.Id && a.UserId == UserId))
                    {
                        userCategories.Add(new UserCategory()
                        {
                            CategoryId = item.Id,
                            UserId = UserId
                        });
                    }
                    }
                
                var result = _unitOfWork.UserCategoryGR.AddRange(userCategories);
                return result;
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "مقادیر ورودی اشتبا می باشد"
                };
            }

        }


            
        public CVViewModel GetCV(string userId)
        {
            CVViewModel model = new CVViewModel();

            
            model.User = UserManager.Users.FirstOrDefault(u => u.Id == userId);

            var jobResumes = _unitOfWork.JobResumeGR.FindBy(f => f.UserId == userId).ToList();
            if (jobResumes.Count > 0)
            {
                model.JobResumes.AddRange(jobResumes.ConvertJobResumesToJobResumeVMs());
            }

            var educationalRecords = _unitOfWork.EducationalRecordGR.GetAllIncluding(f => f.UserId == userId, i => i.University).ToList();
            if (educationalRecords.Count > 0)
            {

                model.EducationalRecords.AddRange(educationalRecords.ConvertEducationalsToEducationalVMs());
            }

            model.Skills = _unitOfWork.SkillGR.GetAllIncluding(f => f.UserId == userId, i => i.Level).ToList();

            var workSamples = _unitOfWork.WorkSampleGR.FindBy(f => f.UserId == userId).ToList();
            if (workSamples.Count > 0)
            {
                model.WorkSamples.AddRange(workSamples.ConvertWorkSamplesToWorkSampleVMs());
            }

            return model;
        }

    }
 
}
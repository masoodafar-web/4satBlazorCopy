using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using newFace.Utility;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using Microsoft.EntityFrameworkCore;

namespace newFace.Controllers.Api
{
    [ApiController]
    public class AccountController : Controller
    {
        ModelStateHelper modelStateHelper = new ModelStateHelper();
        private readonly ISendRepository _sendRepository;
        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;

        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;

        private IUnitOfWork _unitOfWork;
        //private readonly IService<ProjectSetting> _ProjectSettingService;
        //private readonly IService<Skill> _SkillService;
        public AccountController(ISendRepository sendRepository, IUserRepository userRepository, IFileRepository fileRepository, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUnitOfWork unitOfWork)
        {
            _sendRepository = sendRepository;
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            UserManager = userManager;
            SignInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [HttpPost, Route("api/GetUserSSToken")]
        public ResultUser GetUserSecurityStampToken([FromBody] Request model)
        {
            ResultUser Result = new ResultUser();

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            ApplicationUser user = UserManager.Users.FirstOrDefault(u => u.UserName == model.UserName);

            if (user != null)
            {
                Result.Statue = Enums.Statue.Success;
                Result.Message = "ورود با موفقیت انجام شد.";
                Result.UserVM = user.ConvertUserToUserVm();
                return Result;
            }
            else
            {
                Result.Message = "کاربری با این مشخصات یافت نشد";
                return Result;
            }


        }

        #region Login
        [HttpPost, Route("api/Login")]
        public ResultUser Login([FromBody] LoginViewModel model)
        {
            ResultUser Result = new ResultUser();
            if (!ModelState.IsValid)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Messages = modelStateHelper.GetModelStateErrors(ModelState);

                return Result;
            }
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true

            ApplicationUser user = UserManager.Users.FirstOrDefault(u => u.PhoneNumber == model.UserName || u.UserName == model.UserName || u.Email == model.UserName);

            if (user != null)
            {
                SignInResult result = SignInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false).Result;

                if (result.Succeeded)
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "ورود با موفقیت انجام شد.";
                    Result.UserVM = user.ConvertUserToUserVm();
                    return Result;
                }
                if (result.RequiresTwoFactor)
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "اوه اوه ورود شما دو مرحله ای است لطفا این مورد را گزارش دهید.";
                    return Result;
                }

                if (result.IsNotAllowed)
                {
                    if (!user.PhoneNumberConfirmed)
                    {
                        // Send sms Message
                        var code = UserManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                        var message = new IdentityMessage
                        {
                            Destination = user.PhoneNumber,
                            Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                        };
                        _sendRepository.Sms(message.Destination, message.Body);

                        Result.Statue = Enums.Statue.AccessDenied;
                        Result.Message = "شماره همراه شما تایید نشده است.";
                        Result.UserVM = user.ConvertUserToUserVm();
                        return Result;
                    }
                    else
                    {
                        Result.Statue = Enums.Statue.Failure;
                        Result.Message = "اوه اوه ایمل شما تایید نشده است!!";
                        return Result;
                    }

                }
                if (result.IsLockedOut)
                {
                    Result.Statue = Enums.Statue.LockedOut;
                    Result.Message = "کاربر مورد نظر تحریم شده است.";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "ورود با خطا مواجه شد اطالاعات ورود به ویژه کلمه عبور را بررسی کنید ";
                    return Result;
                }
            }
            else
            {
                Result.Message = "کاربری با این مشخصات یافت نشد";
                return Result;
            }


        }
        #endregion

        #region AddPhoneNumber

        [HttpPost, Route("api/AddPhoneNumber")]
        public Result AddPhoneNumber([FromBody] VerifyPhoneNumberViewModel model)
        {
            ResultUser Result = new ResultUser();
            ModelState.Remove("model.Code");
            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Token))
            {
                var resultUser = _userRepository.GetByToken(model.Token);
                if (resultUser.Statue != Enums.Statue.Success)
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = resultUser.Message
                    };
                }
                model.UserId = resultUser.User.Id;
                var code = UserManager.GenerateChangePhoneNumberTokenAsync(resultUser.User, model.PhoneNumber);
                var message = new IdentityMessage
                {
                    Destination = model.PhoneNumber,
                    Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                };
                if (_sendRepository.Sms(message.Destination, message.Body))
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Success,
                        Message = "کد امنیتی با موفقیت ساخته و ارسال شد."
                    };
                }
                else
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "متاسفانه ارسال کد امنیتی با مشکل مواجه شد"
                    };
                }

            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات صحیح نمی باشد",
                    Messages = modelStateHelper.GetModelStateErrors(ModelState)

                };
            }

        }

        #endregion

        #region phonNamberCodeCheck
        [HttpPost, Route("api/PhoneNumberVerify")]
        public Result PhoneNumberCheckCode([FromBody] VerifyPhoneNumberViewModel model)
        {
            ResultUser Result = new ResultUser();

            if (ModelState.IsValid && !string.IsNullOrEmpty(model.Token))
            {
                var resultUser = _userRepository.GetByToken(model.Token);
                if (resultUser.Statue != Enums.Statue.Success)
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = resultUser.Message
                    };
                }
                model.UserId = resultUser.User.Id;

                //موقت !!تا پنل اس ام اس راه اندازی شود
                model.Code = UserManager.GenerateChangePhoneNumberTokenAsync(resultUser.User, resultUser.User.PhoneNumber).Result;

                IdentityResult result1 = UserManager.ChangePhoneNumberAsync(resultUser.User, model.PhoneNumber, model.Code).Result;
                if (result1.Succeeded)
                {
                    if (model.ForgotPassword == true)
                    {
                        string CodeToken = UserManager.GeneratePasswordResetTokenAsync(resultUser.User).Result;

                        return new ResultUser()
                        {
                            Statue = Enums.Statue.Success,
                            UserVM = UserManager.FindByIdAsync(resultUser.User.Id).Result.ConvertUserToUserVm(),
                            Message = "تایید موبایل موفقیت آمیز بود",
                            CodeToken = CodeToken
                        };
                    }
                    else
                    {
                        return new ResultUser()
                        {
                            Statue = Enums.Statue.Success,
                            UserVM = UserManager.FindByIdAsync(resultUser.User.Id).Result.ConvertUserToUserVm(),
                            Message = "تایید موبایل موفقیت آمیز بود"
                        };
                    }


                }
                else
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "اطلاعات صحیح نمی باشد"
                    };
                }
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات صحیح نمی باشد",
                    Messages = modelStateHelper.GetModelStateErrors(ModelState)

                };
            }

        }
        #endregion

        #region phonNamberCodeSend
        [HttpPost, Route("api/SendVerifyCode")]

        public Result SendVerifyCode([FromBody] VerifyPhoneNumberViewModel model)
        {

            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                var resultUser = UserManager.Users.FirstOrDefault(f => f.PhoneNumber == model.PhoneNumber);
                if (resultUser == null)
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "کاربری با این شماره همراه وجود ندارد"
                    };
                }

                //Send sms Message
                var code = UserManager.GenerateChangePhoneNumberTokenAsync(resultUser, resultUser.PhoneNumber);
                var message = new IdentityMessage
                {
                    Destination = resultUser.PhoneNumber,
                    Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                };

                if (_sendRepository.Sms(message.Destination, message.Body))
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Success,
                        Message = "کد تایید ارسال شد"
                    };
                }
                else
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "خطا در ارسال کد تایید"
                    };
                }
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات صحیح نمی باشد",
                    Messages = modelStateHelper.GetModelStateErrors(ModelState)

                };
            }

        }
        #endregion

        #region Register
        [HttpPost, Route("api/Register")]

        public ResultUser Register(RegisterViewModel model)
        {
            ResultUser Result = new ResultUser();

            var removeResult = _userRepository.RemoveFakeUsers();
            if (!removeResult)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Messages.Add(" حذف کاربران فیک با خطا همراه بود");
                return Result;
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.Email.Split('@')[0], Email = model.Email, FullName = model.FullName, Phone = model.PhoneNumber, PhoneNumber = model.PhoneNumber };
                user.Img = "/Content/img/default_logo.jpg";
                user.NickName = "نام مستعار";
                user.AboutMe = "درباره من";
                IdentityResult result = UserManager.CreateAsync(user, model.Password).Result;
                bool IsPhoneAlreadyRegistered = UserManager.Users.Any(item => item.PhoneNumber == model.PhoneNumber);
                //if (IsPhoneAlreadyRegistered)
                //{
                //    Result.Statue = Enums.Statue.Failure;
                //    Result.Messages.Add("شماره موبایل شما تکراری است");
                //    return Result;
                //}
                if (result.Succeeded)
                {
                    //Create User First Skill
                    var ProjectSetting = _unitOfWork.ProjectSettingGR.GetAll().FirstOrDefault();
                    if (ProjectSetting != null)
                    {
                        var Skill = new Skill
                        {
                            UserId = user.Id,
                            CategoryId = ProjectSetting.DefultCategoryId,
                            Percent = 0,
                            SkillType = SkillType.Adventitious,
                            Credit = 0
                        };
                        _unitOfWork.SkillGR.Add(Skill);
                    }

                    //Send sms Message
                    var code = UserManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);
                    var message = new IdentityMessage
                    {
                        Destination = user.PhoneNumber,
                        Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                    };
                    _sendRepository.Sms(message.Destination, message.Body);
                    Result.Statue = Enums.Statue.Success;
                    Result.Messages.Add("ثبت نام با موفقیت انجام شد.");
                    Result.UserVM = user.ConvertUserToUserVm();
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    AddErrors(result);
                    Result.Messages = modelStateHelper.GetModelStateErrors(ModelState);
                    return Result;
                }

            }
            else
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Messages = modelStateHelper.GetModelStateErrors(ModelState);
                return Result;
            }

        }
        #endregion

        #region SetUserInfo&&EditProfile

        [HttpPost, Route("api/IndexUserInfo")]
        public UserInfoViewModel IndexUserInfo([FromBody] Request model)
        {
            var loginUser = _userRepository.GetByToken(model.Token);
            if (loginUser.Statue == Enums.Statue.AccessDenied)
            {
                return new UserInfoViewModel() { Statue = loginUser.Statue, Message = loginUser.Message };
            }

            if (!String.IsNullOrEmpty(model.UserId))
            {
                var checkUser = UserManager.Users.Where(w => w.Id == model.UserId).Include(i => i.City.Province.Country).Include(i => i.Skills.Select(s => s.Level)).Include(i => i.Favorites).FirstOrDefault();

                var user = checkUser;

                if (user != null)
                {
                    user.IsFavorite = user.Favorites.Any(f => f.UserId == loginUser.User.Id);

                    return new UserInfoViewModel
                    {
                        ProfileEditViewModel = user.ConvertUserToUserVm(),
                        SocialNetworkViewModel = new SocialNetworkViewModel
                        {
                            GitHub = user.GitHub,
                            GooglePlus = user.GooglePlus,
                            Instageram = user.Instageram,
                            LinkedIn = user.LinkedIn,
                            Telegram = user.Telegram,
                            Twitter = user.Twitter,
                            WebSite = user.WebSite,
                            UserId = user.Id,
                            WhatsApp = user.WhatsApp
                        },
                        UserSkills = user.Skills.ToList().ConvertskillsToSkillViewModels(),
                        UserFavedCount = user.Favorites.Count(w => w.FavedType == FavedType.User),
                        Statue = Enums.Statue.Success,
                        Message = "ارسال با موفقیت انجام شد."
                    };

                }
                else
                {
                    return new UserInfoViewModel()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "شناسه ارسال شده اشتباه است."
                    };
                }
            }
            else
            {
                return new UserInfoViewModel()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "لطفا شناسه کاربر را ارسال کنید."
                };
            }
        }

        [HttpPost, Route("api/EditProfile")]
        public ResultUser EditProfile([FromBody] UserInfoViewModel model)
        {
            ApplicationUser user = new ApplicationUser();
            ResultUser resultUser = new ResultUser();
            if (ModelState.IsValid)
            {
                user = model.ProfileEditViewModel.ConvertUserVmToUser();
                try
                {
                    //Find UserId From Token

                    #region FindUserIdFromToken
                    if (!String.IsNullOrEmpty(model.ProfileEditViewModel.SecurityStamp))
                    {
                        user.Id = _userRepository.GetByToken(model.ProfileEditViewModel.SecurityStamp).User.Id;

                    }
                    else
                    {
                        return new ResultUser()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "اطلاعات به درستی ارسال نشده است."
                        };
                    }
                    #endregion

                    var beforuser = UserManager.FindByIdAsync(user.Id).Result;

                    user.Img = beforuser.Img;
                    user.PhoneNumber = beforuser.PhoneNumber;
                    user.Email = beforuser.Email;
                    //درصد تکمیل پروفایل
                    #region CompltPercent
                    var beforCopmltPercent = (((String.IsNullOrEmpty(beforuser.FullName) ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.NickName) ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.UserName) ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.PhoneNumber) ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.Phone) ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.AboutMe) ? 0 : 1) +
                                             (beforuser.BirthDate == null ? 0 : 1) +
                                             (beforuser.JobStatusId == null ? 0 : 1) +
                                             (beforuser.MaritalStatusId == null ? 0 : 1) +
                                             (beforuser.SolderStatusId == null ? 0 : 1) +
                                             (beforuser.GenderId == null ? 0 : 1) +
                                             (beforuser.CityId == null ? 0 : 1) +
                                             (String.IsNullOrEmpty(beforuser.Address) ? 0 : 1))
                                             * 0.0769230769230769) * 16.66666666666667;
                    #endregion
                    var result = _userRepository.EditBasicInfo(user);
                    //درصد تکمیل پروفایل
                    #region CompltPercent
                    var s = user;
                    var afterCompltPercent = (((String.IsNullOrEmpty(s.FullName) ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.NickName) ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.UserName) ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.PhoneNumber) ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.Phone) ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.AboutMe) ? 0 : 1) +
                                         (s.BirthDate == null ? 0 : 1) +
                                         (s.JobStatusId == null ? 0 : 1) +
                                         (s.MaritalStatusId == null ? 0 : 1) +
                                         (s.SolderStatusId == null ? 0 : 1) +
                                         (s.GenderId == null ? 0 : 1) +
                                         (s.CityId == null ? 0 : 1) +
                                         (String.IsNullOrEmpty(s.Address) ? 0 : 1))
                                         * 0.0769230769230769) * 16.66666666666667;
                    #endregion

                    if (result.Statue == Enums.Statue.Success)
                    {
                        //درصد تکمیل پروفایل
                        #region CompltPercent
                        if (beforuser.UserInfoComplatePercent == 0)
                        {
                            _userRepository.ChangeUserInfoComplatePercent(user.Id, afterCompltPercent);
                        }
                        else
                        {
                            double complatePercent = afterCompltPercent - beforCopmltPercent;
                            _userRepository.ChangeUserInfoComplatePercent(user.Id, complatePercent);
                        }
                        #endregion
                        resultUser.Message = result.Message;
                        resultUser.Statue = result.Statue;


                    }
                    else
                    {
                        resultUser.Message = result.Message;
                        resultUser.Statue = result.Statue;
                    }
                    //برای رفع ارور A circular reference was detected while serializing an object of type 

                    var jsonUser = UserManager.FindByIdAsync(user.Id).Result.ConvertUserToUserVm();
                    jsonUser.City = null;
                    resultUser.ProfileEditViewModel = jsonUser;
                }
                catch (Exception ex)
                {
                    resultUser.Message = ex.InnerException.Message;
                    //برای رفع ارور A circular reference was detected while serializing an object of type 
                    var jsonUser = UserManager.FindByIdAsync(user.Id).Result.ConvertUserToUserVm();
                    jsonUser.City = null;
                    resultUser.ProfileEditViewModel = jsonUser;
                }
            }
            else
            {
                return new ResultUser()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات به درستی ارسال نشده است."
                };
            }

            return resultUser;
        }

        #region UserImage

        [HttpPost, Route("api/UserImageSave")]
        public Result UserImageSave()
        {

            string userId = "";
            var model = HttpContext.Request;
            if (model.Form.Files.Count > 0)
            {
                var file = model.Form.Files[0];
                var token = model.Form["Token"];
                var image = _fileRepository.SaveFile(file, "User", "20", true);
                if (!String.IsNullOrEmpty(token))
                {
                    userId = _userRepository.GetByToken(token).User.Id;


                    if (!String.IsNullOrEmpty(userId) && image.Statue == Enums.Statue.Success)
                    {
                        var user = UserManager.Users.FirstOrDefault(f => f.Id == userId);

                        if (user.Img != "/Content/img/default_logo.jpg")
                        {
                            _fileRepository.RemoveFile(user.Img);
                            _fileRepository.RemoveFile(user.Img.Replace("S128x128", "S50x50"));
                            _fileRepository.RemoveFile(user.Img.Replace("S128x128", "S200x200"));
                        }

                        //ResizeFilePaths[0]=S50x50||ResizeFilePaths[1]=S128x128||ResizeFilePaths[2]=S200x200
                        user.Img = image.ResizeFilePaths[1];
                        var isSave = _userRepository.EditBasicInfo(user);
                        if (isSave.Statue == Enums.Statue.Success)
                        {
                            image.Message = isSave.Message;
                            image.Statue = Enums.Statue.Success;
                        }
                    }
                    else
                    {
                        image.FilePath = "/Content/img/default_logo.jpg";
                        image.Message = "بارگذاری با مشکل مواجه شد.لطفا این مورد را از تماس با ما گذارش دهید.";
                        image.Statue = Enums.Statue.Failure;
                    }
                }
                else
                {
                    return new Result()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "توکن ارسالی صحیح نمیباشد"
                    };
                }
                return image;
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "هیچ فایلی ارسال نشده است"
                };
            }

        }



        #endregion
        #endregion

        #region SocialNetwork

        [HttpPost, Route("api/SocialNetwork")]

        public ResultUser SocialNetwork([FromBody] UserInfoViewModel model)
        {
            ApplicationUser user = new ApplicationUser();
            ResultUser resultUser = new ResultUser();

            if (ModelState.IsValid && !String.IsNullOrEmpty(model.SocialNetworkViewModel.SecurityStamp))
            {
                ApplicationUser EditUser = UserManager.FindByIdAsync(_userRepository.GetByToken(model.SocialNetworkViewModel.SecurityStamp).User.Id).Result;
                //درصد تکمیل پروفایل
                #region CompltPercent

                var beforCopmltPercent = (((String.IsNullOrEmpty(EditUser.Telegram) ? 0 : 1) +
                                           (String.IsNullOrEmpty(EditUser.Instageram) ? 0 : 1) +
                                           (String.IsNullOrEmpty(EditUser.LinkedIn) ? 0 : 1) +
                                           (String.IsNullOrEmpty(EditUser.Twitter) ? 0 : 2) +
                                           (String.IsNullOrEmpty(EditUser.WhatsApp) ? 0 : 1) +
                                           (String.IsNullOrEmpty(EditUser.WebSite) ? 0 : 1)) *
                                          0.1428571428571429) * 16.66666666666667;

                #endregion

                user = model.SocialNetworkViewModel.ConvertSocialNetworkVMToUser(EditUser);
                try
                {
                    var result = _userRepository.EditBasicInfo(user);
                    //درصد تکمیل پروفایل
                    #region CompltPercent
                    var s = user;
                    var afterCompltPercent = (((String.IsNullOrEmpty(s.Telegram) ? 0 : 1) +
                                              (String.IsNullOrEmpty(s.Instageram) ? 0 : 1) +
                                              (String.IsNullOrEmpty(s.LinkedIn) ? 0 : 1) +
                                              (String.IsNullOrEmpty(s.Twitter) ? 0 : 2) +
                                              (String.IsNullOrEmpty(s.WhatsApp) ? 0 : 1) +
                                              (String.IsNullOrEmpty(s.WebSite) ? 0 : 1)) *
                                              0.1428571428571429) * 16.66666666666667;
                    #endregion
                    if (result.Statue == Enums.Statue.Success)
                    {   //درصد تکمیل پروفایل
                        #region CompltPercent
                        if (EditUser.UserInfoComplatePercent == 0)
                        {
                            _userRepository.ChangeUserInfoComplatePercent(user.Id, afterCompltPercent);
                        }
                        else
                        {
                            double complatePercent = afterCompltPercent - beforCopmltPercent;
                            _userRepository.ChangeUserInfoComplatePercent(user.Id, complatePercent);
                        }
                        #endregion
                    }
                    resultUser.Message = result.Message;
                    resultUser.Statue = Enums.Statue.Success;
                    resultUser.SocialNetworkViewModel = user.ConvertUserToSocialNetworkVM();
                }
                catch (Exception ex)
                {
                    resultUser.Message = ex.InnerException.Message;
                    resultUser.Statue = Enums.Statue.Failure;
                }
            }
            else
            {
                return new ResultUser()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات به درستی ارسال نشده است."
                };
            }
            return resultUser;
        }

        #endregion

        #region ForgotPassword
        [HttpPost, Route("api/ForgotPassword")]
        public ResultUser ForgotPassword([FromBody] ForgotPasswordViewModel model)
        {
            if (model.phoneNumber.Length < 11 || model.phoneNumber.Length > 11)
            {
                return new ResultUser()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "شماره موبایل باید 11 رقم باشد"
                };
            }
            ApplicationUser user = UserManager.Users.FirstOrDefault(w => w.PhoneNumber == model.phoneNumber);

            if (user == null)
            {
                return new ResultUser()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "کاربری با این شماره همراه یافت نشد."
                };
            }
            else
            {
                var code = UserManager.GenerateChangePhoneNumberTokenAsync(user, model.phoneNumber);
                var message = new IdentityMessage
                {
                    Destination = model.phoneNumber,
                    Body = " کد امنیتی شما در اپلیکیشن فرصت: " + code
                };
                var smsRsult = _sendRepository.Sms(message.Destination, message.Body);
                if (/*smsRsult*/true)
                {
                    return new ResultUser()
                    {
                        Statue = Enums.Statue.Success,
                        Message = "پیام تایید ارسال شد",
                        UserVM = user.ConvertUserToUserVm()
                    };
                }
                else
                {
                    return new ResultUser()
                    {
                        Statue = Enums.Statue.Failure,
                        Message = "ارسال پیام تایید با مشکل مواجه شد."
                    };
                }
            }

        }

        [HttpPost, Route("api/ResetPassword")]
        public Result ResetPassword([FromBody] ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "اطلاعات ورودی اشتباه است"
                };
            }
            ApplicationUser user = UserManager.Users.Where(w => w.PhoneNumber == model.PhoneNumber).FirstOrDefault();
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "کاربری با این شماره موبایل یافت نشد!!"
                };
            }

            //موقت !!تا پنل اس ام اس راه اندازی شود
            model.Code = UserManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber).Result;

            IdentityResult result = UserManager.ResetPasswordAsync(user, model.Code, model.Password).Result;
            if (result.Succeeded)
            {
                //return RedirectToAction("ResetPasswordConfirmation", "Account", new { Message = "u" });
                return new Result()
                {
                    Statue = Enums.Statue.Success,
                    Message = "عملیات مربوط به کلمه عبور با موفقیت انجام شد ,اکنون وارد شوید."
                };
            }
            AddErrors(result);
            return new Result()
            {
                Statue = Enums.Statue.Failure,
                Message = modelStateHelper.GetModelStateErrors(ModelState).FirstOrDefault()
            };
        }

        #endregion

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                if (error.Description.Contains("Name"))
                {
                    ModelState.AddModelError("", "(این نام کاربری قبلا در سیستم ثبت شده است (نام کاربری قسمت قبل @ ایمیل شماست");
                }
                else if (error.Description.Contains("Email"))
                {
                    ModelState.AddModelError("", "این ایمیل قبلا در سیستم ثبت نام شده است");
                }
                else
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
        }


    }

    public class RequestAddPhoneNumberViewModel : Request
    {
        public AddPhoneNumberViewModel AddPhoneNumberViewModel { get; set; }
    }

}
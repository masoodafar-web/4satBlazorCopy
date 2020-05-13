using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels.CategoryViewModel;
using newFace.Shared.Repositories;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using newFace.Shared;

namespace newFace.Controllers.Api
{
    [ApiController]
    public class HiController : Controller
    {
        private readonly ICartRepository _cartrepository;
        private readonly IChatRepository _chatRepository;
        private readonly ITransactionRepository _transactionrepository;
        private readonly IShopRepository _shoprepository;
        private readonly IGiftRepository _giftrepository;
        private readonly IFavoriteRepository _favoriterepository;
        private readonly IShareholderRepository _shareholderRepository;
        private readonly IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public HiController(ICartRepository cartrepository, IChatRepository chatRepository, ITransactionRepository transactionrepository, IShopRepository shoprepository, IGiftRepository giftrepository, IFavoriteRepository favoriterepository, IShareholderRepository shareholderRepository, IUserRepository userRepository, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _cartrepository = cartrepository;
            _chatRepository = chatRepository;
            _transactionrepository = transactionrepository;
            _shoprepository = shoprepository;
            _giftrepository = giftrepository;
            _favoriterepository = favoriterepository;
            _shareholderRepository = shareholderRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }

       
        #region RegionCasecading
        [HttpPost, Route("api/CascadingGetCountry")]

        public GeneralItemRes CascadingGetCountry()
        {
            var country = _unitOfWork.CountryGR.AsQueryable();
            return new GeneralItemRes()
            {
                Statue = Enums.Statue.Success,
                SelectListItem = country.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name })
                    .ToList(),
            };
        }
        [HttpPost, Route("api/CascadingGetProvince")]

        public GeneralItemRes CascadingGetProvince(GeneralFilterReq country)
        {
            var province = _unitOfWork.ProvinceGR.AsQueryable();
            if (country.Id != null)
            {
                province = province.Where(p => p.CountryId == country.Id);
            }

            if (!String.IsNullOrEmpty(country.FilterText))
            {
                province = province.Where(p => p.Name.Contains(country.FilterText));
            }
            return new GeneralItemRes()
            {
                Statue = Enums.Statue.Success,
                SelectListItem = province.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name })
                    .ToList(),
            };
        }
        [HttpPost, Route("api/CascadingGetCity")]

        public GeneralItemRes CascadingGetCity(GeneralFilterReq province)
        {
            var city = _unitOfWork.CityGR.AsQueryable();
            if (province.Id != null)
            {
                city = city.Where(p => p.ProvinceId == province.Id);
            }
            if (!String.IsNullOrEmpty(province.FilterText))
            {
                city = city.Where(p => p.Name.Contains(province.FilterText));
            }
            return new GeneralItemRes()
            {
                Statue = Enums.Statue.Success,
                SelectListItem = city.Select(p => new SelectListItem() { Value = p.Id.ToString(), Text = p.Name })
                    .ToList(),
            };
        }

        #endregion

        #region ProfilDropDowns

        [HttpPost, Route("api/ProfilDropDowns")]
        public object ProfilDropDowns()
        {
            object result = new object();
            try
            {
                result = new
                {
                    ProfilDropDowns = new
                    {
                        GenderId = Enum.GetValues(typeof(Enums.Gender)).Cast<Enums.Gender>()
                        .Select(v => new SelectListItem
                        {
                            Selected = v.Equals(Enums.Gender.Men),
                            Text = v.GetDisplayName(),
                            Value = System.Convert.ToInt32(v).ToString()
                        }).ToList(),
                        JobStatusId = Enum.GetValues(typeof(Enums.JobStatus)).Cast<Enums.JobStatus>()
                            .Select(v => new SelectListItem
                            {
                                Selected = v.Equals(Enums.JobStatus.Unemployed),
                                Text = v.GetDisplayName(),
                                Value = System.Convert.ToInt32(v).ToString()
                            }).ToList(),
                        MaritalStatusId = Enum.GetValues(typeof(Enums.MaritalStatuse)).Cast<Enums.MaritalStatuse>()
                            .Select(v => new SelectListItem
                            {
                                Selected = v.Equals(Enums.MaritalStatuse.Single),
                                Text = v.GetDisplayName(),
                                Value = System.Convert.ToInt32(v).ToString()
                            }),
                        SolderStatusId = Enum.GetValues(typeof(Enums.SolderStatus)).Cast<Enums.SolderStatus>()
                            .Select(v => new SelectListItem
                            {
                                Selected = v.Equals(Enums.SolderStatus.NotGone),
                                Text = v.GetDisplayName(),
                                Value = System.Convert.ToInt32(v).ToString()
                            }).ToList(),
                        EducationalStatusId = Enum.GetValues(typeof(Enums.EducationalStatus)).Cast<Enums.EducationalStatus>()
                            .Select(v => new SelectListItem
                            {
                                Selected = v.Equals(Enums.EducationalStatus.Cycle),
                                Text = v.GetDisplayName(),
                                Value = System.Convert.ToInt32(v).ToString()
                            }).ToList(),
                        HealthStatusId = Enum.GetValues(typeof(Enums.HealthStatus)).Cast<Enums.HealthStatus>()
                            .Select(v => new SelectListItem
                            {
                                Selected = v.Equals(Enums.HealthStatus.Healthy),
                                Text = v.GetDisplayName(),
                                Value = System.Convert.ToInt32(v).ToString()
                            }).ToList(),
                    },
                    Statue = 1,
                    Message = "موارد با موفقیت ارسال شد"
                };
            }
            catch (Exception e)
            {
                result = new
                {
                    ProfilDropDowns = new
                    { },
                    Statue = 0,
                    Message = e.Message
                };
            }

            return result;
        }

        #endregion
        [HttpPost,Route("api/GetAllCategory")]
        public ResultCat_CatViewModel GetAllCategory([FromBody]RequestCategoryTypeEnum categoryTypeEnum)
        {
            List<Category_CategoryViewModel> categoriesCategoryViewModels = new List<Category_CategoryViewModel>();
            if (categoryTypeEnum.CategoryTypeEnum != null)
            {
                if (categoryTypeEnum.CategoryTypeEnum==CategoryTypeEnum.Skill)
                {
                    categoriesCategoryViewModels.AddRange(
                        _unitOfWork.Category_CategoryGR
                            .FindBy(f => f.Children.CategoryType == categoryTypeEnum.CategoryTypeEnum && f.Parent.CategoryType != CategoryTypeEnum.SkillUpgrade).DistinctBy(d=>d.ChildrenCatId).ToList()
                            .ToCategory_CategoryViewModels());
                }
                else
                {
                    categoriesCategoryViewModels.AddRange(
                        _unitOfWork.Category_CategoryGR
                            .FindBy(f =>
                                f.Children.CategoryType == categoryTypeEnum.CategoryTypeEnum &&
                                f.Parent.CategoryType != CategoryTypeEnum.SkillUpgrade).ToList()
                            .ToCategory_CategoryViewModels());

                }
                
            }
            else
            {
                categoriesCategoryViewModels.AddRange(
                    _unitOfWork.Category_CategoryGR.GetAll().Where(w => w.Parent.CategoryType != CategoryTypeEnum.SkillUpgrade).ToList().ToCategory_CategoryViewModels()
                );
            }
            return new ResultCat_CatViewModel()
            {
                Categories = categoriesCategoryViewModels,
                Statue = Enums.Statue.Success,
                Message = "دسته بندی با موفقیت ارسال شد",
            };
        }

        [HttpPost,Route("api/GetAutoComplate")]
        public ResultAutoComplate GetAutoComplate([FromBody]ReqAutoComplate reqAutoComplate)
        {
            var Resource = _unitOfWork.ResourcesGR.FindBy(f=>f.ResourcesType==Resources.ResourcesTypeEnum.University || f.ResourcesType == Resources.ResourcesTypeEnum.JobPosition || f.ResourcesType == Resources.ResourcesTypeEnum.Company).ToList();

            if (String.IsNullOrEmpty(reqAutoComplate.type))
            {
                return new ResultAutoComplate()
                {
                    Message = "موارد ارسال شده صحیح نمی باشد.",
                    Statue = Enums.Statue.Failure
                };
            }
            if (reqAutoComplate.type == "University")
            {
                Resource = Resource.Where(w => w.ResourcesType == Resources.ResourcesTypeEnum.University).ToList();
            }
            if (reqAutoComplate.type == "JobPosition")
            {
                Resource = Resource.Where(w => w.ResourcesType == Resources.ResourcesTypeEnum.JobPosition).ToList();
            }
            if (reqAutoComplate.type == "Company")
            {
                Resource = Resource.Where(w => w.ResourcesType == Resources.ResourcesTypeEnum.Company).ToList();
            }
            if (!string.IsNullOrWhiteSpace(reqAutoComplate.text) && !string.IsNullOrEmpty(reqAutoComplate.text))
            {
                Resource = Resource.Where(w => w.Name.Contains(reqAutoComplate.text)).Select(s => new Resources
                {
                    Id = s.Id,
                    Name = s.Name,
                    ResourcesType = s.ResourcesType

                }).ToList();
            }


            return new ResultAutoComplate()
            {
                Resourceses = Resource,
                Message = "با موفقیت ارسال شد.",
                Statue = Enums.Statue.Success
            };
        }

        [HttpPost, Route("api/AddNewForResource")]
        public ResultResources AddNewForResource(ReqResources Value)
        {
            if (!String.IsNullOrEmpty(Value.Name) && !String.IsNullOrWhiteSpace(Value.Name) && !String.IsNullOrEmpty(Value.type))
            {
                Resources resources=new Resources();
                try
                {
                    if (Value.type == "University")
                    {
                        resources.ResourcesType = Resources.ResourcesTypeEnum.University;
                    }
                    if (Value.type == "JobPosition")
                    {
                        resources.ResourcesType = Resources.ResourcesTypeEnum.JobPosition;
                    }
                    if (Value.type == "Company")
                    {
                        resources.ResourcesType = Resources.ResourcesTypeEnum.Company;
                    }

                    resources.Name = Value.Name;
                    var Result = _unitOfWork.ResourcesGR.Add(resources);
                    return new ResultResources
                    {
                        Statue = Result.Statue,
                        Message = Result.Message,
                        Resources = resources
                    };
                }
                catch (Exception e)
                {
                    return new ResultResources
                    {
                        Statue = Enums.Statue.Failure,
                        Message = e.Message,
                        Resources = resources
                    };
                }
            }
            else
            {

                return new ResultResources
                {
                    Statue = Enums.Statue.Failure,
                    Message = "پارامتر های ورودی اشتباه است.",
                };
            }
        }

        [HttpPost, Route("api/GetField")]
        public ResultGetField GetField([FromBody]ReqGetField reqGetField)
        {
            var Field = _unitOfWork.FieldAndOrientationGR.GetAll().Select(s => new
            {
                Id = s.Id,
                Name = s.Name,
                ParentId = s.ParentId

            }).ToList().Select(s => new FieldAndOrientationVM()
            {
                Id = s.Id,
                Name = s.Name,
                ParentId = s.ParentId

            }).ToList();

            if (!string.IsNullOrEmpty(reqGetField.parentId))
            {
                int pId = int.Parse(reqGetField.parentId);
                Field = Field.Where(w => w.ParentId == pId).ToList();
            }
            else
            {
                Field = Field.Where(w => w.ParentId == null).ToList();
            }
            if (!string.IsNullOrWhiteSpace(reqGetField.text) && !string.IsNullOrEmpty(reqGetField.text))
            {
                Field = Field.Where(w => w.Name.Contains(reqGetField.text)).ToList();
            }

            return new ResultGetField()
            {
                Statue = Enums.Statue.Success,
                Message = "با موفقیت ارسال شد.",
                FieldAndOrientations = Field
            };
        }

        [HttpPost, Route("api/CreateFAndO")]

        public ResultGetField CreateFAndO([FromBody]FieldAndOrientation fieldAndOrientation)
        {
            if (ModelState.IsValid)
            {

                var Result = _unitOfWork.FieldAndOrientationGR.Add(fieldAndOrientation);

                return new ResultGetField()
                {
                    Statue = Result.Statue,
                    Message = Result.Message,
                    FieldAndOrientation = fieldAndOrientation
                };
            }
            else
            {
                return new ResultGetField()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "موارد ارسالی نادرست است",
                    FieldAndOrientation = null,
                };
            }

        }

        [HttpPost, Route("api/Hi")]
        public async Task<ResultHi> Hi([FromBody]RequestHi model)
        {
            ResultHi result = new ResultHi();

            //وقتی درخواست از سمت وب نباشد
            if (!model.IsWebRequest)
            {
                var LastappUpdate =await _unitOfWork.AppUpdateGR.GetAll().OrderByDescending(o => o.Vresion).FirstOrDefaultAsync();
                if (model.Version < LastappUpdate?.Vresion)
                {
                    //برای این که هنوز آپدیت نیست پس پیام یک باره اضافه هست
                    LastappUpdate.UpdateMessage = "";

                    if (LastappUpdate.Isforce)
                    {
                        return new ResultHi()
                        {
                            Statue = Enums.Statue.NotLastVersionForce,
                            AppUpdate = LastappUpdate,
                        };
                    }
                    else
                    {
                        result.Statue = Enums.Statue.NotlastVersion;
                        result.AppUpdate = LastappUpdate;
                    }
                }
                else
                {
                    //اگر پیام را یک بار دیده دیگر آن را نفرست چون یک باره اضافه است
                    if (model.IsSeenUpdateMsg)
                    {
                        LastappUpdate.UpdateMessage = "";
                    }
                    result.Statue = Enums.Statue.Success;
                    result.AppUpdate = LastappUpdate;
                }



                result.ProfilDropdownEnums = new
                {
                    ProfilDropDowns = new
                    {
                        GenderId = Enum.GetValues(typeof(Enums.Gender)).Cast<Enums.Gender>()
                   .Select(v => new SelectListItem
                   {
                       Selected = v.Equals(Enums.Gender.Men),
                       Text = v.GetDisplayName(),
                       Value = System.Convert.ToInt32(v).ToString()
                   }).ToList(),
                        JobStatusId = Enum.GetValues(typeof(Enums.JobStatus)).Cast<Enums.JobStatus>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.JobStatus.Unemployed),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }).ToList(),
                        MaritalStatusId = Enum.GetValues(typeof(Enums.MaritalStatuse)).Cast<Enums.MaritalStatuse>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.MaritalStatuse.Single),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }),
                        SolderStatusId = Enum.GetValues(typeof(Enums.SolderStatus)).Cast<Enums.SolderStatus>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.SolderStatus.NotGone),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }).ToList(),
                        EducationalStatusId = Enum.GetValues(typeof(Enums.EducationalStatus)).Cast<Enums.EducationalStatus>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.EducationalStatus.Cycle),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }).ToList(),
                        HealthStatusId = Enum.GetValues(typeof(Enums.HealthStatus)).Cast<Enums.HealthStatus>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.HealthStatus.Healthy),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }).ToList(),
                        GradeId = Enum.GetValues(typeof(Enums.EducationalStatus)).Cast<Enums.EducationalStatus>()
                       .Select(v => new SelectListItem
                       {
                           Selected = v.Equals(Enums.EducationalStatus.Cycle),
                           Text = v.GetDisplayName(),
                           Value = System.Convert.ToInt32(v).ToString()
                       }).ToList(),
                    }
                };

                result.Cities =await _unitOfWork.CityGR.GetAllAsync();
                result.Provinces =await _unitOfWork.ProvinceGR.GetAllAsync();
                result.Countries = await _unitOfWork.CountryGR.GetAllAsync(); ;

                result.Resources = await _unitOfWork.ResourcesGR.GetAllAsync();
                result.Levels = await _unitOfWork.LevelGR.GetAllAsync();

            }

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                ApplicationUser user = await UserManager.Users.FirstOrDefaultAsync(u => u.SecurityStamp == model.Token);

                if (user != null)
                {
                    result.User = user.ConvertUserToUserVm();
                }
                else
                {
                    result.Message = "کاربری با این مشخصات یافت نشد";
                }
             
                result.Cartcount = await _unitOfWork.CartGR.CountAsync(c => c.UserId == user.Id);
                result.unSeenCount =_chatRepository.UnSeenCount(user.Id);
                result.BillCount = await _unitOfWork.BillGR.CountAsync(c => c.UserId == user.Id);
                result.ProductsCount = await _unitOfWork.FactorforsaleProductGR.CountAsync(w => w.Bill.Status == 1 && w.UserId == user.Id && w.BuyType == BuyType.Normal);
                result.ShareholderListCount = await _unitOfWork.ShareholderGR.CountAsync(s => s.UserId == user.Id);
                result.CountFaved = await _unitOfWork.FavoriteGR.CountAsync(c => c.UserId == user.Id);
                result.MySentGifts = _giftrepository.MySentGifts(user.Id);
                result.MyRecievedGifts = _giftrepository.MyRecievedGifts(user.Id);
                result.AdviceVisionCount = await _unitOfWork.VisionGR.CountAsync(f => f.UserId == user.Id && f.VisionStatus == Enums.VisionStatus.Advice);
                result.UserInfoComplatePercent = (int)UserManager.FindByIdAsync(user.Id).Result.UserInfoComplatePercent;
                result.WalletCredit = UserManager.FindByIdAsync(user.Id).Result.WalletCredit;
                result.UserPoint = "0";
                if (user != null && user.Point != 0)
                {
                    result.UserPoint = string.Format("{0:N0}", user.Point);
                }

                result.UserCredit = "0";
                if (user != null && user.Credit != 0)
                {
                    result.UserCredit = string.Format("{0:N0}", user.Credit);
                }

                var LoginUserGen = await _unitOfWork.UserGeneologyGR.FirstOrDefaultAsync(p => p.UserId == user.Id);
                result.LoginUserGenId = LoginUserGen?.Id;

            }

            //result.Categories = _Category_CategoryService.GetAll().ToList().ToCategory_CategoryViewModels();
           

            return result;
        }


        [HttpPost, Route("api/GetCounts")]
        public ResultHi GetCounts([FromBody]RequestHi model)
        {
            ResultHi result = new ResultHi();

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue == Enums.Statue.AccessDenied)
                {
                    result.Message = checkUser.Message;
                    result.Statue = checkUser.Statue;
                    return result;
                }

                var user = checkUser.User;

                result.Cartcount = _unitOfWork.CartGR.Count(c => c.UserId == user.Id);
                result.unSeenCount = _chatRepository.UnSeenCount(user.Id);
                result.BillCount = _unitOfWork.BillGR.Count(c => c.UserId == user.Id);
                result.ProductsCount = _unitOfWork.FactorforsaleProductGR.Count(w => w.Bill.Status == 1 && w.UserId == user.Id && w.BuyType == BuyType.Normal);
                result.ShareholderListCount = _unitOfWork.ShareholderGR.Count(s => s.UserId == user.Id);
                result.CountFaved = _favoriterepository.GetAll(null, user.Id).TotalFavedCount;
                result.MySentGifts = _giftrepository.MySentGifts(user.Id);
                result.MyRecievedGifts = _giftrepository.MyRecievedGifts(user.Id);
                result.AdviceVisionCount = _unitOfWork.VisionGR.Count(f => f.UserId == user.Id && f.VisionStatus == Enums.VisionStatus.Advice);
                result.UserInfoComplatePercent = (int)user.UserInfoComplatePercent;
                result.UserPoint = "0";
                result.WalletCredit = user.WalletCredit;

                if (user != null && user.Point != 0)
                {
                    result.UserPoint = string.Format("{0:N0}", user.Point);
                }

                result.UserCredit = "0";
                if (user != null && user.Credit != 0)
                {
                    result.UserCredit = string.Format("{0:N0}", user.Credit);
                }

                result.LoginUserGenId = _unitOfWork.UserGeneologyGR.FirstOrDefault(p => p.UserId == user.Id)?.Id;

            }

            result.Statue = Enums.Statue.Success;

            return result;
        }
    }


}

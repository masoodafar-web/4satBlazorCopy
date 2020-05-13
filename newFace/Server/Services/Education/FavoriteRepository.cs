using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Server.Services.Resource;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Services.Education
{
    public class FavoriteRepository : IFavoriteRepository
    {
        private readonly IPostRepository _postRepository;
        private readonly IProductRepository _productRepository;
        private readonly IUserRepository _userRepository;
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public FavoriteRepository(IPostRepository postRepository, IProductRepository productRepository, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _postRepository = postRepository;
            _productRepository = productRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public ResultFavorite AddRemove(int? id, string userid, FavedType type, Enums.changetype changeType, string currentUserid)
        {
            ResultFavorite result = new ResultFavorite();
            if (type==FavedType.User)
            {
                if (id == null && string.IsNullOrEmpty(userid))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "لطفا شناسه را ارسال نمایید";
                    result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                    return result;
                }
            }
          
            switch (type)
            {
                case FavedType.Post:
                    //آیا پست وجود دارد؟
                    ResultPost resultPost = _postRepository.GetById(id, "");
                    if (resultPost.Statue == Enums.Statue.Success)
                    {
                        //آیا در لیست علاقه مندی های کاربر از قبل وجود دارد؟
                        var findfaved = _unitOfWork.FavoriteGR.FirstOrDefault(f => f.UserId == currentUserid && f.FavedType == FavedType.Post && f.PostFavedId == id);
                        if (changeType == Enums.changetype.Add)
                        {
                            //اگر در لیست علاقه مندی های قبلی نبود   اضافه کن
                            if (findfaved == null)
                            {
                                var fave = new Favorite
                                {
                                    UserId = currentUserid,
                                    PostFavedId = id,
                                    FavedType = FavedType.Post
                                };
                                var save = _unitOfWork.FavoriteGR.Add(fave);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype =changetype.Add;
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                        else if (changeType == Enums.changetype.Remove)
                        {
                            //اگر در لیست علاقه مندی های قبلی بود   حذف کن
                            if (findfaved != null)
                            {
                                var save = _unitOfWork.FavoriteGR.Delete(findfaved);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype = Enums.changetype.Remove;
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                    }
                    else
                    {
                        return new ResultFavorite()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "پست یافت نشد"
                        };
                    }
                    break;
                case FavedType.Product:
                    var resultAsync= _productRepository.GetById(id.Value, "").Result;
                    ResultProduct resultProduct = resultAsync;
                    if (resultProduct.Statue == Enums.Statue.Success)
                    {
                        //آیا در لیست علاقه مندی های کاربر از قبل وجود دارد؟
                        var findfaved = _unitOfWork.FavoriteGR.FirstOrDefault(f => f.UserId == currentUserid && f.FavedType == FavedType.Product && f.ProductFavedId == id);
                        if (changeType == Enums.changetype.Add)
                        {
                            //اگر در لیست علاقه مندی های قبلی نبود   اضافه کن
                            if (findfaved == null)
                            {
                                var fave = new Favorite
                                {
                                    UserId = currentUserid,
                                    ProductFavedId = id,
                                    FavedType = FavedType.Product
                                };
                                var save = _unitOfWork.FavoriteGR.Add(fave);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype = Enums.changetype.Add;
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                        else if (changeType == Enums.changetype.Remove)
                        {
                            //اگر در لیست علاقه مندی های قبلی بود   حذف کن
                            if (findfaved != null)
                            {
                                var save = _unitOfWork.FavoriteGR.Delete(findfaved);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype = Enums.changetype.Remove;
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                    }
                    else
                    {
                        return new ResultFavorite()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "پست یافت نشد"
                        };
                    }
                    break;
                case FavedType.User:
                   ApplicationUser user = UserManager.Users.FirstOrDefault(f => f.Id == userid);
                   if (user != null)
                    { 
                        //آیا در لیست علاقه مندی های کاربر از قبل وجود دارد؟
                        var findfaved = _unitOfWork.FavoriteGR.FirstOrDefault(f => f.UserId == currentUserid && f.FavedType == FavedType.User && f.UserFavedId == userid);
                        if (changeType == Enums.changetype.Add)
                        {
                            //اگر در لیست علاقه مندی های قبلی نبود   اضافه کن
                            if (findfaved == null)
                            {
                                var fave = new Favorite
                                {
                                    UserId = currentUserid,
                                    UserFavedId = userid,
                                    FavedType = FavedType.User
                                };
                                var save = _unitOfWork.FavoriteGR.Add(fave);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype = Enums.changetype.Add;
                                result.CountUserFaved = _unitOfWork.FavoriteGR.Count(c => c.FavedType == FavedType.User && c.UserFavedId == userid);
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                        else if (changeType == Enums.changetype.Remove)
                        {
                            //اگر در لیست علاقه مندی های قبلی بود   حذف کن
                            if (findfaved != null)
                            {
                                var save = _unitOfWork.FavoriteGR.Delete(findfaved);
                                result.Statue = save.Statue;
                                result.Message = save.Message;
                                result.Changetype = Enums.changetype.Remove;
                                result.CountUserFaved = _unitOfWork.FavoriteGR.Count(c => c.FavedType == FavedType.User && c.UserFavedId == userid);
                                result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == currentUserid);
                                return result;

                            }
                        }
                    }
                    else
                    {
                        return new ResultFavorite()
                        {
                            Statue = Enums.Statue.Failure,
                            Message = "پست یافت نشد"
                        };
                    }
                    break;
                default:
                   return new ResultFavorite()
                   {
                       Statue = Enums.Statue.Failure,
                       Message = "نوع علاقه مندی ارسال نشده"
                   };

            }
            return new ResultFavorite()
            {
                Statue = Enums.Statue.Failure,
                Message = "نوع علاقه مندی ارسال نشده"
            };
        }

        public bool IsFaved(int? id, string userid, FavedType type, string currentUserid)
        {
            switch (type)
            {
                case FavedType.Post:
                   return _unitOfWork.FavoriteGR.Any(f =>
                        f.UserId == currentUserid && f.FavedType == FavedType.Post && f.PostFavedId == id);
                    break;
                case FavedType.User:
                    return _unitOfWork.FavoriteGR.Any(f =>
                        f.UserId == currentUserid && f.FavedType == FavedType.User && f.UserFavedId == userid);
                    break;
                case FavedType.Product:
                    return _unitOfWork.FavoriteGR.Any(f =>
                        f.UserId == currentUserid && f.FavedType == FavedType.Product && f.ProductFavedId == id);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
     

         }


        public int GetUserFavedCount(string userid)
        {
            var count = _unitOfWork.FavoriteGR.Count(c => c.UserFavedId == userid);

            return count;
        }

        public ResultFavorite GetAll(FavedType? type, string userid)
        {
            ResultFavorite result = new ResultFavorite();
            result.TotalFavedCount = _unitOfWork.FavoriteGR.Count(c => c.UserId == userid);

            if (type != null)
            {
                try
                {
                    if (type == FavedType.Post)
                    {
                        var postsfaved = _unitOfWork.FavoriteGR.FindBy(w => w.UserId == userid && w.FavedType == FavedType.Post)
                            .Select(s => s.Post)
                            .Include(i=>i.Category)
                            .Include(i=>i.Levels)
                            .Include(i=>i.Comment)
                            .Include(i=>i.Users)
                            .Include(i=>i.Favorites)
                            .ToList();
                        postsfaved.ForEach(f=>f.IsFavorite=true);
                        result.Posts.AddRange(postsfaved);
                        result.Statue = Enums.Statue.Success;
                        result.Message = "لیست پست های مورد علاقه با موفقیت ارسال شد";
                        return result;
                    }
                    if (type == FavedType.Product)
                    {
                        var productsfaved = _unitOfWork.FavoriteGR.FindBy(w => w.UserId == userid && w.FavedType == FavedType.Product)
                            .Select(s => s.Product)
                            .ToList();
                        productsfaved.ForEach(f => f.IsFavorite = true);
                        result.Products.AddRange(productsfaved);
                        result.Statue = Enums.Statue.Success;
                        result.Message = "لیست محصولات مورد علاقه با موفقیت ارسال شد";
                        return result;
                    }
                    if (type == FavedType.User)
                    {
                        var usersfaved = _unitOfWork.FavoriteGR.FindBy(w => w.UserId == userid && w.FavedType == FavedType.User)
                            .Select(s => s.User)
                            .ToList();
                        usersfaved.ForEach(f => f.IsFavorite = true);
                        //اگر مشکلی برای این قسمت پیش آمد حتما با مسعود برای اصلاح مشورت شود
                        result.Users.AddRange(usersfaved.Select(s=>s.ConvertUserToUserVm()).ToList());
                        result.Statue = Enums.Statue.Success;
                        result.Message = "لیست کاربران ذخیره شده با موفقیت ارسال شد";
                        return result;
                    }

                }
                catch (Exception e)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "خطای ارسال لیست علاقه مندی ها";
                    return result;
                }
            }

            try
            {
                var FavedList = _unitOfWork.FavoriteGR.GetAllIncluding(w => w.UserId == userid, i => i.User, i => i.Post, i => i.Product).ToList();
                result.Favorites.AddRange(FavedList);
                result.Statue = Enums.Statue.Success;
                result.Message = "لیست علاقه مندی ها با موفقیت ارسال شد";
                return result;

            }
            catch (Exception e)
            {
                result.Favorites = new List<Favorite>();
                result.Statue = Enums.Statue.Failure;
                result.Message = "خطای ارسال لیست علاقه مندی ها";
                return result;
            }


        }




    }
}
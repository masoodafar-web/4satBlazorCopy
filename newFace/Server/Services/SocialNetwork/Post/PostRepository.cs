using System.Collections.Generic;
using System.Linq;
using System;
using System.Web;
using System.Linq.Expressions;
using newFace.Server;
using newFace.Server.Push;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repository.Push;
using Microsoft.AspNetCore.Identity;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Services.Resource
{
    public class PostRepository : IPostRepository
    {

        private readonly IUserRepository _userRepository;
        private readonly IPointRepository _pointLogRepository;
        private readonly IFileRepository _fileRepository;
        private readonly ICommentRepository _commentRepository;
       private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private IFireBaseNotification _notification;
        private UserManager<ApplicationUser> UserManager;

        public PostRepository(IUserRepository userRepository, IPointRepository pointLogRepository, IFileRepository fileRepository, ICommentRepository commentRepository, ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IFireBaseNotification notification, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _pointLogRepository = pointLogRepository;
            _fileRepository = fileRepository;
            _commentRepository = commentRepository;
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _notification = notification;
            UserManager = userManager;
        }
        
        public ResultPost GetById(int? id, string loginUserId)
        {
            ResultPost result = new ResultPost();


            if (id != null)
            {
                //var posts = _db.Posts.Where(p => p.Id == id.Value).Include(p => p.Category).Include(p => p.Levels).Include(p => p.Users).Include(p => p.Comment.Select(u => u.CommentsChilds)).ToList();
                var posts = _unitOfWork.PostGR.GetAllIncluding(p => p.Id == id.Value, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), p => p.PostChangeRequests).ToList();
                var postList = posts.Select(p => new Post
                {
                    AdsType = p.AdsType,
                    Category = p.Category,
                    CategoryId = p.CategoryId,
                    CDate = p.CDate,
                    CommentCount = p.Comment.Count(),
                    Desc = p.Desc,
                    DisLike = p.DisLike,
                    DocumentFile = p.DocumentFile,
                    File = p.File,
                    Id = p.Id,
                    Img = p.Img,
                    ImgThumbnail = p.ImgThumbnail,
                    IsDeleted = p.IsDeleted,
                    LevelId = p.LevelId,
                    Levels = p.Levels,
                    Like = p.Like,
                    MDate = p.MDate,
                    Seen = p.Seen,
                    Title = p.Title,
                    Type = p.Type,
                    Users = p.Users,
                    UserId = p.UserId,
                    Video = p.Video,
                    PostChangeRequests = string.IsNullOrEmpty(loginUserId) ? new List<PostChangeRequest>() { } : p.PostChangeRequests?.Where(pc => pc.UserId == loginUserId).ToList() ?? new List<PostChangeRequest>() { },
                    IsFavorite = string.IsNullOrEmpty(loginUserId) ? false : p.Favorites?.Any(f => f.UserId == loginUserId) ?? false,
                    VideoThumbnail = p.VideoThumbnail,
                }).ToList();

                Post post = postList.FirstOrDefault();
                if (post != null)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Post = post;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "با موفقیت ارسال نشد";
                    return result;
                }

            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "آی دی دریافت نشد";
                return result;
            }


        }

        public ResultPost GetByIdForEdit(int? id)
        {
            ResultPost result = new ResultPost();


            if (id != null)
            {
                var post = _unitOfWork.PostGR.GetById(id.Value);

                if (post != null)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Post = post;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "با موفقیت ارسال نشد";
                    return result;
                }

            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "آی دی دریافت نشد";
                return result;
            }


        }
        public Result Create(Post post)
        {
            return _unitOfWork.PostGR.Add(post);
        }

        public Result Delete(int? id)
        {
            Result result = new Result();

            if (id == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "ای دی دریافت نشد";
                return result;
            }

            var post = _unitOfWork.PostGR.GetSingleIncluding(p => p.Id == id.Value, p => p.Comment);

            if (post == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "یافت نشد";
                return result;
            }

            var postFavorits = _unitOfWork.FavoriteGR.GetAll().Where(f => f.PostFavedId == post.Id).ToList();
            var PostNotifi = _unitOfWork.NotifiGR.FindBy(f => f.PostId == post.Id).ToList();
            if (PostNotifi.Any())
            {
                _unitOfWork.NotifiGR.DeleteRange(PostNotifi);
            }
            if (postFavorits.Any())
            {
                _unitOfWork.FavoriteGR.DeleteRange(postFavorits);
            }

            var postComments = post.Comment.Where(p => p.ParentId == null).ToList();
            if (postComments.Any())
            {
                //foreach (var item in post.Comment.Where(p => p.ParentId == null).ToList())
                //{
                //    _commentRepository.Delete(item.Id);

                //}

                _unitOfWork.CommentGR.DeleteRange(postComments);

            }
            return _unitOfWork.PostGR.Delete(post);

        }

        public Result Delete(Post post)
        {
            return _unitOfWork.PostGR.Delete(post);
        }

        public Result Edit(Post post)
        {
            return _unitOfWork.PostGR.Update(post);
        }

        public Result Seen(Seen model)
        {


            var dublicate = _unitOfWork.SeenGR.Any(f => f.UserId == model.UserId && f.PostId == model.PostId);
            if (dublicate)
            {
                return new Result { Message = "بازدید تکراری است", Statue = Enums.Statue.Failure };
            }
            else
            {
                _unitOfWork.SeenGR.Add(model);

                Post post = _unitOfWork.PostGR.GetById(model.PostId);
                post.Seen++;
                return _unitOfWork.PostGR.Update(post);
            }

        }
        public Result SeenPostList(List<Post> Posts, string userId)
        {
            try
            {

                foreach (var item in Posts)
                {
                    if (SeenPost(item, userId).Statue == Enums.Statue.Failure)
                        return new Result { Message = "پست " + item.Id + " سین نخورد", Statue = Enums.Statue.Failure };
                }
                return new Result { Message = "بازدید با موفقیت انجام شد", Statue = Enums.Statue.Success };

            }
            catch (Exception e)
            {

                return new Result { Message = e.Message, Statue = Enums.Statue.Failure };
            }
        }
        public Result SeenPostList(List<int> Posts, string userId)
        {
            try
            {

                foreach (var item in Posts)
                {
                    if (SeenPost(item, userId).Statue == Enums.Statue.Failure)
                        return new Result { Message = "پست " + item + " سین نخورد", Statue = Enums.Statue.Failure };
                }
                return new Result { Message = "بازدید با موفقیت انجام شد", Statue = Enums.Statue.Success };

            }
            catch (Exception e)
            {

                return new Result { Message = e.Message, Statue = Enums.Statue.Failure };
            }
        }
        public Result SeenPost(Post Post, string userId)
        {
            try
            {
                Seen seen = new Seen
                {
                    PostId = Post.Id,
                    UserId = userId,
                    Date = DateTime.Now
                };
                return Seen(seen);
            }
            catch (Exception e)
            {

                return new Result { Message = e.Message, Statue = Enums.Statue.Failure };
            }
        }
        public Result SeenPost(int Post, string userId)
        {

            try
            {
                Seen seen = new Seen
                {
                    PostId = Post,
                    UserId = userId,
                    Date = DateTime.Now
                };
                return Seen(seen);
            }
            catch (Exception e)
            {

                return new Result { Message = e.Message, Statue = Enums.Statue.Failure };
            }
        }
        public LikeResultViewModel LikeDislike(Like model)
        {
            LikeResultViewModel result = new LikeResultViewModel();
            result.Statue = Enums.Statue.Success;
            ApplicationUser user = UserManager.Users.FirstOrDefault(p => p.Id == model.UserId);
            Post post = _unitOfWork.PostGR.GetSingleIncluding(w => w.Id == model.PostId, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), P => P.PostChangeRequests, p => p.Favorites, p => p.Likes);

            Like likIsLike = new Like();
            bool IsLikeForCurentUser = false;
            bool IsDisLikeForCurentUser = false;
            bool likeOrDislikeDeletedDontSendPush = false;
            bool likeOrDislikeDeletedSendNewPush = false;
            //اگر قبلا لایک یا دیسلایک وجود داشت
            if (_unitOfWork.LikeGR.Any(f => f.UserId == model.UserId && f.PostId == model.PostId))
            {
                likIsLike = _unitOfWork.LikeGR.FindBy(f => f.UserId == model.UserId && f.PostId == model.PostId).FirstOrDefault();
                // اگر جدید بود مثلا قبلا همین کاربر لاک کرده الان دیسلایک میکند یا بلعکس
                if (likIsLike.IsLike != model.IsLike)
                {
                    likeOrDislikeDeletedSendNewPush = true;
                    //اگر جدید بود و لایک بود لایک را اضافه کن و دیسلایک را بردار
                    if (model.IsLike)
                    {

                        likIsLike.IsLike = model.IsLike;
                        var rate = likIsLike.Rate;

                        likIsLike.Rate -= likIsLike.Rate;
                        likIsLike.Rate++;
                        likIsLike.Date = DateTime.Now;
                        result.Statue = _unitOfWork.LikeGR.Update(likIsLike).Statue;
                        //لایک را اضافه کن
                        post.Like++;
                        post.Rate -= rate;
                        post.Rate++;
                        //دیسلایک را بردار
                        post.DisLike--;
                        if (result.Statue == Enums.Statue.Success)
                        {
                            IsLikeForCurentUser = true;
                            IsDisLikeForCurentUser = false;
                            result.Statue = _unitOfWork.PostGR.Update(post).Statue;

                            result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 1, Count = 12 }).Statue;
                        }

                    }
                    //اگر جدید بود و دیسلایک بود پس دیسلایک را اضافه کن و لایک را بردار
                    else
                    {

                        likIsLike.IsLike = model.IsLike;
                        var rate = likIsLike.Rate;
                        likIsLike.Rate -= likIsLike.Rate;
                        likIsLike.Rate--;

                        likIsLike.Date = DateTime.Now;
                        result.Statue = _unitOfWork.LikeGR.Update(likIsLike).Statue;
                        //دیسلایک را اضافه کن
                        post.DisLike++;
                        post.Rate -= rate;
                        post.Rate--;
                        //لایک را بردار
                        post.Like--;
                        if (result.Statue == Enums.Statue.Success)
                        {
                            IsLikeForCurentUser = false;
                            IsDisLikeForCurentUser = true;
                            result.Statue = _unitOfWork.PostGR.Update(post).Statue;
                            result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 2, Count = 12 }).Statue;
                        }
                    }
                }
                //اگر قبلا هم وجود داشت. مثلا کاربر لایک کرده و میخواهد لایکش را بردارد
                else
                {
                    likeOrDislikeDeletedDontSendPush = true;

                    //اگر لایک بود لایک را بردار
                    if (model.IsLike)
                    {
                        var rate = likIsLike.Rate;
                        int Count = (int)likIsLike.Count;
                        result.Statue = _unitOfWork.LikeGR.Delete(likIsLike).Statue;
                        post.Like -= Count;
                        post.Rate -= rate;
                        if (result.Statue == Enums.Statue.Success)
                        {
                            IsLikeForCurentUser = false;
                            result.Statue = _unitOfWork.PostGR.Update(post).Statue;
                            result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 1, Count = 12 }).Statue;
                        }
                    }
                    //اگر دیسلایک بود دیسلایک رو بردار
                    else
                    {

                        var rate = likIsLike.Rate;
                        int Count = (int)likIsLike.Count;
                        result.Statue = _unitOfWork.LikeGR.Delete(likIsLike).Statue;
                        post.DisLike -= Count;
                        post.Rate -= rate;
                        if (result.Statue == Enums.Statue.Success)
                        {
                            IsDisLikeForCurentUser = false;
                            result.Statue = _unitOfWork.PostGR.Update(post).Statue;
                            result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 2, Count = 12 }).Statue;
                        }

                    }
                }

            }
            else
            {
                if (model.IsLike)
                {
                    likIsLike = model;
                    likIsLike.Count++;
                    likIsLike.Point++;
                    likIsLike.Rate++;
                    likIsLike.Date = DateTime.Now;
                    result.Statue = _unitOfWork.LikeGR.Add(likIsLike).Statue;
                    post.Like++;
                    post.Rate++;
                    if (result.Statue == Enums.Statue.Success)
                    {
                        IsLikeForCurentUser = true;
                        result.Statue = _unitOfWork.PostGR.Update(post).Statue;
                        user.Point++;
                        result.Statue = _userRepository.EditBasicInfo(user).Statue;
                        result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 1, Count = 12 }).Statue;
                    }
                }
                else
                {
                    likIsLike = model;
                    likIsLike.Count++;
                    likIsLike.Point++;
                    likIsLike.Rate--;
                    likIsLike.Date = DateTime.Now;
                    result.Statue = _unitOfWork.LikeGR.Add(likIsLike).Statue;

                    post.DisLike++;
                    post.Rate--;
                    if (result.Statue == Enums.Statue.Success)
                    {
                        IsDisLikeForCurentUser = true;
                        result.Statue = _unitOfWork.PostGR.Update(post).Statue;
                        user.Point++;
                        result.Statue = _userRepository.EditBasicInfo(user).Statue;
                        result.Statue = _pointLogRepository.PointLog(new Point { UserId = model.UserId, PointTypeId = 2, Count = 12 }).Statue;
                    }
                }
            }

            if (likeOrDislikeDeletedDontSendPush || likeOrDislikeDeletedSendNewPush)
            {
                var notifis = _unitOfWork.NotifiGR.GetAll().Where(n => n.SenderId == model.UserId && n.ReceiverId == post.UserId && n.PostId == likIsLike.PostId && n.NotifiType == NotifiType.Postlike).ToList();
                if (notifis != null)
                {
                    _unitOfWork.NotifiGR.DeleteRange(notifis);
                }
            }

            if (!likeOrDislikeDeletedDontSendPush)
            {
                _notification.SendByUserId(post.UserId, "", likIsLike.Id,NotifiType.Postlike);
            }
            if (result.Statue != Enums.Statue.Success)
            {
                result.Message = "خطایی رخ داده دوباره امتحان کنید";
            }
            return new LikeResultViewModel
            {
                DisLike = post.DisLike,
                Like = post.Like,
                Statue = result.Statue,
                Message = result.Message,
                Rate = post.Rate,
                IsDisLike = IsDisLikeForCurentUser,
                IsLike = IsLikeForCurentUser,
            };
        }
        public ResultPost SearchAll(string userId, int? categoryId, int? levelId, int? adsType, int? favoriteRank, PostType postType, int pageNumber, int returnPostCount, string LoginUserId, bool? Like, bool? CDate, bool? Rate, bool? Seen, bool? Favorite = null)
        {
            ResultPost result = new ResultPost();
            try
            {
                CDate = CDate != null ? CDate.Value : false;
                Like = Like != null ? Like.Value : false;
                Seen = Seen != null ? Seen.Value : false;
                Rate = Rate != null ? Rate.Value : false;


                var postList = _unitOfWork.PostGR.GetAllIncluding(p => (p.Type == postType),
                 p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), P => P.PostChangeRequests, p => p.Favorites, p => p.Likes);

                if (!string.IsNullOrEmpty(userId))
                {
                    //var y = postList.ToList();
                    postList = postList.Where(p => p.UserId == userId);
                    //var x = postList.ToList();
                }
                if (categoryId != null)
                {
                    postList = postList.Where(p => p.CategoryId == categoryId.Value);

                }
                if (levelId != null)
                {
                    postList = postList.Where(p => p.LevelId == levelId.Value);

                }
                if (adsType != null)
                {
                    postList = postList.Where(p => p.AdsType == (AdsType)adsType.Value);

                }
                if (favoriteRank != null)
                {
                    postList = postList.Where(p => p.Like == favoriteRank.Value);
                }
                if (Favorite != null)
                {
                    postList = postList.Where(p => p.Favorites.Any(f => f.UserId == LoginUserId));
                }
                var posts = postList.OrderByDescending(p => CDate.Value ? p.Id : Rate.Value ? p.Rate : Seen.Value ? p.Seen : Like.Value ? p.Like : p.Id).Skip((pageNumber - 1) * returnPostCount).Take(returnPostCount).ToList();

                if (posts.Any())
                {
                    result.PostList = posts.ToPostViewModels(LoginUserId);
                    //.Select(p => new PostViewModel()
                    //{
                    //    AdsType = p.AdsType,
                    //    Category = p.Category,
                    //    CategoryId = p.CategoryId,
                    //    CDate = p.CDate,
                    //    CommentCount = p.Comment.Count(),
                    //    Desc = p.Desc,
                    //    DisLike = p.DisLike,
                    //    DocumentFile = p.DocumentFile,
                    //    File = p.File,
                    //    Id = p.Id,
                    //    Img = p.Img,
                    //    ImgThumbnail = p.ImgThumbnail,
                    //    IsDeleted = p.IsDeleted,
                    //    LevelId = p.LevelId,
                    //    Levels = p.Levels,
                    //    Like = p.Like,
                    //    MDate = p.MDate,
                    //    Seen = p.Seen,
                    //    Rate = p.Rate,
                    //    Title = p.Title,
                    //    Type = p.Type,
                    //    Users = p.Users,
                    //    UserId = p.UserId,
                    //    Video = p.Video,
                    //    VideoThumbnail = p.VideoThumbnail,
                    //    PostChangeRequests = string.IsNullOrEmpty(LoginUserId) ? null : p.PostChangeRequests.Where(pc => pc.UserId == LoginUserId).ToList(),
                    //    IsFavorite = string.IsNullOrEmpty(LoginUserId) ? false : p.Favorites.Any(f => f.UserId == LoginUserId)
                    //}).ToList();
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "موردی یافت نشد";
                    result.PostList = new List<PostViewModel>();
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
        //-----------------------------------------------------------
        public ResultPost PostChangeRequestChecker(int? postId, int? levelId, bool isExist, int? categoryId, string UserId)
        {
            ResultPost result = new ResultPost();

            int errorCount = 0;

            try
            {
                if (postId == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی پست دریافت نشد";
                    return result;
                    //return "ای دی پست دریافت نشد";
                }

                var post = _unitOfWork.PostGR.GetById(postId.Value);
                if (post == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "هیچ پستی با این مشخصات یافت نشد";
                    return result;
                    //return "هیچ پستی با این مشخصات یافت نشد";
                }

                if (UserId != null)
                {
                    string userId = UserId;

                    PostChangeRequest postChangeRequest = new PostChangeRequest
                    {
                        UserId = userId,
                        PostId = postId.Value
                    };

                    if (levelId != null)
                    {

                        PostChangeRequest duplicatePostChangeRequest = _unitOfWork.PostChangeRequestGR.FindBy(p => p.UserId == userId && p.PostId == postId && p.LevelId == levelId).FirstOrDefault();

                        if (duplicatePostChangeRequest != null)
                        {
                            postChangeRequest.LevelId = duplicatePostChangeRequest.LevelId;
                            result.Message += "عملیات گزارش تعیین سطح تکراری است." + Environment.NewLine;
                            errorCount++;
                            //تکراری
                        }
                        else
                        {
                            postChangeRequest.LevelId = levelId;



                            var postChangeRequestsCount = _unitOfWork.PostChangeRequestGR.Count(p => p.PostId == postId && p.LevelId == levelId);

                            if (postChangeRequestsCount == GlobalParametrs.GetLevelCount)
                            {
                                post.LevelId = levelId.Value;
                                _unitOfWork.PostGR.Update(post);
                            }
                            result.Message += "گزارش تعیین سطح با موفقیت ثبت شد." + Environment.NewLine;

                        }



                    }

                    if (isExist)
                    {

                        PostChangeRequest duplicatePostChangeRequest = _unitOfWork.PostChangeRequestGR.FindBy(p => p.UserId == userId && p.PostId == postId && p.IsExist).FirstOrDefault();

                        if (duplicatePostChangeRequest != null)
                        {
                            postChangeRequest.IsExist = duplicatePostChangeRequest.IsExist;
                            result.Message += "عملیات گزارش کپی رایت تکراری است." + Environment.NewLine;
                            errorCount++;
                        }
                        else
                        {
                            postChangeRequest.IsExist = isExist;


                            var postChangeRequestsCount = _unitOfWork.PostChangeRequestGR.Count(p => p.PostId == postId && p.IsExist == isExist);

                            if (postChangeRequestsCount == GlobalParametrs.GetIsExistCount)
                            {
                                _unitOfWork.PostGR.Delete(post);
                            }
                            result.Message += "گزارش کپی رایت با موفقیت ثبت شد." + Environment.NewLine;
                        }



                    }

                    if (categoryId != null)
                    {

                        PostChangeRequest duplicatePostChangeRequest = _unitOfWork.PostChangeRequestGR.FindBy(p => p.UserId == userId && p.PostId == postId && p.CategoryId != null).FirstOrDefault();

                        if (duplicatePostChangeRequest != null)
                        {
                            postChangeRequest.CategoryId = duplicatePostChangeRequest.CategoryId;
                            result.Message += "عملیات  گزارش دسته بندی تکراری است." + Environment.NewLine;
                            errorCount++;
                        }
                        else
                        {
                            postChangeRequest.CategoryId = categoryId;


                            var postChangeRequestsCount = _unitOfWork.PostChangeRequestGR.Count(p => p.PostId == postId && p.CategoryId == categoryId);

                            if (postChangeRequestsCount == GlobalParametrs.GetCategoryIdCount)
                            {
                                post.CategoryId = categoryId.Value;

                                _unitOfWork.PostGR.Update(post);

                            }
                            result.Message += "گزارش دسته بندی با موفقیت ثبت شد." + Environment.NewLine;
                        }


                    }
                    if ((levelId != null || isExist == true || categoryId != null) && errorCount != 3)
                    {
                        var postChangeRequestForEdit = _unitOfWork.PostChangeRequestGR.FindBy(p => p.UserId == userId && p.PostId == postId).FirstOrDefault();

                        if (postChangeRequestForEdit != null)
                        {
                            if (postChangeRequest.LevelId != null)
                                postChangeRequestForEdit.LevelId = postChangeRequest.LevelId;

                            if (postChangeRequest.CategoryId != null)
                                postChangeRequestForEdit.CategoryId = postChangeRequest.CategoryId;

                            postChangeRequestForEdit.IsExist = postChangeRequest.IsExist;

                            _unitOfWork.PostChangeRequestGR.Update(postChangeRequestForEdit);
                        }
                        else
                        {
                            _unitOfWork.PostChangeRequestGR.Add(postChangeRequest);
                        }

                        result.Statue = Enums.Statue.Success;
                        return result;
                    }
                    else
                    {
                        result.Statue = Enums.Statue.Failure;
                        if (errorCount == 3)
                        {
                            result.Message = " گزارش ارسالی تکراری است";
                        }
                        else
                        {
                            result.Message = "گزارشی ارسال نشده است";
                        }
                        return result;
                    }


                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "نام کاربری یافت نشد";
                    return result;
                    //نام کاربری یافت نشد

                }

            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }
        public Result RemovePostFiles(int? id)
        {
            Result result = new Result();

            if (id == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "ای دی دریافت نشد";
                return result;
            }
            var post = _unitOfWork.PostGR.GetById(id.Value);

            if (post == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "یافت نشد";
                return result;
            }

            _fileRepository.RemoveFile(post.Video);
            _fileRepository.RemoveFile(post.Img);
            _fileRepository.RemoveFile(post.DocumentFile);

            result.Statue = Enums.Statue.Success;
            result.Message = "";
            return result;

        }
        public ResultPost GetAll(int pageNumber, int returnPostCount)
        {
            ResultPost result = new ResultPost();
            List<Post> postList;
            try
            {
                var postcount = _unitOfWork.PostGR.Count();
                if ((postcount - (pageNumber * returnPostCount)) >= returnPostCount)
                {

                    postList = _unitOfWork.PostGR.GetAll(pageNumber, returnPostCount, p => p.Id, null, OrderBy.Descending, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds)).ToList();

                }
                else if ((pageNumber * returnPostCount) < postcount)
                {
                    postList = _unitOfWork.PostGR.GetAll(pageNumber, returnPostCount, p => p.Id, null, OrderBy.Descending, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds)).ToList();

                }
                else
                {
                    postList = new List<Post>();
                }
                if (postList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.PostList = postList.ToPostViewModels(null);
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.PostList = new List<PostViewModel>() { };
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
        public ResultPost GetAllByType(PostType postType, int pageNumber, int returnPostCount, string userId, bool? Like, bool? CDate, bool? Rate, bool? Seen)
        {
            ResultPost result = new ResultPost();
            List<Post> postList = new List<Post>();
            CDate = CDate != null ? CDate.Value : false;
            Like = Like != null ? Like.Value : false;
            Seen = Seen != null ? Seen.Value : false;
            Rate = Rate != null ? Rate.Value : false;
            try
            {


                List<int> userSkill;

                if (string.IsNullOrEmpty(userId))
                {
                    userSkill = new utility().loginUser("").UserCategorys.Select(p => p.Category).ToList().Select(i => i.Id).ToList();
                }
                else
                {
                    userSkill = new utility().loginUser(userId).UserCategorys.Select(p => p.Category).ToList().Select(i => i.Id).ToList();
                }



                postList = _unitOfWork.PostGR.GetAllIncluding(p => (userSkill.Any() ? (userSkill.Any(u => u == p.CategoryId) && p.Type == postType) || (p.UserId == userId && p.Type == postType) : p.Type == postType),
                    p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), p => p.PostChangeRequests, p => p.Favorites, p => p.Likes)
                    .OrderByDescending(p => CDate.Value ? p.Id : Rate.Value ? p.Rate : Seen.Value ? p.Seen : Like.Value ? p.Like : p.Id)
                    .Skip((pageNumber - 1) * returnPostCount).Take(returnPostCount).ToList();


                if (postList.Any())
                {
                    result.PostList = postList.ToPostViewModels(userId);
                    //    .Select(p => new Post
                    //{
                    //    AdsType = p.AdsType,
                    //    Category = p.Category,
                    //    CategoryId = p.CategoryId,
                    //    CDate = p.CDate,
                    //    CommentCount = p.Comment != null ? p.Comment.Count() : 0,
                    //    Desc = p.Desc,
                    //    DisLike = p.DisLike,
                    //    DocumentFile = p.DocumentFile,
                    //    File = p.File,
                    //    Id = p.Id,
                    //    Img = p.Img,
                    //    ImgThumbnail = p.ImgThumbnail,
                    //    IsDeleted = p.IsDeleted,
                    //    LevelId = p.LevelId,
                    //    Levels = p.Levels,
                    //    Like = p.Like,
                    //    MDate = p.MDate,
                    //    Seen = p.Seen,
                    //    Rate = p.Rate,
                    //    Title = p.Title,
                    //    Type = p.Type,
                    //    Users = p.Users,
                    //    UserId = p.UserId,
                    //    Video = p.Video,
                    //    VideoThumbnail = p.VideoThumbnail,
                    //    PostChangeRequests = string.IsNullOrEmpty(userId) ? null : p.PostChangeRequests.Where(pc => pc.UserId == userId).ToList(),
                    //    IsFavorite = string.IsNullOrEmpty(userId) ? false : p.Favorites.Any(f => f.UserId == userId)
                    //}).ToList();

                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "موردی یافت نشد!!";
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
        public ResultPost GetAll()
        {
            ResultPost result = new ResultPost();


            var postList = _unitOfWork.PostGR.GetAllIncluding(null, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), p => p.PostChangeRequests, p => p.Favorites).ToList();
            if (postList.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.PostList = postList.ToPostViewModels(null);
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                return result;
            }


        }
        public ResultPost GetAllByType(PostType postType)
        {
            ResultPost result = new ResultPost();

            List<Post> postList = _unitOfWork.PostGR.GetAllIncluding(p => p.Type == postType, p => p.Category, p => p.Levels, p => p.Users, p => p.Comment.Select(u => u.CommentsChilds), p => p.PostChangeRequests, p => p.Favorites).ToList();

            if (postList.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.PostList = postList.ToPostViewModels(null);
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                return result;
            }



        }
    }
 
}
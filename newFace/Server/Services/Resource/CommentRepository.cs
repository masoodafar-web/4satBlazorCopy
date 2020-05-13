using System.Collections.Generic;
using System.Linq;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;
using Microsoft.EntityFrameworkCore;
using newFace.Server.Push;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repository.Push;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Services.Resource
{
    public class CommentRepository : ICommentRepository
    {

        private IUnitOfWork _unitOfWork;
        private IFireBaseNotification _notification;

        public CommentRepository(IUnitOfWork unitOfWork, IFireBaseNotification notification)
        {
            _unitOfWork = unitOfWork;
            _notification = notification;
        }

        public Result Create(Comment obj)
        {
            obj.CDate = DateTime.Now;

            var result = _unitOfWork.CommentGR.Add(obj);

            if (obj.Rank == null && obj.PostId != null)
            {
                string userIdToPush = "";

                if (obj.ParentId != null)
                {
                    var parentComment = _unitOfWork.CommentGR.FirstOrDefault(c => c.Id == obj.ParentId);

                    userIdToPush = parentComment.UserId;
                }
                else
                {

                    var commentPost = _unitOfWork.PostGR.FirstOrDefault(p => p.Id == obj.PostId);

                    userIdToPush = commentPost.UserId;
                }

                _notification.SendByUserId(userIdToPush, "", obj.Id,NotifiType.PostComment);
            }


            return result;


        }

        public ResultComment Delete(int? id)
        {
            
            ResultComment result = new ResultComment();


            if (id == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "ای دی دریافت نشد";
                return result;
            }
            Comment comment = _unitOfWork.CommentGR.GetSingleIncluding(c=>c.Id == id.Value,p =>p.Post);

            if (comment == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "یافت نشد";
                return result;
            }
            var postId = comment.PostId;
            var productId = comment.ProductId;
            List<Comment> childComments = _unitOfWork.CommentGR.GetAllIncluding(c => c.ParentId == comment.Id || c.FirstParentId == comment.Id, p => p.Post).ToList();

            if (comment.PostId != null)
            {

                string receiverId = "";
                if (childComments.Any())
                {
                    foreach (var item in childComments)
                    {

                        if (item.ParentId != null)
                        {
                            receiverId = item.CommentParent.UserId;
                        }
                        else
                        {
                            receiverId = item.Post.UserId;
                        }

                        var notifiChild = _unitOfWork.NotifiGR.FirstOrDefault(n => n.SenderId == item.UserId && n.ReceiverId == receiverId && n.PostId == item.PostId && n.CommentId == item.Id && n.NotifiType == NotifiType.PostComment);
                        if (notifiChild != null)
                        {
                            _unitOfWork.NotifiGR.Delete(notifiChild);
                        }

                        Delete(item.Id);
                    }

                }

                if (comment.ParentId != null)
                {
                    receiverId = comment.CommentParent.UserId;
                }
                else
                {
                    receiverId = comment.Post.UserId;
                }

                var notifi = _unitOfWork.NotifiGR.FirstOrDefault(n => n.SenderId == comment.UserId && n.ReceiverId == receiverId && n.PostId == comment.PostId && n.CommentId == comment.Id && n.NotifiType == NotifiType.PostComment);
                if (notifi != null)
                {
                    _unitOfWork.NotifiGR.Delete(notifi);
                }
            }

                
            
            var DeleteResult = _unitOfWork.CommentGR.Delete(comment);
            if (DeleteResult.Statue == Statue.Success)
            {
                //اگر کسانی جواب کامنتی که داره حذف میشه رو داده باشن اون جواب ها هم حذف میشه
                if (childComments.Any())
                {
                    _unitOfWork.CommentGR.DeleteRange(childComments);
                }
            }
            comment.PostId = postId;
            comment.ProductId = productId;


            result.Statue = DeleteResult.Statue;
            result.Message = DeleteResult.Message;
            result.Comment = comment;


            return result;



        }

        //public Result Delete(Comment obj)
        //{
        //    return _CommentService.Delete(obj);
        //}

        public Result Edit(Comment obj)
        {
            Result result = new Result();


            var vc = new ValidationContext(obj, null, null);

            if (Validator.TryValidateObject(obj, vc, null, true))
            {
                return _unitOfWork.CommentGR.Update(obj);
            }
            result.Statue = Enums.Statue.Failure;
            result.Message = "خطایی رخ داده است";
            return result;


        }

        public ResultComment GetById(int? id)
        {
            ResultComment result = new ResultComment();

            if (id != null)
            {

                //Comment obj = _db.Comments.Include(c => c.User).Include(c => c.FirstCommentsChilds.Select(fc => fc.User)).FirstOrDefault(p => p.Id == id.Value);
                Comment obj = _unitOfWork.CommentGR
                    .GetAll()
                    .Include(p => p.FirstCommentsChilds).ThenInclude(p => p.User)
                    .Include(p => p.User)
                    .Include(p => p.CommentParent).ThenInclude(p => p.User)
                    .FirstOrDefault(p => p.Id == id);
                if (obj != null)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Comment = obj;
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

        public ResultComment GetAll()
        {
            ResultComment result = new ResultComment();

            //List<Comment> list = _db.Comments.Include(c => c.CommentParent).Include(c => c.Post).Include(c => c.User).ToList();
            List<Comment> list = _unitOfWork.CommentGR.GetAllIncluding(null, c => c.CommentParent, c => c.Post, c => c.User).ToList();
            if (list.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.CommentList = list;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                return result;
            }



        }
        public ResultComment GetCommnetCount(int? postId, int? productId)
        {
            ResultComment result = new ResultComment();

            //var commnetCount = _db.Comments.Where(c => productId == null ? c.PostId == postId : c.ProductId == productId).Where(c => c.Rank == null).Count();
            var commnetCount = _unitOfWork.CommentGR.Count(c => productId == null ? c.PostId == postId : c.ProductId == productId && c.Rank == null);
            if (commnetCount >= 0)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.CommentCount = commnetCount;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                return result;
            }



        }
        public ResultComment GetProductCommnet(int productId)
        {
            ResultComment result = new ResultComment();


            //var commnet = _db.Comments.Where(c => c.ProductId == productId).ToList();
            var commnet = _unitOfWork.CommentGR.FindBy(c => c.ProductId == productId).ToList();
            if (commnet.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.CommentList = commnet;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                result.CommentList = new List<Comment>();
                return result;
            }

        }

        public ResultComment GetCommnetsLazyLoad(int? productId, int? postId,int? blogId, int pageNumber = 0)
        {
            ResultComment result = new ResultComment();


            if (productId == null && postId == null && blogId==null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "ارش ریدی نه پست میفرستی نه محصول";
                return result;
            }
            int returnCount = 10;
            //var commnets = _db.Comments.Include(c => c.User).Where(c => productId == null ? c.PostId == postId : c.ProductId == productId).Where(c => c.FirstParentId == null && c.Rank == null);
            var commnets = new List<Comment>();
            if (productId != null)
            {
                commnets = _unitOfWork.CommentGR
                    .GetAll()
                    .Include(p=>p.FirstCommentsChilds).ThenInclude(p=>p.User)
                    .Include(p=>p.User)
                    .Include(p=>p.CommentParent).ThenInclude(p => p.User)
                    .Where(c=> c.Rank == null && c.FirstParentId == null && c.ProductId == productId)
                    .OrderByDescending(p=>p.Id)
                    .Skip((pageNumber - 1) * returnCount)
                    .Take(returnCount)
                    .ToList();

            }
            else if (postId != null)
            {
                commnets = _unitOfWork.CommentGR
                    .GetAll()
                    .Include(p => p.FirstCommentsChilds).ThenInclude(p => p.User)
                    .Include(p => p.User)
                    .Include(p => p.CommentParent).ThenInclude(p => p.User)
                    .Where(c => c.Rank == null && c.FirstParentId == null && c.BlogId == blogId)
                    .OrderByDescending(p => p.Id)
                    .Skip((pageNumber - 1) * returnCount)
                    .Take(returnCount)
                    .ToList();

            }
            else if (blogId != null)
            {
                commnets = _unitOfWork.CommentGR
                    .GetAll()
                    .Include(p => p.FirstCommentsChilds).ThenInclude(p => p.User)
                    .Include(p => p.User)
                    .Include(p => p.CommentParent).ThenInclude(p => p.User)
                    .Where(c => c.Rank == null && c.FirstParentId == null && c.PostId == postId)
                    .OrderByDescending(p => p.Id)
                    .Skip((pageNumber - 1) * returnCount)
                    .Take(returnCount)
                    .ToList();

            }
            if (commnets.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.CommentList = commnets.OrderByDescending(c=>c.Id).ToList();
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "موردی یافت نشد!!";
                result.CommentList = new List<Comment>();
                return result;
            }

        }

        public Result CalculateProductRate(int productId, int rank)
        {
            Result result = new Result();

            //var comments = _db.Comments.Where(c => c.ProductId == productId && c.Rank != null).ToList();
            var comments = _unitOfWork.CommentGR.FindBy(c => c.ProductId == productId && c.Rank != null);

            int commentsCount = 0;
            float rate = rank;

            if (comments != null && comments.Any())
            {
                commentsCount = comments.Count();

                rate = comments.Sum(c => c.Rank.Value) / commentsCount;
            }



            //var product = _db.Products.FirstOrDefault(p => p.Id == productId);
            var product = _unitOfWork.ProductGR.GetById(productId);

            if (product != null)
            {
                product.Rate = rate;


                return _unitOfWork.ProductGR.Update(product);
            }

            result.Statue = Enums.Statue.Failure;
            result.Message = "عملیات ناموفق بود";
            return result;

        }




      

    }
}
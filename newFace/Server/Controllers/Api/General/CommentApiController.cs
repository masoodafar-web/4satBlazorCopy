
using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;


namespace newFace.Controllers.Api.General
{
    [ApiController]
    public class CommentApiController : Controller
    {


        private readonly ICommentRepository _commentRepository;
        private readonly IUserRepository _userRepository;

        private readonly IPostRepository _postRepository;
        private readonly IProductRepository _productRepository;

        private IUnitOfWork _unitOfWork;

        public CommentApiController(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;   
        }

        //----------------------------------------------------------------------

        [HttpPost, Route("api/Comment")]
        public ResultComment Comment([FromBody]RequestComment model)
        {
            ResultComment result = new ResultComment();

            try
            {
                Result saveResult = new Result();


                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue == Enums.Statue.AccessDenied)
                {
                    result.Message = checkUser.Message;
                    result.Statue = checkUser.Statue;
                    return result;
                }

                //امتیازدهی
                if (model.Comment != null && model.Comment.Rank != null && model.Comment.ProductId != null)
                {
                    model.Comment.UserId = checkUser.User.Id;
                    var IfExist =_unitOfWork.CommentGR.Any(w =>
                        w.UserId == model.Comment.UserId && w.ProductId == model.Comment.ProductId && w.Rank != null);
                    if (IfExist)
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "ثبت امتیاز تکراری است";
                        return result;
                    }

                    _commentRepository.Create(model.Comment);

                    var saveRankResult = _commentRepository.CalculateProductRate(model.Comment.ProductId.Value, model.Comment.Rank.Value);
                    if (saveRankResult.Statue == Enums.Statue.Success)
                    {
                        result.Message = "امتیاز شما با موفقیت ثبت شد";
                        result.Statue = Enums.Statue.Success;
                        return result;
                    }

                    else if (saveRankResult.Statue == Enums.Statue.Repetitive)
                    {
                        result.Message = "ثبت امتیاز تکراری است";
                        result.Statue = Enums.Statue.Failure;
                        return result;
                    }

                    result.Message = "ثبت امتیاز با خطا همراه بود";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                //لیزی لود کامنت
                if (model.PageNumber != null)
                {
                    result.CommentList = _commentRepository.GetCommnetsLazyLoad(model.ProductId, model.PostId, model.BlogId, model.PageNumber.Value).CommentList;

                    result.Statue = Enums.Statue.Success;
                    return result;
                }

                //حذف و ویرایش
                if (model.DeleteId != null || model.Comment.Id != 0)
                {
                    var commentId = model.DeleteId != null ? model.DeleteId : model.Comment.Id;

                    var comment = _commentRepository.GetById(commentId)?.Comment;
                    if (comment == null)
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "هیچ کامنتی با این مشخصات یافت نشد";
                        return result;
                    }

                    //حذف
                    if (model.DeleteId != null)
                    {
                        return _commentRepository.Delete(model.DeleteId);
                    }
                    //ویرایش
                    else if (model.Comment.Id != 0)
                    {
                        comment.Desc = model.Comment.Desc;
                        var editResult = _commentRepository.Edit(comment);

                        result.Statue = editResult.Statue;
                        result.Message = editResult.Message;

                        return result;
                    }
                }
                //کامنت جدید
                if (model.Comment != null && model.Comment.Id == 0)
                {
                    if (model.Comment.PostId != null)
                    {
                        var post = _postRepository.GetById(model.Comment.PostId,"");
                        if (post == null)
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = "هیچ پستی با این مشخصات یافت نشد";
                            return result;
                        }
                    }
                    else if (model.Comment.ProductId != null)
                    {
                        var product =_unitOfWork.ProductGR.Any(p=>p.Id == model.Comment.ProductId);
                        if (!product)
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = "هیچ محصولی با این مشخصات یافت نشد";
                            return result;
                        }
                    }
                    model.Comment.UserId = checkUser.User.Id;
                    saveResult = _commentRepository.Create(model.Comment);
                }

                if (saveResult.Statue == Enums.Statue.Success)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = saveResult.Message;

                    if (model.Comment != null)
                    {
                        result.CommentId = model.Comment.Id;
                    }
                    return result;
                }
                result.Statue = Enums.Statue.Failure;
                result.Message = saveResult.Message;

                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.InnerException.ToString();
                return result;
            }

        }

        //----------------------------------------------------------------------


        


    }
}

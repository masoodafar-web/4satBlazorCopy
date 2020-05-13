using System.Collections.Generic;
using System.Linq;
using System;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;

namespace newFace.Server.Services.Resource
{
    public class GiftRepository : IGiftRepository
    {
        private IUnitOfWork _unitOfWork;

        public GiftRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Create(Gift gift)
        {
            return _unitOfWork.GiftGR.Add(gift);
        }

        public Result Edit(Gift gift)
        {
            return _unitOfWork.GiftGR.Update(gift);

        }

        public Result Delete(Gift gift)
        {
            return _unitOfWork.GiftGR.Delete(gift);
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
                var gift = _unitOfWork.GiftGR.GetById(id.Value);

                if (gift == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "یافت نشد";
                    return result;
                }

                return Delete(gift);
               
       }

        public ResultGift GetById(int? id)
        {
            ResultGift result = new ResultGift();
         
                if (id != null)
                {
                    Gift gift = _unitOfWork.GiftGR.GetById(id.Value);
                    if (gift != null)
                    {
                        result.Statue = Enums.Statue.Success;
                        result.Message = "با موفقیت ارسال شد";
                        result.Gift = gift;
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

        public ResultGift GetAll(string userid)
        {
            ResultGift result = new ResultGift();
            List<Gift> giftLists = _unitOfWork.GiftGR.GetAllIncluding(
                           w => (w.UserResiv == userid) || (w.UserSend == userid) && w.Products.FactorforsaleProducts.Any(v => v.UserId == userid),
                           i => i.Products.FactorforsaleProducts, i => i.SendUsers, i => i.ResiveUsers,
                           i => i.Bill, i => i.Products.Books.Select(p => p.Publishers), i => i.Products.Courses.Select(p => p.Teacher),
                           i => i.Products.Exams.Select(p => p.Designer)).ToList();

                List<GiftViewModel> giftList = _unitOfWork.GiftGR.GetAllIncluding(
                    w => (w.UserResiv == userid) || (w.UserSend == userid) && w.Products.FactorforsaleProducts.Any(v=> v.UserId==userid),
                    i => i.Products.FactorforsaleProducts, i => i.SendUsers, i => i.ResiveUsers,
                    i => i.Bill,i=>i.Products.Books.Select(p=>p.Publishers), i => i.Products.Courses.Select(p => p.Teacher),
                    i => i.Products.Exams.Select(p => p.Designer)).ToList().Select(product => new GiftViewModel
                    {
                        Author = product.Products.Books.Any() ? (product.Products.Books.FirstOrDefault().Author != null ? product.Products.Books.FirstOrDefault().Author.FullName : "") : "",
                        Teacher = product.Products.Courses.Any() ? (product.Products.Courses.FirstOrDefault().Teacher != null ? product.Products.Courses.FirstOrDefault().Teacher.FullName : "") : "",
                        Designer = product.Products.Exams.Any() ? (product.Products.Exams.FirstOrDefault().Designer != null ? product.Products.Exams.FirstOrDefault().Designer.FullName : "") : "",
                        Date=product.Date,
                        PorductId=product.PorductId,
                        Status=product.Status,
                        UserResiv=product.ResiveUsers.FullName,
                        UserSend=product.SendUsers.FullName,
                        UserResivId = product.ResiveUsers.Id,
                        UserSendId = product.SendUsers.Id,
                        PorductImg =product.Products.Img,
                        PorductTitle=product.Products.Title,
                        Rate = product.Products.Comments != null ? product.Products.Comments.FirstOrDefault(c => c.UserId == userid && c.Rank != null)?.Rank != null ? (float)product.Products.Comments.FirstOrDefault(c => c.UserId == userid && c.Rank != null)?.Rank : 0 : 0

                    }).ToList();
                   

                if (giftList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.GiftViewModel = giftList;
                    result.GiftList = giftLists;
                return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.GiftViewModel = giftList;
                    result.GiftList = giftLists;
                return result;
                }

        }


        public int MySentGifts(string userid)
        {
            var mysentgifts = _unitOfWork.GiftGR.Count(w => w.UserSend == userid);

            return mysentgifts;
        }

        public int MyRecievedGifts(string userid)
        {
            var mysentgifts = _unitOfWork.GiftGR.Count(w => w.UserResiv == userid);

            return mysentgifts;
        }




    }
}
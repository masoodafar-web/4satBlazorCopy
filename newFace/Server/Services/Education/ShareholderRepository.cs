using System.Collections.Generic;
using System.Linq;
using System;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.EntityFrameworkCore;

namespace newFace.Server.Services.Resource
{
    public class ShareholderRepository : IShareholderRepository
    {

        private IUnitOfWork _unitOfWork;

        public ShareholderRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        //public Result Create(Shareholder shareholder)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        _db.Shareholders.Add(shareholder);
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }
        //}

        //public Result Edit(Shareholder shareholder)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        _db.Entry(shareholder).State = EntityState.Modified;

        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;

        //    }
        //    catch (System.Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;

        //    }
        //}

        //public Result Delete(Shareholder shareholder)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        _db.Shareholders.Remove(shareholder);
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }
        //}

        //public Result Delete(int? id)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        if (id == null)
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "ای دی دریافت نشد";
        //            return result;
        //        }
        //        var shareholder = _db.Shareholders.FirstOrDefault(p => p.Id == id.Value);

        //        if (shareholder == null)
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "یافت نشد";
        //            return result;
        //        }

        //        _db.Shareholders.Remove(shareholder);
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }

        //}

        //public Result Save(string message)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        if (Convert.ToBoolean(_db.SaveChanges()))
        //        {
        //            result.Statue = Enums.Statue.Success;
        //            result.Message = message + " با موفقیت انجام شد";
        //            return result;
        //        }
        //        else
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "عملیات" + message + " ناموفق بود";
        //            return result;
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }


        //}

        //public ResultShareholder GetById(int? id)
        //{
        //    ResultShareholder result = new ResultShareholder();
        //    try
        //    {
        //        if (id != null)
        //        {
        //            Shareholder shareholder = _db.Shareholders.FirstOrDefault(p => p.Id == id.Value);
        //            if (shareholder != null)
        //            {
        //                result.Statue = Enums.Statue.Success;
        //                result.Message = "با موفقیت ارسال شد";
        //                result.Shareholder = shareholder;
        //                return result;
        //            }
        //            else
        //            {
        //                result.Statue = Enums.Statue.Failure;
        //                result.Message = "با موفقیت ارسال نشد";
        //                return result;
        //            }

        //        }
        //        else
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "آی دی دریافت نشد";
        //            return result;
        //        }

        //    }
        //    catch (System.Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;

        //    }
        //}

        //public ResultShareholder GetAll()
        //{
        //    ResultShareholder result = new ResultShareholder();
        //    try
        //    {

        //        List<Shareholder> shareholderList = _db.Shareholders.ToList();
        //        if (shareholderList.Any())
        //        {
        //            result.Statue = Enums.Statue.Success;
        //            result.Message = "با موفقیت ارسال شد";
        //            result.ShareholderList = shareholderList;
        //            return result;
        //        }
        //        else
        //        {
        //            result.Statue = Enums.Statue.NullList;
        //            result.Message = "موردی یافت نشد!!";
        //            result.ShareholderList = shareholderList;
        //            return result;
        //        }


        //    }
        //    catch (System.Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;

        //    }
        //}
        //public ResultShareholder GetUserProductsOfShareholdersCount(string userId)
        //{
        //    ResultShareholder result = new ResultShareholder();

        //    var shareholders = _unitOfWork.ShareholderGR.Count(s => s.UserId == userId);
                
        //    result.Count = shareholders;
        //    result.Statue = Enums.Statue.Success;
        //    return result;
        //}
        public  ResultShareholder GetProductsOfShareholders(string userId)
        {
            ResultShareholder result = new ResultShareholder();

            var shareholders = _unitOfWork.ShareholderGR.GetAll()
                .Where(s=>s.UserId == userId)
                .Include(i => i.Product)
                .Include(p => p.Product.Books)
                .Include(p => p.Product.Books.Select(c => c.Author))
                .Include(p => p.Product.Books.Select(c => c.Publishers))
                .Include(p => p.Product.Books.Select(c => c.Speakers))
                .Include(p => p.Product.Books.Select(c => c.Translators))
                .Include(p => p.Product.Courses)
                .Include(p => p.Product.Courses.Select(c => c.Teacher))
                .Include(p => p.Product.Exams)
                .Include(p => p.Product.Exams.Select(s => s.Designer))
                .Include(p => p.Product.Exams.Select(s => s.Questions))
                .ToList();

            result.ShareholderList = shareholders;
            result.Statue = Enums.Statue.Success;
            return result;
        }

        //-----------------------------------------------------------



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;

namespace newFace.Server.Services.Education
{
    public class BookRepository : IBookRepository
    {
        private IUnitOfWork _unitOfWork;

        public BookRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Create(Book Book)
        {
            Result result = new Result();
            if (Book.ProductId==0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه محصول را ارسال کنید";
                return result;
            }

            if (string.IsNullOrEmpty(Book.FileText))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا فایل اصلی را ارسال کنید";
                return result;
            }
            if (Book.PageCount==0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا تعداد صفحات کتاب را ارسال کنید";
                return result;
            }
          

                return _unitOfWork.BookGR.Add(Book);
        
        }

        public Result Edit(Book Book)
        {
            Result result = new Result();

            if (string.IsNullOrEmpty(Book.FileText))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا فایل اصلی را ارسال کنید";
                return result;
            }
            if (Book.PageCount == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا تعداد صفحات کتاب را ارسال کنید";
                return result;
            }
            try
            {

                _unitOfWork.BookGR.Update(Book);
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

       public ResultBook GetById(int? Id)
        {
            ResultBook Result = new ResultBook();
            try
            {
                if (Id != null)
                {
                    Book Book = _unitOfWork.BookGR.GetSingleIncluding(p => p.Id == Id.Value, i => i.Translators,
                        i => i.Speakers, i => i.Publishers, i => i.Author);
           
                    if (Book != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.Book = Book;
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
                   Book Book = _unitOfWork.BookGR.GetById(Id.Value);
                   _unitOfWork.BookGR.Delete(Book);
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


        public ResultBook GetAll()
        {
            ResultBook result = new ResultBook();
            try
            {

                List<Book> BookList = _unitOfWork.BookGR.GetAll().ToList();
                if (BookList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Books = BookList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.Books = BookList;
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


    }
}
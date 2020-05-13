using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Server.Services.Resource;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using static newFace.Shared.Models.Resource.Enums;
using Video = newFace.Shared.Models.Video;
using Xabe.FFmpeg;

namespace newFace.Server.Services.Education
{
    public class ProductRepository : IProductRepository
    {
        private readonly IBookRepository _bookRepository;
        private readonly IProductScaleRepository _productScaleRepository;
        private readonly IFileRepository _fileRepository;
        private readonly IExamRepository _examRepository;
        private readonly IQuestionRepository _questionRepository;
        private IUnitOfWork _unitOfWork;
        HttpContext httpContext = new DefaultHttpContext();
        public ProductRepository(IBookRepository bookRepository, IProductScaleRepository productScaleRepository, IFileRepository fileRepository, IExamRepository examRepository, IQuestionRepository questionRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _productScaleRepository = productScaleRepository;
            _fileRepository = fileRepository;
            _examRepository = examRepository;
            _questionRepository = questionRepository;
            _unitOfWork = unitOfWork;
        }



        public Result CreateBook(ProductViewModels ProductViewModels, IFormFile FileText, IFormFile FileAudio)
        {
            Result result = new Result();


            //foreach (var items in ProductViewModels.ProductScales)
            //{
            //    var checksum = _productScaleRepository.CheckSum(items.CatId,
            //        items.Percent);

            //    if (checksum.Statue != Enums.Statue.Success)
            //    {
            //        ProductViewModels.ProductScales.Remove(items);
            //    }
            //}

            if (ProductViewModels.ProductScales.Count == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "درصد های هر بخش از مجموع مربوط به دسته بندی بیش از 100 میباشد";
                return result;
            }


            if (FileText != null)
            {
                var savefiletext = _fileRepository.SaveFile(FileText, "Product/Book", "100000");
                var savefileaudio = _fileRepository.SaveFile(FileAudio, "Product/Book", "100000");
                if (savefiletext.Statue == Enums.Statue.Success)
                {
                    ProductViewModels.Book.FileText = savefiletext.FilePath;
                }
                if (savefileaudio.Statue == Enums.Statue.Success)
                {
                    ProductViewModels.Book.FileAudio = savefileaudio.FilePath;
                }
            }

            if (ProductViewModels.Product.Type == ProductType.Book)
            {
                try
                {
                    if (string.IsNullOrEmpty(ProductViewModels.Book.FileText))
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "لطفا فایل کتاب را ارسال کنید";
                        return result;
                    }

                    if (ProductViewModels.ProductScales.Count == 0)
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "لطفا دسته بندی را انتخاب نمایید ";
                        return result;
                    }
                    ProductViewModels.Product.Date = DateTime.Now;
                    ProductViewModels.Product.Type = ProductType.Book;
                    ProductViewModels.Product.PriceWithDiscount =
                        ProductViewModels.Product.Price -
                        ((ProductViewModels.Product.Price * ProductViewModels.Product.Discount) / 100f);
                    var saveproduct = _unitOfWork.ProductGR.Add(ProductViewModels.Product);
                    if (saveproduct.Statue == Enums.Statue.Success)
                    {
                        ProductViewModels.Book.ProductId = ProductViewModels.Product.Id;

                        var savebook = _bookRepository.Create(ProductViewModels.Book);

                        if (savebook.Statue == Enums.Statue.Success)
                        {
                            int countps = 0;
                            foreach (var item in ProductViewModels.ProductScales)
                            {
                                item.ProductId = ProductViewModels.Product.Id;
                                try
                                {
                                    _productScaleRepository.Create(item);
                                    countps++;
                                }
                                catch
                                {

                                }

                            }

                            if (countps == 0)
                            {
                                result.Statue = Enums.Statue.Failure;
                                result.Message = "تاثیر محصول ثبت نشد";
                                return result;
                            }

                            result.Statue = Enums.Statue.Success;
                            result.Message = "محصول با موفقیت ذخیره شد";
                            return result;
                        }












                    }

                    result.Statue = Enums.Statue.Failure;
                    result.Message = "محصول ثبت نشد";
                    return result;
                }
                catch (System.Exception e)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = e.Message;
                    return result;

                }
            }

            result.Message = "نوع محصول بدرستی انتخاب نشده است";
            result.Statue = Enums.Statue.Null;
            return result;
        }

        public Result CreateCourse(ProductViewModels ProductViewModels, IFormFile sample, List<VideoFileViewModel> Videoes)
        {
            Result result = new Result();
            Result savevideos = new Result();





            var savesample = _fileRepository.SaveFile(sample, "Product/Course", "100000");
            if (savesample.Statue == Enums.Statue.Success)
            {
                ProductViewModels.Course.SampleofCourse = savesample.FilePath;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "نمونه فایل را وارد کنید";
                return result;
            }



            if (Videoes.Count == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا فایل های آموزشی را ارسال کنید";
                return result;
            }


            try
            {
                ProductViewModels.Product.Date = DateTime.Now;
                ProductViewModels.Product.Type = ProductType.Course;
                ProductViewModels.Product.PriceWithDiscount =
                    ProductViewModels.Product.Price -
                    ((ProductViewModels.Product.Price * ProductViewModels.Product.Discount) / 100f);
                var saveproduct = _unitOfWork.ProductGR.Add(ProductViewModels.Product);
                if (saveproduct.Statue == Enums.Statue.Success)
                {
                    ProductViewModels.Course.ProductId = ProductViewModels.Product.Id;

                    var savecourse = _unitOfWork.CourseGR.Add(ProductViewModels.Course);

                    if (savecourse.Statue == Enums.Statue.Success)
                    {
                        ResultFile savevideo = new ResultFile();
                        savevideo.Statue = Statue.Success;
                        foreach (var items in Videoes)
                        {

                            savevideo = _fileRepository.SaveFile(items.File, "Product/Course", "1000000");
                            if (savevideo.Statue != Statue.Success)
                            {
                                break;
                            }
                            //var ffProbe = new NReco.VideoInfo.FFProbe();
                            Video vid = new Video();
                            //var path = httpContext..Current.Server.MapPath(savevideo.FilePath);
                            vid.CourseId = ProductViewModels.Course.Id;
                            vid.File = savevideo.FilePath;
                            vid.VideoThumbnail = savevideo.VideoThumbnail;
                            vid.Size = items.File.Length;
                            vid.Title = items.Title;
                         
                            vid.VideoTime = DateTime.Now.AddHours(1);/*ffProbe.GetMediaInfo(path).Duration*/;


                            _unitOfWork.VideoGR.Add(vid);

                        }

                        if (savevideos.Statue != Enums.Statue.Success)
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = "خطای آپلود ویدپوها!";
                            return result;
                        }
                        var videoSaveResult = _unitOfWork.SaveChanges();
                        if (videoSaveResult.Statue != Enums.Statue.Success)
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = "خطای ثبت ویدپوها!";
                            return result;
                        }
                    }

                    int countps = 0;
                    foreach (var item in ProductViewModels.ProductScales)
                    {
                        item.ProductId = ProductViewModels.Product.Id;
                        try
                        {
                            _productScaleRepository.Create(item);
                            countps++;
                        }
                        catch
                        {

                        }

                    }

                    if (countps == 0)
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "تاثیر محصول ثبت نشد";
                        return result;
                    }

                    result.Statue = Enums.Statue.Success;
                    result.Message = "محصول با موفقیت ذخیره شد";
                    return result;


                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "محصول ثبت نشد!";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }




            result.Statue = Enums.Statue.Success;

            return result;
        }

        public Result CreateExam(ProductViewModels productViewModels)
        {
            Result result = new Result();

            productViewModels.Product.Type = ProductType.Exam;
            productViewModels.Product.Date = DateTime.Now;
            productViewModels.Product.PriceWithDiscount =
                productViewModels.Product.Price -
                ((productViewModels.Product.Price * productViewModels.Product.Discount) / 100f);
            var SaveProduct = _unitOfWork.ProductGR.Add(productViewModels.Product);
            if (SaveProduct.Statue == Enums.Statue.Success)
            {
                //productViewModels.ProductScale.ProductId = productViewModels.Product.Id;
                productViewModels.Exam.ProductId = productViewModels.Product.Id;
                //کپی پروداکت اسکیلهای محصول ارتباط داده شده به آزمون برای خود آزمون
                var newProductScals = productViewModels.ProductScales.Select(s => new ProductScale()
                {
                    CatId = s.CatId,
                    LevelId = s.LevelId,
                    ProductId = productViewModels.Product.Id,
                    Priority = (float)(s.Priority + 0.5),
                    Credit = s.Credit,
                    Point = s.Point
                }).ToList();
                var addRangeResult = _unitOfWork.ProductScaleGR.AddRange(newProductScals);


                if (addRangeResult.Statue == Enums.Statue.Failure)
                {
                    _unitOfWork.ProductGR.Delete(productViewModels.Product);
                    return addRangeResult;
                }
                var Save = _examRepository.Create(productViewModels.Exam);
                if (Save.Statue == Enums.Statue.Success)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "محصول با موفقیت ثبت شد";
                    return result;
                }
            }


            result.Statue = Enums.Statue.Failure;
            result.Message = "محصول ثبت نشد!!!";
            return result;
        }

        public Result CreateQuestion(QuestionAnswerViewModel questionAnswerViewModel, int? correctAnswer)
        {
            Result result = new Result();
            Result saveanswers = new Result()
            {
                Statue = Enums.Statue.Success,
            };
            if (questionAnswerViewModel.Question.ExamId == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه آزمون ارسال نشده است";
                return result;
            }

            var exist = _examRepository.GetById(questionAnswerViewModel.Question.ExamId);
            if (exist == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "شناسه آزمون اشتبا است لطفا از تغییر هرگونه المان در نمایش سایت خودداری نمایید";
                return result;
            }
            if (correctAnswer == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "پاسخ صحیح مشخص نشده است";
                return result;
            }

            if (questionAnswerViewModel.AnswerList.Count < 4 && questionAnswerViewModel.Question.QuestionType == Question.Questiontype.RadioButton)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "تعداد جوابها کمتر از ۴ پاسخ است";
                return result;
            }

            if (questionAnswerViewModel.Question.QuestionType == Question.Questiontype.TextArea && questionAnswerViewModel.AnswerList.Count != 1)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا پاسخ تشریحی صحیح را وارد نمایید";
                return result;
            }

            try
            {
                var createquestion = _questionRepository.Create(questionAnswerViewModel.Question);
                if (createquestion.Statue == Enums.Statue.Success)
                {
                    int count = 1;
                    foreach (var items in questionAnswerViewModel.AnswerList)
                    {
                        items.QuestionId = questionAnswerViewModel.Question.Id;
                        items.CorrectAnswer = correctAnswer == count ? true : false;
                        if (_unitOfWork.AnswerGR.Add(items).Statue != Enums.Statue.Success)
                        {
                            saveanswers.Statue = Enums.Statue.Failure;
                        }
                        count++;
                    }


                    if (saveanswers.Statue == Enums.Statue.Success)
                    {
                        result.Statue = Enums.Statue.Success;
                        result.Message = "سوالات با پاسخها با موفقیت ثبت شد";
                        return result;
                    }
                    else
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "پاسخها ثبت نشد!!!!";
                        if (questionAnswerViewModel.Question.Id != 0)
                        {
                            _questionRepository.Delete(questionAnswerViewModel.Question.Id);
                        }

                        return result;
                    }



                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "سوال ثبت نشد!!!";
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


        public async Task<Result> EditBook(ProductViewModels ProductViewModels, IFormFile FileText, IFormFile FileAudio)
        {

            Result result = new Result();
            var resultAsync = await GetById(ProductViewModels.Product.Id, "");
            var Editing_product = resultAsync.Product;

            var Editing_book = _bookRepository.GetById(ProductViewModels.Book.Id).Book;
            if (Editing_book == null || Editing_book.ProductId != Editing_product.Id)
            {
                result.Message = "اطلاعات کتاب نامعتبر است";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            //var Editing_productscale = _productScaleRepository.GetById(ProductViewModels.ProductScale.Id).ProductScale;
            //if (Editing_productscale == null || Editing_productscale.ProductId != ProductViewModels.Product.Id)
            //{
            //    result.Message = "اطلاعات تاثیرکتاب نامعتبر است";
            //    result.Statue = Enums.Statue.Failure;
            //    return result;
            //}

            var savefiletext = _fileRepository.SaveFile(FileText, "Product/Book", "100000");
            var savefileaudio = _fileRepository.SaveFile(FileAudio, "Product/Book", "100000");
            if (savefiletext.Statue == Enums.Statue.Success)
            {
                ProductViewModels.Book.FileText = savefiletext.FilePath;
            }
            else
            {
                ProductViewModels.Book.FileText = Editing_book.FileText;

            }
            if (savefileaudio.Statue == Enums.Statue.Success)
            {
                ProductViewModels.Book.FileAudio = savefileaudio.FilePath;
            }
            else
            {
                ProductViewModels.Book.FileAudio = Editing_book.FileAudio;

            }

            //if (ProductViewModels.ProductScale.CatId == null)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "لطفا دسته بندی را انتخاب نمایید ";
            //    return result;
            //}
            ProductViewModels.Product.Date = DateTime.Now;
            ProductViewModels.Product.Type = ProductType.Book;

            //_db.Entry(ProductViewModels.Product).State = EntityState.Modified;
            // _db.SaveChanges();

            Editing_product.Date = ProductViewModels.Product.Date;
            Editing_product.Description = ProductViewModels.Product.Description;
            Editing_product.Discount = ProductViewModels.Product.Discount;
            Editing_product.Img = ProductViewModels.Product.Img;
            Editing_product.LanguageId = ProductViewModels.Product.LanguageId;
            Editing_product.Price = ProductViewModels.Product.Price;
            Editing_product.PriceWithDiscount = ProductViewModels.Product.Price - ((ProductViewModels.Product.Price * ProductViewModels.Product.Discount) / 100f);
            Editing_product.Title = ProductViewModels.Product.Title;
            Editing_product.ShareholderPercentForSell = ProductViewModels.Product.ShareholderPercentForSell;
            Editing_product.ShareholderUnitPrice = ProductViewModels.Product.ShareholderUnitPrice;
            Editing_product.ReferralRight = ProductViewModels.Product.ReferralRight;
            //_db.SaveChanges();





            Editing_book.AuthorId = ProductViewModels.Book.AuthorId;
            Editing_book.Barcode = ProductViewModels.Book.Barcode;
            Editing_book.FileAudio = ProductViewModels.Book.FileAudio;
            Editing_book.FileText = ProductViewModels.Book.FileText;
            Editing_book.PageCount = ProductViewModels.Book.PageCount;
            Editing_book.Partofbook = ProductViewModels.Book.Partofbook;
            Editing_book.PublisherId = ProductViewModels.Book.PublisherId;
            Editing_book.Size = ProductViewModels.Book.Size;
            Editing_book.SpeakerId = ProductViewModels.Book.SpeakerId;
            Editing_book.TranslatorId = ProductViewModels.Book.TranslatorId;



            var old_productSacales = _unitOfWork.ProductScaleGR
                .GetAllIncluding(g => g.ProductId == ProductViewModels.Product.Id).ToList();

            if (ProductViewModels.ProductScales.Count == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "تاثیر محصول یافت نشد";
                return result;
            }

            _unitOfWork.ProductScaleGR.DeleteRange(old_productSacales);

            foreach (var items in ProductViewModels.ProductScales)
            {
                try
                {
                    ProductScale ps = new ProductScale
                    {
                        CatId = items.CatId,
                        //Credit = items.Credit,
                        LevelId = items.LevelId,
                        //Percent = items.Percent,
                        Priority = items.Priority,
                        ProductId = ProductViewModels.Product.Id
                    };

                    _productScaleRepository.Create(ps);
                }
                catch
                {

                }

            }


            var saveproduct = _unitOfWork.ProductGR.Update(Editing_product);
            saveproduct = _bookRepository.Edit(Editing_book);

            if (saveproduct.Statue == Enums.Statue.Success)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "محصول ویرایش شد";
                return result;
            }
            result.Statue = Enums.Statue.Failure;
            result.Message = "محصول ویرایش نشد";
            return result;


        }

        public async Task<Result> EditCourse(ProductViewModels ProductViewModels, IFormFile Sample, List<VideoFileViewModel> Videos)
        {
            Result result = new Result();
            var resultAsync = await GetById(ProductViewModels.Product.Id, "");
            var Editing_product = resultAsync.Product;

            var Editing_Course = _unitOfWork.CourseGR.GetById(ProductViewModels.Course.Id);
            if (Editing_Course == null || Editing_Course.ProductId != Editing_product.Id)
            {
                result.Message = "اطلاعات دوره نامعتبر است";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            //var Editing_productscale = _productScaleRepository.GetById(ProductViewModels.ProductScale.Id).ProductScale;
            //if (Editing_productscale == null || Editing_productscale.ProductId != ProductViewModels.Product.Id)
            //{
            //    result.Message = "اطلاعات تاثیر دوره نامعتبر است";
            //    result.Statue = Enums.Statue.Failure;
            //    return result;
            //}

            var savesample = _fileRepository.SaveFile(Sample, "Product/Course", "100000");
            if (savesample.Statue == Enums.Statue.Success)
            {
                ProductViewModels.Course.SampleofCourse = savesample.FilePath;
            }
            else
            {
                ProductViewModels.Course.SampleofCourse = Editing_Course.SampleofCourse;
                ProductViewModels.Course.SampleofCourse = Editing_Course.SampleofCourse;
            }

            //if (ProductViewModels.ProductScale.CatId == null)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "لطفا دسته بندی را انتخاب نمایید ";
            //    return result;
            //}

            if (Videos == null || Videos.Count == 0)
            {
                var countoldvideos = Editing_Course.Videos.Count;
                if (countoldvideos == 0)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "لطفا فایل های آموزشی را ارسال کنید";
                    return result;
                }


            }

            //var checksum = _productScaleRepository.CheckSum(ProductViewModels.ProductScale.CatId,
            //    ProductViewModels.ProductScale.Percent);
            //if (checksum.Statue != Enums.Statue.Success)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "درصد وارد شده با احتساب مجموع درصدهای این دسته بندی بیش از 100 میباشد.";
            //    return result;
            //}


            Editing_product.Date = DateTime.Now;
            Editing_product.Type = ProductType.Course;
            Editing_product.Description = ProductViewModels.Product.Description;
            Editing_product.Discount = ProductViewModels.Product.Discount;
            Editing_product.Img = ProductViewModels.Product.Img;
            Editing_product.LanguageId = ProductViewModels.Product.LanguageId;
            Editing_product.Price = ProductViewModels.Product.Price;
            Editing_product.PriceWithDiscount = ProductViewModels.Product.Price - ((ProductViewModels.Product.Price * ProductViewModels.Product.Discount) / 100f);
            Editing_product.Title = ProductViewModels.Product.Title;
            Editing_product.ShareholderPercentForSell = ProductViewModels.Product.ShareholderPercentForSell;
            Editing_product.ShareholderUnitPrice = ProductViewModels.Product.ShareholderUnitPrice;
            Editing_product.ReferralRight = ProductViewModels.Product.ReferralRight;

            Editing_Course.TeacherId = ProductViewModels.Course.TeacherId;
            Editing_Course.SampleofCourse = ProductViewModels.Course.SampleofCourse;

            var old_productSacales = _unitOfWork.ProductScaleGR
                .GetAllIncluding(g => g.ProductId == ProductViewModels.Product.Id).ToList();

            if (ProductViewModels.ProductScales.Count == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "تاثیر محصول یافت نشد";
                return result;
            }

            _unitOfWork.ProductScaleGR.DeleteRange(old_productSacales);

            foreach (var items in ProductViewModels.ProductScales)
            {
                try
                {
                    ProductScale ps = new ProductScale
                    {
                        CatId = items.CatId,
                        //Credit = items.Credit,
                        LevelId = items.LevelId,
                        //Percent = items.Percent,
                        Priority = items.Priority,
                        ProductId = ProductViewModels.Product.Id
                    };

                    _productScaleRepository.Create(ps);
                }
                catch
                {

                }

            }
            Result saveproduct = new Result()
            {
                Statue = Enums.Statue.Success,
            };

            if (Videos != null && Videos.Count > 0)
            {

                foreach (var items in Videos)
                {

                    var savevideo = _fileRepository.SaveFile(items.File, "Product/Course", "1000000");

                    //var ffProbe = new NReco.VideoInfo.FFProbe();
                    Video vid = new Video();
                    //var path = HttpContext.Current.Server.MapPath(savevideo.FilePath);
                    vid.CourseId = ProductViewModels.Course.Id;
                    vid.File = savevideo.FilePath;
                    vid.Size = items.File.Length;
                    vid.Title = items.Title;
                    vid.VideoTime = DateTime.Now.AddHours(1); /*ffProbe.GetMediaInfo(path).Duration*/;

                    if (_unitOfWork.VideoGR.Add(vid).Statue != Enums.Statue.Success)
                    {
                        saveproduct.Statue = Enums.Statue.Failure;
                    }
                }

            }

            if (saveproduct.Statue == Enums.Statue.Success)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "محصول ویرایش شد";
                return result;
            }
            result.Statue = Enums.Statue.Failure;
            result.Message = "محصول ویرایش نشد";
            return result;


        }

        public async Task<Result> EditExam(ProductViewModels ProductViewModels)
        {
            Result result = new Result();
            var resultAsync = await GetById(ProductViewModels.Product.Id, "");
            var Editing_product = resultAsync.Product;
            if (Editing_product == null)
            {
                result.Message = "اطلاعات محصول نامعتبر است";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            var Editing_Exam = _examRepository.GetById(ProductViewModels.Exam.Id).Exam;
            if (Editing_Exam == null || Editing_Exam.ProductId != Editing_product.Id)
            {
                result.Message = "اطلاعات آزمون نامعتبر است";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            //var Editing_productscale = _productScaleRepository.GetById(ProductViewModels.ProductScale.Id).ProductScale;
            //if (Editing_productscale == null || Editing_productscale.ProductId != ProductViewModels.Product.Id)
            //{
            //    result.Message = "اطلاعات تاثیر آزمون نامعتبر است";
            //    result.Statue = Enums.Statue.Failure;
            //    return result;
            //}


            //if (ProductViewModels.ProductScale.CatId == null)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "لطفا دسته بندی را انتخاب نمایید ";
            //    return result;
            //}



            //var checksum = _productScaleRepository.CheckSum(ProductViewModels.ProductScale.CatId,
            //    ProductViewModels.ProductScale.Percent);
            //if (checksum.Statue != Enums.Statue.Success)
            //{
            //    result.Statue = Enums.Statue.Failure;
            //    result.Message = "درصد وارد شده با احتساب مجموع درصدهای این دسته بندی بیش از 100 میباشد.";
            //    return result;
            //}


            Editing_product.Date = DateTime.Now;
            Editing_product.Type = ProductType.Exam;
            Editing_product.Description = ProductViewModels.Product.Description;
            Editing_product.Discount = ProductViewModels.Product.Discount;
            Editing_product.Img = ProductViewModels.Product.Img;
            Editing_product.LanguageId = ProductViewModels.Product.LanguageId;
            Editing_product.Price = ProductViewModels.Product.Price;
            Editing_product.PriceWithDiscount = ProductViewModels.Product.Price - ((ProductViewModels.Product.Price * ProductViewModels.Product.Discount) / 100f);
            Editing_product.Title = ProductViewModels.Product.Title;
            Editing_product.ShareholderPercentForSell = ProductViewModels.Product.ShareholderPercentForSell;
            Editing_product.ShareholderUnitPrice = ProductViewModels.Product.ShareholderUnitPrice;
            Editing_product.ReferralRight = ProductViewModels.Product.ReferralRight;

            Editing_Exam.AcceptancePerecentage = ProductViewModels.Exam.AcceptancePerecentage;
            Editing_Exam.DesignerId = ProductViewModels.Exam.DesignerId;
            Editing_Exam.ExamTime = ProductViewModels.Exam.ExamTime;
            Editing_Exam.ExamType = ProductViewModels.Exam.ExamType;
            Editing_Exam.Status = ProductViewModels.Exam.Status;

            var old_productSacales = _unitOfWork.ProductScaleGR
                .GetAllIncluding(g => g.ProductId == ProductViewModels.Product.Id).ToList();
            _unitOfWork.ProductScaleGR.DeleteRange(old_productSacales);
            //کپی پروداکت اسکیلهای محصول ارتباط داده شده به آزمون برای خود آزمون
            var newProductScals = ProductViewModels.ProductScales.Select(s => new ProductScale()
            {
                CatId = s.CatId,
                LevelId = s.LevelId,
                ProductId = ProductViewModels.Product.Id,
                Priority = s.Priority,
                Credit = s.Credit,
                Point = s.Point
            }).ToList();
            var addRangeResult = _unitOfWork.ProductScaleGR.AddRange(newProductScals);
            if (addRangeResult.Statue == Enums.Statue.Failure)
            {
                return addRangeResult;
            }

            Editing_product.ProductId = ProductViewModels.Product.ProductId;

            var saveproduct = _unitOfWork.ProductGR.Update(Editing_product);
            saveproduct = _examRepository.Edit(Editing_Exam);

            if (saveproduct.Statue == Enums.Statue.Success)
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "محصول ویرایش شد";
                return result;
            }
            result.Statue = Enums.Statue.Failure;
            result.Message = "محصول ویرایش نشد";
            return result;


        }



        public Result Delete(int? Id)
        {
            Result result = new Result();
            try
            {
                if (Id != null)
                {
                    Product Product = _unitOfWork.ProductGR.GetById(Id.Value);
                    _unitOfWork.ProductGR.Delete(Product);
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



        public async Task<ResultProduct> GetById(int? Id, string loginUserId)
        {
            ResultProduct Result = new ResultProduct();
            ProductVm productvm = new ProductVm();

            try
            {
                if (Id != null)
                {
                    var product = await _unitOfWork.ProductGR.FirstOrDefaultAsync(f => f.Id == Id);
                    var producttype = product.Type;

                    if (producttype == ProductType.Book)
                    {
                        productvm.Product = await _unitOfWork.ProductGR.GetAll()
                            .Include(p => p.Favorites)
                            .Include(p => p.Gift)
                            .Include(p => p.Comments)
                            .Include(p => p.FactorforsaleProducts)
                            .Include(p => p.Language)
                            .Include(p => p.Books).ThenInclude(p => p.Author)
                            .Include(p => p.Books).ThenInclude(p => p.Publishers)
                            .Include(p => p.Books).ThenInclude(p => p.Speakers)
                            .Include(p => p.Books).ThenInclude(p => p.Translators)
                            .FirstOrDefaultAsync(p => p.Id == Id.Value);

                    }
                    else if (producttype == ProductType.Course)
                    {
                        productvm.Product = await _unitOfWork.ProductGR.GetAll()
                            .Include(p => p.Favorites)
                            .Include(p => p.Gift)
                            .Include(p => p.Comments)
                            .Include(p => p.FactorforsaleProducts)
                            .Include(p => p.Language)
                            .Include(p => p.Courses).ThenInclude(p=>p.Videos)
                            .Include(p => p.Courses).ThenInclude(p => p.Teacher)
                            .FirstOrDefaultAsync(p => p.Id == Id.Value);


                    }
                    else
                    {
                        productvm.Product = await _unitOfWork.ProductGR.GetAll()
                            .Include(p => p.Gift)
                            .Include(p => p.Favorites)
                            .Include(p => p.Comments)
                            .Include(p => p.FactorforsaleProducts)
                            .Include(p => p.Language)
                            .Include(p => p.Exams).ThenInclude(p => p.Designer)
                            .Include(p => p.Exams).ThenInclude(p => p.Questions).ThenInclude(p => p.Answers)
                            .Include(p => p.Exams).ThenInclude(p => p.ExamResults)
                            .Include(p => p.Exams)
                            .FirstOrDefaultAsync(p => p.Id == Id.Value);


                    }

                    productvm.Book = productvm.Product.Books?.FirstOrDefault();
                    productvm.Exam = productvm.Product.Exams?.FirstOrDefault();
                    productvm.Course = productvm.Product.Courses?.FirstOrDefault();
                    if (productvm.Product.Comments.Where(p => p.Rank != null).Any())
                    {

                        productvm.RateCount = productvm.Product.Comments.Where(p => p.Rank != null).Count();

                        var commentCount = productvm.RateCount;
                        if (productvm.Product.Comments.Where(p => p.Rank > 0 && p.Rank <= 1).Any())
                        {
                            productvm.PercentStar1 = (productvm.Product.Comments.Where(p => p.Rank > 0 && p.Rank <= 1).Count() / float.Parse(commentCount.ToString())) * 100;
                        }

                        if (productvm.Product.Comments.Where(p => p.Rank > 1 && p.Rank <= 2).Any())
                        {
                            productvm.PercentStar2 = (productvm.Product.Comments.Where(p => p.Rank > 1 && p.Rank <= 2).Count() / float.Parse(commentCount.ToString())) * 100;
                        }
                        if (productvm.Product.Comments.Where(p => p.Rank > 2 && p.Rank <= 3).Any())
                        {
                            productvm.PercentStar3 = (productvm.Product.Comments.Where(p => p.Rank > 2 && p.Rank <= 3).Count() / float.Parse(commentCount.ToString())) * 100;
                        }
                        if (productvm.Product.Comments.Where(p => p.Rank > 3 && p.Rank <= 4).Any())
                        {
                            productvm.PercentStar3 = (productvm.Product.Comments.Where(p => p.Rank > 3 && p.Rank <= 4).Count() / float.Parse(commentCount.ToString())) * 100;
                        }
                        if (productvm.Product.Comments.Where(p => p.Rank > 4 && p.Rank <= 5).Any())
                        {
                            productvm.PercentStar3 = (productvm.Product.Comments.Where(p => p.Rank > 4 && p.Rank <= 5).Count() / float.Parse(commentCount.ToString())) * 100;
                        }
                        productvm.Star1 = productvm.Product.Comments.Where(p => p.Rank > 0 && p.Rank <= 1).Count();
                        productvm.Star2 = productvm.Product.Comments.Where(p => p.Rank > 1 && p.Rank <= 2).Count();
                        productvm.Star3 = productvm.Product.Comments.Where(p => p.Rank > 2 && p.Rank <= 3).Count();
                        productvm.Star4 = productvm.Product.Comments.Where(p => p.Rank > 3 && p.Rank <= 4).Count();
                        productvm.Star5 = productvm.Product.Comments.Where(p => p.Rank > 4 && p.Rank <= 5).Count();
                    }

                    //بعدا قراره کامپوننت باشه
                    //productvm.Product.Comments = productvm.Product.Comments.Where(p => p.Rank == null && p.FirstParentId == null).OrderByDescending(p => p.Id).Take(3).ToList();
                    productvm.Product.IsFavorite = product.Favorites.Any(f => f.UserId == loginUserId);
                    if (productvm.Product.FactorforsaleProducts.Any(f =>f.UserId == loginUserId && f.BuyType == BuyType.Normal) || productvm.Product.Gift.Any(p => p.UserResiv == loginUserId))
                    {
                        productvm.IsBuyed = true;
                    }
                    if (productvm.Product != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.ProductVm = productvm;
                        //Result.Product = productvm.Product;
                        Result.ProductType = productvm.Product.Type;
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

        public ResultProduct GetByName(string name)
        {
            ResultProduct result = new ResultProduct();
            try
            {

                var productList = _unitOfWork.ProductGR.GetAllIncluding(w => string.IsNullOrEmpty(name) ? w.Id != 0 : w.Title.Contains(name),
                                                                  p => p.Books.Select(b => b.Author),
                                                                  p => p.Courses.Select(b => b.Teacher),
                                                                  p => p.Exams.Select(b => b.Designer))
                                                    .Select(product => new ProductSummaryViewModels
                                                    {
                                                        Id = product.Id,
                                                        Title = product.Title,
                                                        Img = product.Img,
                                                        Author = product.Books.Any() ? product.Books.FirstOrDefault().Author.FullName : "",
                                                        Teacher = product.Courses.Any() ? product.Courses.FirstOrDefault().Teacher.FullName : "",
                                                        Designer = product.Exams.Any() ? product.Exams.FirstOrDefault().Designer.FullName : "",
                                                        Price = product.Price,
                                                        PriceWithDiscount = product.PriceWithDiscount,
                                                        Type = product.Type,
                                                        Rate = product.Rate
                                                    }).ToList();

                if (productList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ProductsSummary = productList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.ProductsSummary = productList;
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
        public ResultProduct GetByCatId(int? id)
        {
            ResultProduct result = new ResultProduct();
            try
            {

                var productList = _unitOfWork.ProductScaleGR.GetAllIncluding(g => g.CatId == id, g => g.Product)

                    .Select(product => new ProductSummaryViewModels
                    {
                        Id = product.Product.Id,
                        Title = product.Product.Title,
                        Img = product.Product.Img,
                        Author = product.Product.Books.Any() ? product.Product.Books.FirstOrDefault().Author.FullName : "",
                        Teacher = product.Product.Courses.Any() ? product.Product.Courses.FirstOrDefault().Teacher.FullName : "",
                        Designer = product.Product.Exams.Any() ? product.Product.Exams.FirstOrDefault().Designer.FullName : "",
                        Price = product.Product.Price,
                        PriceWithDiscount = product.Product.PriceWithDiscount,
                        Type = product.Product.Type,
                        Rate = product.Product.Rate
                    }).ToList();





                if (productList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ProductsSummary = productList.ToList();
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.ProductsSummary = productList;
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

        public ResultProduct GetAll(ProductType? productType)
        {
            ResultProduct result = new ResultProduct();
            try
            {

                var productList = _unitOfWork.ProductGR.GetAllIncluding(w => productType == null ? w.Id != 0 : w.Type == productType, w => w.Exams);

                if (productList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Products = productList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.Products = productList;
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

        /// <summary>
        /// این تابع مسئول تهیه یک لیست خلاصه از محصولات است فیلدهای اضافی را حذف میکند و 
        ///  اینکلودهای لازم را بر اساس نوع محصول انجام میدهد
        ///  در اخرین تغییر اگر ای دی یک پروداکتر به ان ارسال شود فقط محصولات ان پروداکتر
        ///  را ارسال میکند
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="returnCount"></param>
        /// <param name="getProductType"></param>
        /// <param name="productType"></param>
        /// <param name="categoryId"></param>
        public ResultShop GetProductsSummary(int? pageNumber, int? returnCount, ProductSearchType? productSearchType, ProductType? productType, int? categoryId, int? producterId, string UserId, BuyType? buyType, GiftType? giftType, string textSearch)
        {
            ResultShop list = new ResultShop();
            IQueryable<Product> products = _unitOfWork.ProductGR.GetAll();


            //FilterBy ProductType-----------------------------------------------------------------------------------------------------

            #region FilterBy ProductType

            if (productType != null)
            {
                products = products.Where(p => p.Type == productType);
            }

            #endregion

            //FilterBy textSearch--------------------------------------------------------------------------------------------------

            #region FilterBy textSearch

            if (!String.IsNullOrEmpty(textSearch))
            {
                products = products.Where(p => p.Title == textSearch);
            }

            #endregion

            //FilterBy GetProductType--------------------------------------------------------------------------------------------------
            #region FilterBy GetProductType

            if (productSearchType == ProductSearchType.NewProducts)
            {
                products = products.OrderByDescending(p => p.Id);
            }
            else if (productSearchType == ProductSearchType.SuggestionProducts && !String.IsNullOrEmpty(UserId))
            {
                //var SuggestedProduct = _unitOfWork.SuggestedProductGR.FindBy(
                //         w => w.UserId == suggestUserId && w.Product.Type == productType)
                //     .OrderByDescending(o => o.VisionCatId != null)
                //     .ThenBy(t => t.SkillPriorityFV)
                //     .ThenBy(t => t.ProductPriorityFV).Select(s => s.Product);//اگر به این صورت میشد پایین تر نمیشد اینکلود کرد چون الان اینجا سلکت شده

                products = products.Where(w => w.SuggestedProducts.Any(w => w.UserId == UserId && w.Product.Type == productType))
                    .OrderByDescending(o => o.SuggestedProducts.Any(so => so.VisionCatId != null))
                    .ThenBy(o => o.SuggestedProducts.FirstOrDefault().SkillPriorityFV)
                    .ThenBy(o => o.SuggestedProducts.FirstOrDefault().ProductPriorityFV);
            }
            else if (productSearchType == ProductSearchType.BestSellerProducts)
            {
                products = products.Where(p => p.SoldCount != 0).OrderByDescending(p => p.SoldCount);
            }
            else if (productSearchType == ProductSearchType.RelatedProducts && categoryId != null)
            {
                products = products.Include(p => p.ProductScale).ThenInclude(ps => ps.Category).Where(p => (p.ProductScale.Any() ? p.ProductScale.Where(ps => ps.Category.Id == categoryId.Value).Any() : false));
            }
            else if (productSearchType == ProductSearchType.MyProducts && !String.IsNullOrEmpty(UserId) && buyType != null)
            {
                if (buyType == BuyType.Normal)
                {
                    products = products.Where(w => w.FactorforsaleProducts.Any(w => w.Bill.Status == 1 && w.UserId == UserId && (productType == null ? w.Id != 0 : w.Products.Type == productType) && w.BuyType == buyType));

                }
                else if (buyType == BuyType.Gift && giftType != null)
                {
                    if (giftType == GiftType.GiftSend)
                    {
                        products = products.Where(f => f.Gift.Any(w => (w.UserSend == UserId) && w.Products.FactorforsaleProducts.Any(v => v.UserId == UserId)));
                    }
                    else
                    {
                        products = products.Where(f => f.Gift.Any(w => (w.UserResiv == UserId)));

                    }
                }
                else if (buyType == BuyType.Shareholder)
                {
                    products = products.Where(f => f.Shareholders.Any(w => (w.UserId == UserId)));
                }
                else
                {
                    products = products.Where(f => f.Favorites.Any(w => (w.UserId == UserId)));
                }
            }
            else
            {
                products = products.OrderByDescending(p => p.Id);
            }

            #endregion

            //IncludeBy ProductType-----------------------------------------------------------------------------------------------------

            #region IncludeBy ProductType

            switch (productType)
            {
                case ProductType.Book:
                    products = products
                        .Include(p => p.Books).ThenInclude(b => b.Author)
                        .Include(p => p.Books).ThenInclude(b => b.Publishers)
                        .Include(p => p.Books).ThenInclude(b => b.Speakers)
                        .Include(p => p.Books).ThenInclude(b => b.Translators);
                    break;
                case ProductType.Course:
                    products = products.Include(p => p.Courses).ThenInclude(b => b.Teacher);
                    break;
                case ProductType.Exam:
                    products = products.Include(p => p.Exams).ThenInclude(b => b.Designer);
                    break;
                case null:
                    if (buyType == BuyType.Gift && giftType != null)
                    {
                        if (giftType == GiftType.GiftSend)
                        {
                            products = products.Include(i => i.Gift).ThenInclude(ti => ti.SendUsers);
                        }
                        else
                        {
                            products = products.Include(i => i.Gift).ThenInclude(ti => ti.ResiveUsers);
                        }
                    }
                    else
                    {
                        products = products.Include(p => p.Exams).ThenInclude(b => b.Designer)
                                                .Include(p => p.Courses).ThenInclude(b => b.Teacher)
                                                .Include(p => p.Books).ThenInclude(b => b.Author)
                                                .Include(p => p.Books).ThenInclude(b => b.Publishers)
                                                .Include(p => p.Books).ThenInclude(b => b.Speakers)
                                                .Include(p => p.Books).ThenInclude(b => b.Translators);
                    }


                    break;

            }

            #endregion
            //Filter By producter-----------------------------------------------------------------------------------------------------------------

            #region ProducterId
            if (producterId != null && producterId != 0)
            {
                switch (productType)
                {
                    case ProductType.Book:
                        products = products.Where(p => p.Books.Any(b => b.TranslatorId == producterId) ||
                                                       p.Books.Any(b => b.PublisherId == producterId) ||
                                                       p.Books.Any(b => b.SpeakerId == producterId) ||
                                                       p.Books.Any(b => b.AuthorId == producterId));
                        break;

                    case ProductType.Course:
                        products = products.Where(p => p.Courses.Any(b => b.TeacherId == producterId));
                        break;

                    case ProductType.Exam:
                        products = products.Where(p => p.Exams.Any(b => b.DesignerId == producterId));
                        break;

                    case null:
                        products = products.Where(p => p.Books.Any(b => b.AuthorId == producterId) ||
                                                       p.Books.Any(b => b.PublisherId == producterId) ||
                                                       p.Books.Any(b => b.SpeakerId == producterId) ||
                                                       p.Books.Any(b => b.TranslatorId == producterId) ||
                                                       p.Exams.Any(e => e.DesignerId == producterId) ||
                                                       p.Courses.Any(c => c.TeacherId == producterId));
                        break;

                }
            }

            #endregion

            //LazyLoad-----------------------------------------------------------------------------------------------------------------

            #region LazyLoad
            if (pageNumber != null)
            {
                if (returnCount == null)
                {
                    returnCount = 10;
                }
                var count = products.Count();
                list.Count = count / returnCount.Value;
                list.Count = count % returnCount.Value > 0 ? list.Count + 1 : list.Count;
                if ((count - (pageNumber * returnCount)) >= returnCount)
                {
                    products = products.Skip(pageNumber.Value * returnCount.Value).Take(returnCount.Value);
                }
                else if ((pageNumber * returnCount) < count)
                {
                    products = products.Skip(pageNumber.Value * returnCount.Value);
                }
                else
                {
                    list.ProductsSummary = new List<ProductSummaryViewModels>();
                    return list;
                }
            }

            #endregion

            //Summary------------------------------------------------------------------------------------------------------------------

            #region return Summary

            list.ProductsSummary = products.Select(product => new ProductSummaryViewModels
            {
                Id = product.Id,
                Title = product.Title,
                Img = product.Img,
                Speaker = product.Books.Any() ? (product.Books.FirstOrDefault().Speakers != null ? product.Books.FirstOrDefault().Speakers.FullName : "") : "",
                Translator = product.Books.Any() ? (product.Books.FirstOrDefault().Translators != null ? product.Books.FirstOrDefault().Translators.FullName : "") : "",
                Publisher = product.Books.Any() ? (product.Books.FirstOrDefault().Publishers != null ? product.Books.FirstOrDefault().Publishers.FullName : "") : "",
                Author = product.Books.Any() ? (product.Books.FirstOrDefault().Author != null ? product.Books.FirstOrDefault().Author.FullName : "") : "",

                Teacher = product.Courses.Any() ? (product.Courses.FirstOrDefault().Teacher != null ? product.Courses.FirstOrDefault().Teacher.FullName : "") : "",
                
                Designer = product.Exams.Any() ? (product.Exams.FirstOrDefault().Designer != null ? product.Exams.FirstOrDefault().Designer.FullName : "") : "",
                Rate = product.Rate,
                Price = product.Price,
                PriceWithDiscount = product.PriceWithDiscount,
                Type = product.Type,
                ShareHolderPercent= product.Shareholders.Any() ? product.Shareholders.FirstOrDefault().Percent : 0,
                ShareHolderUnit=product.ShareholderUnitPrice,
                UserSend = product.Gift.Any() ? product.Gift.FirstOrDefault().SendUsers.UserName : null,
                UserRecive = product.Gift.Any() ? product.Gift.FirstOrDefault().SendUsers.UserName : null,

            }).DistinctBy(d => d.Id).ToList();

            return list;

            #endregion
        }

    }

}
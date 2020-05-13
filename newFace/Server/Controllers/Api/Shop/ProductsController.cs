using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;


namespace Foursat.Controllers.Api.Education
{
    [ApiController]
    public class ProductsController : Controller
    {


        private readonly IProductRepository _ProductRepository;
        private readonly IQuestionRepository _questionRepository;
        private readonly IExamRepository _examRepository;
        private readonly IProductSeenInfoRepository _ProductSeenInfoRepository;

        private readonly IBookRepository _bookRepository;

        private readonly IUserRepository _userRepository;
        private readonly IShopRepository _shopRepository;
        private readonly IGiftRepository _giftRepository;
        private IUnitOfWork _unitOfWork;

        public ProductsController(IProductRepository productRepository, IQuestionRepository questionRepository, IExamRepository examRepository, IProductSeenInfoRepository productSeenInfoRepository, IBookRepository bookRepository, IUserRepository userRepository, IShopRepository shopRepository, IGiftRepository giftRepository, IUnitOfWork unitOfWork)
        {
            _ProductRepository = productRepository;
            _questionRepository = questionRepository;
            _examRepository = examRepository;
            _ProductSeenInfoRepository = productSeenInfoRepository;
            _bookRepository = bookRepository;
            _userRepository = userRepository;
            _shopRepository = shopRepository;
            _giftRepository = giftRepository;
            _unitOfWork = unitOfWork;
        }


        [HttpPost, Route("api/GetBookByProductId")]
        public ResultBook book([FromBody] ProductsRequest model)
        {
            var result = new ResultBook();

            var checkUser = _userRepository.GetByToken(model.Token);
            if (checkUser.Statue == Enums.Statue.AccessDenied)
            {
                result.Message = checkUser.Message;
                result.Statue = checkUser.Statue;
                return result;

            }
            if (model.Id != null)
            {
                if (!_unitOfWork.FactorforsaleProductGR.Any(p => p.UserId == checkUser.User.Id && p.ProductId == model.Id && p.BuyType == BuyType.Normal) && !_unitOfWork.GiftGR.Any(p => p.PorductId == model.Id && p.UserResiv == checkUser.User.Id))
                {
                    result.Message = "شما مجاز به مشاهده این محصول نیستید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }
            }
            result.Book = _unitOfWork.BookGR.FirstOrDefault(p => p.ProductId == model.Id);
            result.Message = "با موفقیت ارسال شد";
            result.Statue = Enums.Statue.Success;

            return result;
        }

        [HttpPost, Route("api/GetCourseByProductId")]
        public ResultCourse course([FromBody] ProductsRequest model)
        {
            var result = new ResultCourse();

            var checkUser = _userRepository.GetByToken(model.Token);
            if (checkUser.Statue == Enums.Statue.AccessDenied)
            {
                result.Message = checkUser.Message;
                result.Statue = checkUser.Statue;
                return result;

            }
            if (model.Id != null)
            {
                if (model.BuyType == BuyType.Normal)
                {
                    var product = _shopRepository.GetProducts(checkUser.User.Id, null, model.BuyType);
                    if (!product.Products.Any(p => p.Id == model.Id))
                    {
                        result.Message = "شما مجاز به مشاهده این محصول نیستید";
                        result.Statue = Enums.Statue.Failure;
                        return result;
                    }
                }
                else if (model.BuyType == BuyType.Gift)
                {
                    var gifts = _giftRepository.GetAll(checkUser.User.Id);
                    if (!gifts.GiftList.Any(p => p.PorductId == model.Id))
                    {
                        result.Message = "شما مجاز به مشاهده این محصول نیستید";
                        result.Statue = Enums.Statue.Failure;
                        return result;
                    }
                }
                else
                {
                    result.Message = "شما مجاز به مشاهده این محصول نیستید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }



            }
            result.Course = _unitOfWork.CourseGR.FirstOrDefault(p => p.ProductId == model.Id);
            result.Message = "با موفقیت ارسال شد";
            result.Statue = Enums.Statue.Success;

            return result;

        }

        [HttpPost, Route("api/GetCourseVideos")]
        public ResultCourse GetCourseVideos([FromBody] Request model)
        {
            ResultCourse resultCourse = new ResultCourse();

            resultCourse.Statue = Enums.Statue.Success;
            resultCourse.Videos = _unitOfWork.VideoGR.GetAll().Where(v => v.CourseId == model.Id).ToList();

            return resultCourse;
        }

        [HttpPost, Route("api/GetQuestionsByExamId")]
        public ResultQuestionAnswerList Exam([FromBody] Request model)
        {
            ResultQuestionAnswerList result = new ResultQuestionAnswerList();

            var exam = _questionRepository.GetAllByExamId(model.Id);



            result.QuestionAnswerList = exam.QuestionAnswerList;
            result.Statue = Enums.Statue.Success;
            return result;


        }

        [HttpPost, Route("api/ReadProduct")]
        public ResultProduct readproduct([FromBody] Request model)
        {
            ResultProduct result = new ResultProduct();

            if (model.Id == 0)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه محصول را وارد نمایید";
                return result;
            }


            var r = _ProductRepository.GetById(model.Id, "").Result;

            if (r.Product.Type == Enums.ProductType.Book)
            {
                r.Product = null;
                r.ProductVm.Product.Books.FirstOrDefault().Author = null;

                r.ProductVm.Product.Books.FirstOrDefault().Speakers.Exams = null;

            }

            return r;
        }

        [HttpPost, Route("api/GetAnswers")]
        public ResultExam getresult([FromBody] RequestExam model)
        {
            ResultExam result = new ResultExam();
            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            var r = _examRepository.SaveResultExam(model.UserId, model.UserAnswerVms, model.ExamId, model.StatusTypeQuestion);
            return r;
        }


        [HttpPost, Route("api/CompleteReadProduct")]
        public Result CompleteReadProduct([FromBody] RequestProduct model)
        {
            Result result = new Result();

            if (string.IsNullOrEmpty(model.UserId))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه کاربر را وارد نمایید";
                return result;
            }

            if (model.ProductId != null)
            {
                var product = _ProductRepository.GetById(model.ProductId, "").Result;
                if (product != null)
                {
                    if (product.ProductVm.Product.Type == Enums.ProductType.Book)
                    {
                        var resultReadProduct = _ProductSeenInfoRepository.CompleteReadProduct(product.ProductVm.Product.Id, model.UserId, null);

                        if (resultReadProduct > 0)
                        {
                            result.Statue = Enums.Statue.Success;
                            result.Message = "اعتبار محصول با موفقیت برای کاربر اعمال شد";
                            return result;
                        }
                        else
                        {
                            result.Statue = Enums.Statue.Failure;
                            result.Message = " اعتبار برای کاربر اعمال نشد";
                            return result;
                        }
                    }
                }
            }

            if (model.VideoId != null)
            {
                var resultCompleteVideo = _ProductSeenInfoRepository.CompleteVideo(model.VideoId.Value, model.UserId);

                if (resultCompleteVideo)
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "ویدئو دیده شد";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "از موش میترسی ؟";
                    return result;
                }
            }




            result.Statue = Enums.Statue.Failure;
            result.Message = "خطایی رخ داده است";
            return result;
        }



        [HttpPost, Route("api/RatingExam")]
        public ResultExam RatingList([FromBody] Request model)
        {
            ResultExam result = new ResultExam();

            if (model.Id == null || model.Id == 0)
            {
                result.Message = "لطفا شناسه دسته بندی را وارد نمایید";
                result.Statue = Enums.Statue.Failure;
                return result;
            }

            if (model != null && !string.IsNullOrEmpty(model.Token))
            {
                var checkUser = _userRepository.GetByToken(model.Token);
                if (checkUser.Statue != Enums.Statue.Success)
                {
                    result.Message = "لطفا وارد شوید";
                    result.Statue = Enums.Statue.Failure;
                    return result;
                }

                model.UserId = checkUser.User.Id;
            }
            var products = _unitOfWork.ProductScaleGR.GetAllIncluding(g => g.CatId == model.Id).Select(s => s.Product).ToList();

            //products = products.Select(s => s.Product1).ToList();
            products = products.Where(w => w.Type != Enums.ProductType.Exam).ToList();

            List<Question> questions = new List<Question>();

            foreach (var item in products)
            {
                if (item != null)
                {
                    //یافتن آزمونهای مرتبط با محصول
                    var q = _ProductRepository.GetAll(Enums.ProductType.Exam).Products.Where(g => g.ProductId == item.Id).ToList();
                    //واکشی سوالات آزمون
                    foreach (var items in q)
                    {
                        //بررسی اینکه آیا محصول مرتبط قبلا خوانده شده و امتیاز آن محاسبه شده یا نه
                        var Done = _unitOfWork.ProductSeenInfoGR.FirstOrDefault(f => f.ProductId == item.Id && f.UserId == model.UserId);
                        if (Done == null)
                        {
                            var q2 = items.Exams.ToList();
                            foreach (var exam in q2)
                            {
                                var ques = _unitOfWork.QuestionGR.GetAllIncluding(w =>
                                     w.ExamId == exam.Id && w.StatusType == Enums.StatusTypeQuestion.Rating, i => i.Answers).ToList();

                                questions.AddRange(ques);
                            }
                        }

                    }
                }

            }

            result.Questions.AddRange(questions);
            result.Message = "سوالات آزمون تعیین سطح ارسال شد";
            result.Statue = Enums.Statue.Success;
            return result;


        }
    }

}

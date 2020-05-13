
using System.Collections.Generic;
using System.Linq;
using System;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using newFace.Shared;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Controllers.Api.Shop
{
    [ApiController]
    public class AShopsController : Controller
    {

        private readonly IShopRepository _shopRepository;
        private readonly IProductRepository _productRepository;
        private readonly IProductScaleRepository _productScaleRepository;
        private readonly IGiftRepository _giftRepository;
        private readonly IUserRepository _userRepository;
        private readonly IShareholderRepository _shareholderRepository;
        private IUnitOfWork _unitOfWork;

        public AShopsController(IShopRepository shopRepository, IProductRepository productRepository, IProductScaleRepository productScaleRepository, IGiftRepository giftRepository, IUserRepository userRepository, IShareholderRepository shareholderRepository, IUnitOfWork unitOfWork)
        {
            _shopRepository = shopRepository;
            _productRepository = productRepository;
            _productScaleRepository = productScaleRepository;
            _giftRepository = giftRepository;
            _userRepository = userRepository;
            _shareholderRepository = shareholderRepository;
            _unitOfWork = unitOfWork;
        }


        //[HttpPost,Route("api/[action]")]
        //public async Task<ActionResult<int>> list(testid model)
        //{
        //    return 2;
        //}
        //----------------------------------------------------------------------
        //[HttpGet, Route("api/test")]

        //public string testapi()
        //{
        //    return "Hello";
        //}

        [HttpPost, Route("api/Sliders")]
        public async Task<ResultShop> Sliders([FromBody] RequestShop model)
        {
            ResultShop result = new ResultShop();
            if (model != null && model.productType != null)
            {
                result.Sliders = await _unitOfWork.ShopHomeSliderGR.FindByAsync(f => f.producterType == model.productType);
                result.Sliders = result.Sliders.OrderBy(s => s.Order).ToList();
                result.Statue = Statue.Success;
                return result;
            }
            result.Statue = Statue.Failure;
            return result;
        }

        [HttpPost, Route("api/GetProducts")]
        public ResultShop ShopHome([FromBody] RequestShop model)
        {
            ResultShop result = new ResultShop();
            if (model != null)
            {
                if (model.productSearchType == ProductSearchType.SuggestionProducts && !String.IsNullOrEmpty(model.Token))
                {
                    var User = _userRepository.GetByToken(model.Token);
                    if (User.Statue == Enums.Statue.AccessDenied)
                    {
                        return new ResultShop() { Statue = User.Statue, Message = User.Message, ProductsSummary = new List<ProductSummaryViewModels>() { } };
                    }
                    result = _productRepository.GetProductsSummary(model.PageNumber, model.PageCount, model.productSearchType, model.productType, model.CatId, model.ProducterId, User.User.Id,null,null, model.Search);

                }
                else if (model.productSearchType == ProductSearchType.MyProducts && !String.IsNullOrEmpty(model.Token))
                {
                    var User = _userRepository.GetByToken(model.Token);
                    if (User.Statue == Enums.Statue.AccessDenied)
                    {
                        return new ResultShop() { Statue = User.Statue, Message = User.Message, ProductsSummary = new List<ProductSummaryViewModels>() { } };
                    }
                    result = _productRepository.GetProductsSummary(model.PageNumber, model.PageCount, model.productSearchType, model.productType, model.CatId, model.ProducterId, User.User.Id,model.buyType,model.giftType, model.Search);

                }
                else
                {
                    result = _productRepository.GetProductsSummary(model.PageNumber, model.PageCount, model.productSearchType, model.productType, model.CatId, model.ProducterId, null,null,null, model.Search);

                }
              
                result.Statue = Enums.Statue.Success;
                result.Message = result.ProductsSummary.Any() ? "لیست محصولات با موفقیت ارسال شد" : "متاسفانه موردی یافت نشد";
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "هیچی دریافت نشد :|";
                return result;
            }



        }

        //----------------------------------------------------------------------

        //[HttpPost, Route("api/GetProductsLazyLoad")]
        //public ResultProduct GetProductsLazyLoad([FromBody] RequestShop model)
        //{
        //    ResultProduct result = new ResultProduct();
        //    var checkUser = _userRepository.GetByToken(model.Token);
        //    if (checkUser.Statue == Enums.Statue.AccessDenied)
        //    {
        //        result.Message = checkUser.Message;
        //        result.Statue = checkUser.Statue;
        //        return result;

        //    }
        //    if (model != null)
        //    {
        //        if (model.productSearchType == Enums.ProductSearchType.SuggestionProducts)
        //        {
        //            result.ProductsSummary = _productRepository.GetProductsSummary(model.PageNumber, null, model.productSearchType, model.productType, model.CatId, model.ProducterId, checkUser.User.Id);

        //        }
        //        else
        //        {
        //            result.ProductsSummary = _productRepository.GetProductsSummary(model.PageNumber, null, model.productSearchType, model.productType, model.CatId, model.ProducterId, null);

        //        }
        //        result.Statue = Enums.Statue.Success;
        //        return result;
        //    }
        //    else
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "هیچی دریافت نشد :|";
        //        return result;
        //    }

        //}

        //----------------------------------------------------------------------

        //[HttpPost, Route("api/SearchProducts")]
        //public ResultShop SearchProducts([FromBody] RequestShop model)
        //{
        //    ResultShop result = new ResultShop();

        //    if (model != null && !string.IsNullOrEmpty(model.Search))
        //    {

        //        result.ProductsSummary = _productRepository.GetProductsSummary(null, null, null, null, null, null, null).Where(p => p.Title.Contains(model.Search)).ToList();
        //        result.Statue = Enums.Statue.Success;
        //        return result;
        //    }
        //    else
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "هیچی دریافت نشد :|";
        //        return result;
        //    }

        //}

        //----------------------------------------------------------------------

        //[HttpPost, Route("api/AllProducts")]
        //public ResultShop AllProducts([FromBody] RequestShop model)
        //{
        //    ResultShop result = new ResultShop();

        //    if (model != null && model.CatId != null)
        //    {

        //        result.ProductsSummary = _productScaleRepository.GetProductByCatId(model.CatId.Value, null).Products;
        //        result.Statue = Enums.Statue.Success;
        //        return result;
        //    }
        //    else
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "هیچی دریافت نشد :|";
        //        return result;
        //    }

        //}

        //----------------------------------------------------------------------

        [HttpPost, Route("api/GetProduct")]
        public async Task<ResultShop> GetProduct([FromBody] RequestShop model)
        {
            ResultShop result = new ResultShop();

            var checkUser = _userRepository.GetByToken(model.Token);
            if (checkUser.Statue == Enums.Statue.AccessDenied)
            {
                result.Message = checkUser.Message;
                result.Statue = checkUser.Statue;
                return result;
            }

            if (model != null && model.Id != null)
            {
                var resultAsync = await _productRepository.GetById(model.Id.Value, checkUser.User.Id);
                result.ProductVm = resultAsync.ProductVm;

                //result.Product = result.ProductVm.Product;



                //تو ورژن جدید محصولات مرتبط رو حذف کردیم چون جدا واس خودش یه کامپوننته

                //دسته ای که بیشترین تاثیر را در این محصول دارد
                //var categoryId = result.ProductVm.Product.ProductScale.OrderByDescending(c => c.LevelId).Select(p => p.Category).FirstOrDefault()?.Id;

                //result.CategoryId = categoryId;
                //result.ProductVm.RelatedProducts = _productRepository.GetProductsSummary(0, null, Enums.ProductSearchType.RelatedProducts, null, categoryId, null, null);
                //اینو بخواطر آرش برداشتم اگه کسی به مشکل خرد به آرش  با لباس پارش فش بده از طرف مسعود
                //result.RelatedProducts = _productRepository.GetProductsSummary(0, null, GetProductType.RelatedProducts, null, categoryId,null);

                //var currentProduct = result.RelatedProducts.FirstOrDefault(r => r.Id == model.Id);

                //result.RelatedProducts.Remove(currentProduct);



                result.Statue = Enums.Statue.Success;
                result.Message = "محصول با موفقیت ارسال شد";

                return result;
            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "هیچی دریافت نشد :|";
                return result;
            }

        }

        //----------------------------------------------------------------------
        [HttpPost, Route("api/GetProductByNameAndCatId")]
        public ResultProduct GetProductByNameAndCatId([FromBody] RequestShop model)
        {
            ResultProduct result = new ResultProduct();
            if (model.CatId != 0 && model.CatId != null)
            {
                result.ProductsSummary.AddRange(_productRepository.GetByCatId(model.CatId).ProductsSummary);
            }

            if (!String.IsNullOrWhiteSpace(model.Name) && !String.IsNullOrEmpty(model.Name))
            {
                result.ProductsSummary.AddRange(_productRepository.GetByName(model.Name).ProductsSummary);

            }
            if (!result.ProductsSummary.Any())
            {
                result.Message = "موردی یافت نشد.";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            else
            {
                result.Message = "موارد به درستی ارسال شد";
                result.Statue = Enums.Statue.Success;
                return result;
            }

        }

        //---------------------------deleted GetProductByCategoryId-------------------------------------------

        /// <summary>
        /// این سرویس توسط علی نوشته شده بود 
        /// کتاب خالی بر میگردوند و واسه
        /// امتحان و دوره خطا میداد مجبور شدم از اول بنویسم
        /// @Aspkar
        /// </summary>
        [HttpPost, Route("api/GetProducterInformation")]
        public ResultProducter GetProducterInformation([FromBody] RequestShop model)
        {
            ResultProducter result = new ResultProducter();
            if (model.Id == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه فرد مورد نظر را وارد نمایید";
                return result;
            }

            var producter = _unitOfWork.ProducterGR.FirstOrDefault(s => s.Id == model.Id);

            if (producter == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "هیچ موردی یافت نشد";
                return result;
            }

            result.Producter = producter;
            var AllType = _productRepository.GetProductsSummary(0, null, null, null, null, model.Id, null,null,null, model.Search);
            result.Books = AllType.ProductsSummary.Where(w => w.Type == ProductType.Book).ToList();
            result.Exams = AllType.ProductsSummary.Where(w => w.Type == ProductType.Exam).ToList();
            result.Courses = AllType.ProductsSummary.Where(w => w.Type == ProductType.Course).ToList();



            //var books = _serviceBooks.GetAllIncluding(w => (w.AuthorId == model.Id) ||
            //                                               (w.PublisherId == model.Id) ||
            //                                               (w.SpeakerId == model.Id) ||
            //                                               (w.TranslatorId == model.Id), g => g.Products)
            //                         .ToList()
            //                         .Select(product => new Book
            //                         {
            //                             Id = product.Id,
            //                             Products = new Product
            //                             {
            //                                 Id = product.ProductId,
            //                                 Type = product.Products.Type,
            //                                 Date = product.Products.Date,
            //                                 Description = product.Products.Description,
            //                                 Discount = product.Products.Discount,
            //                                 Price = product.Products.Price,
            //                                 Img = product.Products.Img,
            //                                 Title = product.Products.Title
            //                             }
            //                         }).ToList()
            ;


            //var producter = _serviceProducters.GetAllIncluding(g => g.Id == model.Id, g => g.Books, g => g.Courses, g => g.Exams)
            //                                  .ToList();

            //var producter2 = producter.ToList()
            //                          .Select(s => new Producter
            //                          {
            //                              Id = s.Id,
            //                              Exams = _serviceExams.GetAllIncluding(g => g.DesignerId == model.Id).ToList()
            //                                                   .Select(product => new Exam
            //                                                   {
            //                                                       Id = product.Id,
            //                                                       Products = new Product
            //                                                       {
            //                                                           Id = product.ProductId,
            //                                                           Type = product.Products.Type,
            //                                                           Date = product.Products.Date,
            //                                                           Description = product.Products.Description,
            //                                                           Discount = product.Products.Discount,
            //                                                           Price = product.Products.Price,
            //                                                           Img = product.Products.Img,
            //                                                           Title = product.Products.Title
            //                                                       }
            //                                                   }).ToList(),
            //                              Books = books,
            //                              Type = s.Type,
            //                              Courses = _serviceCourse.GetAll().Where(g => g.TeacherId == model.Id).ToList()
            //                                                      .Select(product => new Course
            //                                                      {
            //                                                          Id = product.Id,
            //                                                          Products = new Product
            //                                                          {
            //                                                              Id = product.ProductId,
            //                                                              Type = product.Products.Type,
            //                                                              Date = product.Products.Date,
            //                                                              Description = product.Products.Description,
            //                                                              Discount = product.Products.Discount,
            //                                                              Price = product.Products.Price,
            //                                                              Img = product.Products.Img,
            //                                                              Title = product.Products.Title
            //                                                          }
            //                                                      }).ToList(),
            //                              Description = s.Description,
            //                              FullName = s.FullName
            //                          }).FirstOrDefault();

            //result.Producter = producter2;
            result.Statue = Enums.Statue.Success;
            result.Message = "اطلاعات با موفقیت ارسال شد";
            return result;

        }


        //----------------------------------------------------------------------

        //[HttpPost, Route("api/MyProducts")]
        //public ResultShop myproducts([FromBody] Request model)
        //{
        //    ResultShop result = new ResultShop();
        //    if (model != null && !string.IsNullOrEmpty(model.Token))
        //    {
        //        var checkUser = _userRepository.GetByToken(model.Token);
        //        if (checkUser.Statue != Enums.Statue.Success)
        //        {
        //            result.Message = "لطفا وارد شوید";
        //            result.Statue = Enums.Statue.Failure;
        //            return result;
        //        }

        //        model.UserId = checkUser.User.Id;
        //    }

        //    var getProductsResult = _shopRepository.GetProducts(model.UserId, model.productType, BuyType.Normal);

        //    List<ProductSummaryViewModels> list = new List<ProductSummaryViewModels>();
        //    if (getProductsResult.Products != null && getProductsResult.Products.Any())
        //    {
        //        list = getProductsResult.Products.Select(product => new ProductSummaryViewModels
        //        {
        //            Id = product.Id,
        //            Title = product.Title,
        //            Img = product.Img,
        //            Author = product.Books.Any() ? (product.Books.FirstOrDefault().Author != null ? product.Books.FirstOrDefault().Author.FullName : "") : "",
        //            Teacher = product.Courses.Any() ? (product.Courses.FirstOrDefault().Teacher != null ? product.Courses.FirstOrDefault().Teacher.FullName : "") : "",
        //            Designer = product.Exams.Any() ? (product.Exams.FirstOrDefault().Designer != null ? product.Exams.FirstOrDefault().Designer.FullName : "") : "",
        //            Price = product.Price,
        //            PriceWithDiscount = product.PriceWithDiscount,
        //            Type = product.Type,
        //            Rate = product.Comments != null ? product.Comments.FirstOrDefault(c => c.UserId == model.UserId && c.Rank != null)?.Rank != null ? (float)product.Comments.FirstOrDefault(c => c.UserId == model.UserId && c.Rank != null)?.Rank : 0 : 0
        //        }).ToList();
        //    }

        //    result.Statue = Enums.Statue.Success;
        //    result.ProductsSummary = list;

        //    return result;
        //}
        //----------------------------------------------------------------------

        //[HttpPost, Route("api/MyGifts")]
        //public ResultGift mygiftss([FromBody] Request model)
        //{
        //    ResultGift result = new ResultGift();
        //    var checkUser = _userRepository.GetByToken(model.Token);
        //    if (checkUser.Statue == Enums.Statue.AccessDenied)
        //    {
        //        result.Message = checkUser.Message;
        //        result.Statue = checkUser.Statue;
        //        return result;

        //    }
        //    if (string.IsNullOrEmpty(checkUser.User.Id))
        //    {
        //        result.Message = "لطفا وارد شوید";
        //        result.Statue = Enums.Statue.Failure;
        //        return result;
        //    }

        //    var gifts = _giftRepository.GetAll(checkUser.User.Id);
        //    return gifts;
        //}
        //----------------------------------------------------------------------
        [HttpPost, Route("api/SentGiftsCount")]
        public ResultShop countsent([FromBody] Request model)
        {
            ResultShop result = new ResultShop();
            if (string.IsNullOrEmpty(model.UserId))
            {
                result.Message = "لطفا وارد شوید";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            var count = _giftRepository.MySentGifts(model.UserId);

            result.Count = count;
            result.Message = "تعداد هدایای ارسالی با موفقیت ارسال شد";
            result.Statue = Enums.Statue.Success;
            return result;
        }

        //----------------------------------------------------------------------

        [HttpPost, Route("api/RecievedGiftsCount")]
        public ResultShop recivedcount([FromBody] Request model)
        {
            ResultShop result = new ResultShop();
            if (string.IsNullOrEmpty(model.UserId))
            {
                result.Message = "لطفا وارد شوید";
                result.Statue = Enums.Statue.Failure;
                return result;
            }
            var count = _giftRepository.MyRecievedGifts(model.UserId);

            result.Count = count;
            result.Message = "تعداد هدایای دریافتی با موفقیت ارسال شد";
            result.Statue = Enums.Statue.Success;
            return result;
        }
        //----------------------------------------------------------------------

        [HttpPost, Route("api/GetUserNames")]
        public ResultUserSearch GetUserNames([FromBody] RequestShop model)
        {
            var result = new ResultUserSearch();
            var checkUser = _userRepository.GetByToken(model.Token);
            if (checkUser.Statue == Enums.Statue.AccessDenied)
            {
                result.Message = checkUser.Message;
                result.Statue = checkUser.Statue;
                return result;

            }
            try
            {
                var usersUserNames = _userRepository.GetAllUsersUserName(model.FilterParam);
                if (usersUserNames.Statue == Enums.Statue.Success)
                {
                    result.Message = usersUserNames.Message;
                    result.Statue = Enums.Statue.Success;
                    result.UserNameAndUserIds = usersUserNames.UserNameAndUserIds;
                    result.ComboBoxItems = usersUserNames.UserNameAndUserIds.Select(p => new ComboBoxResult
                    {
                        StringId = p.UserId,
                        ShowTitle = p.UserFullName + "( " + p.UserName + " )",
                        Description = p.UserAbout,
                        Image = p.UserImage,
                        Title = p.UserFullName
                    }).ToList();
                    return result;
                }
                else
                {
                    result.Message = usersUserNames.Message;
                    result.Statue = Enums.Statue.Success;
                    return result;
                }
            }
            catch (Exception e)
            {

                result.Message = e.Message;
                result.Statue = Enums.Statue.Failure;
                return result;
            }


        }

        //----------------------------------------------------------------------

        //[HttpPost, Route("api/MyShareholders")]
        //public ResultShop MyShareholders([FromBody] Request model)
        //{
        //    ResultShop result = new ResultShop();

        //    try
        //    {

        //        var checkUser = _userRepository.GetByToken(model.Token);
        //        if (checkUser.Statue == Enums.Statue.AccessDenied)
        //        {
        //            result.Message = checkUser.Message;
        //            result.Statue = checkUser.Statue;
        //            return result;
        //        }

        //        var shareholders = _shareholderRepository.GetProductsOfShareholders(checkUser.User.Id);

        //        result.ShareholderList = shareholders?.ShareholderList;
        //        result.Statue = Enums.Statue.Success;
        //        return result;

        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.InnerException.ToString();
        //        return result;
        //    }
        //}



    }

}

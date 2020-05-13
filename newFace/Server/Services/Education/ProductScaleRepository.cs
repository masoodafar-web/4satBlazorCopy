using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;


namespace newFace.Server.Services.Education
{
    public class ProductScaleRepository : IProductScaleRepository
    {
        private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;

        public ProductScaleRepository(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }

        public Result Create(ProductScale ProductScale)
        {
            Result result = new Result();
            if (ProductScale.ProductId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه محصول را ارسال کنید";
                return result;
            }
            if (ProductScale.CatId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه دسته بندی را ارسال کنید";
                return result;
            }

            if (ProductScale.LevelId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه سطح را ارسال کنید";
                return result;
            }


                //var sumcheck = CheckSum(ProductScale.CatId, ProductScale.Percent);
                //if (sumcheck.Statue != Enums.Statue.Success)
                //{
                //    result.Statue = Enums.Statue.Failure;
                //    result.Message = "درصد وارد شده با احتساب مجموع درصدهای این دسته بندی بیش از 100 میباشد.";
                //    return result;
                //}
                //else
                //{
                  return _unitOfWork.ProductScaleGR.Add(ProductScale);
                  
                //}

         
        }

        public Result Edit(ProductScale ProductScale)
        {
            Result result = new Result();

            if (ProductScale.ProductId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه محصول را ارسال کنید";
                return result;
            }
            if (ProductScale.CatId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه دسته بندی را ارسال کنید";
                return result;
            }

            if (ProductScale.LevelId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه سطح را ارسال کنید";
                return result;
            }

           return _unitOfWork.ProductScaleGR.Update(ProductScale);
 }

        public ResultProductScale GetById(int? Id)
        {
            ResultProductScale Result = new ResultProductScale();
   
                if (Id != null)
                {
                    ProductScale ProductScale = _unitOfWork.ProductScaleGR.FirstOrDefault(p => p.Id == Id.Value);
                    if (ProductScale != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.ProductScale = ProductScale;
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

        public Result Delete(int? Id)
        {
            Result result = new Result();
            try
            {
                if (Id != null)
                {
                    ProductScale ProductScale = _unitOfWork.ProductScaleGR.GetById(Id.Value);
                    _unitOfWork.ProductScaleGR.Delete(ProductScale);
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

     
        public ResultProductScale GetAll()
        {
            ResultProductScale result = new ResultProductScale();
           
                List<ProductScale> ProductScaleList = _unitOfWork.ProductScaleGR.GetAll().ToList();
                if (ProductScaleList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ProductScales = ProductScaleList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.ProductScales = ProductScaleList;
                    return result;
                }


         
        }

        public ResultProductScale GetProductByCatIdChild(int catId, List<Category> categories)
        {
            ResultProductScale result = new ResultProductScale();

            var cats = _categoryRepository.FindAllChildList(catId, true).ToList();

            List<ProductSummaryViewModels> ProductList = new List<ProductSummaryViewModels>();

            foreach (var items in cats)
            {
                 var products = _unitOfWork.ProductScaleGR.GetAllIncluding(p => p.CatId == items.Id,
                                                               p => p.Product.Books.Select(b => b.Author),
                                                               p => p.Product.Courses.Select(b => b.Teacher),
                                                               p => p.Product.Exams.Select(b => b.Designer))
                                                                     .Select(p => p.Product)
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
                                                                         Type = product.Type
                                                                     }).ToList();

                 ProductList.AddRange(products);
            }

            var ProductList1 = ProductList.GroupBy(g => g.Id).ToList();
            var ProductList2 = ProductList1.Select(s => s.First()).ToList();
            
           

            if (ProductList.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.Products = ProductList2;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                result.Products = ProductList2;
                return result;
            }

        }

        public ResultProductScale GetProductByCatId(int catId , List<Category> categories)
        {
            ResultProductScale result = new ResultProductScale();
          
                var products = _unitOfWork.ProductScaleGR.GetAllIncluding(p => p.CatId == catId,
                                                                   p => p.Product.Books.Select(b => b.Author),
                                                                   p => p.Product.Courses.Select(b => b.Teacher),
                                                                   p => p.Product.Exams.Select(b => b.Designer))
                                                                         .Select(p => p.Product)
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
                                                                             Type = product.Type
                                                                         }).ToList();

                if (products.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Products = products;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.Products = products;
                    return result;
                }

        }

        //public Result CheckSum(int? CatId, double? Percent)
        //{
        //    Result result = new Result();
        //    if (CatId == null || Percent == null)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = "اطلاعات را بدرستی وارد نمایید";
        //        return result;
        //    }

        //    if (Percent > 100)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        return result;
        //    }
        //    var SumCategory = _productScaleService.FindBy(w => w.CatId == CatId).ToList();
        //    if (SumCategory.Count > 0)
        //    {
        //        result.Statue = SumCategory.Sum(s => s.Percent) + Percent > 100 ? Enums.Statue.Failure : Enums.Statue.Success;
        //        return result;
        //    }

        //    result.Statue = Enums.Statue.Success;
        //    return result;
        //}

        public double GetProductCredit(int productId)
        {

            var productScale = _unitOfWork.ProductScaleGR.FirstOrDefault(p => p.ProductId == productId);
            if (productScale != null)
            {
                return productScale.Credit;
            }
            return 0;
        }
    }
}
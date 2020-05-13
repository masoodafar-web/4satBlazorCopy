using System.Collections.Generic;
using System.Linq;
using System;
using newFace.Shared.Models;
using newFace.Shared.Models.Advice;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;


namespace newFace.Server.Services.Resource
{
    public class SuggestionRepository : ISuggestionRepository
    {

        private readonly ICategoryRepository _categoryRepository;
        private IUnitOfWork _unitOfWork;
        private float chancePercent;

        public SuggestionRepository(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
        }
  
        //محصولات یک مهارت
        public List<ProductScale> SkillProducts(int CategoyId)
        {
            return _unitOfWork.ProductScaleGR.GetAllIncluding(f => f.CatId == CategoyId, i => i.Product).ToList();
        }

        public Result SuggestProductByVisionCatId(int VisionCatId, string UserId)
        {
            var visionSkills = _categoryRepository.FindOneLevelChildList(VisionCatId);
            bool SaveAllProductAsSkills = true;
            foreach (var item in visionSkills)
            {
                //برای بدست آوردن اولویت مهارت نسبت به هدف
                var SkillPriorityTovision = _unitOfWork.Category_CategoryGR
                    .FirstOrDefault(f => f.ChildrenCatId == item.Id && f.ParentCatId == VisionCatId).Priority;
                SaveAllProductAsSkills = InsertToSuggestProduct(SkillProducts(item.Id), UserId,
                    SkillPriorityTovision, item.Id, VisionCatId).Statue == Enums.Statue.Success ? true : false;

            }

            if (SaveAllProductAsSkills)
            {
                return new Result()
                {
                    Statue = Enums.Statue.Success,
                    Message = "پیشنهاد کلی محصولات با موفقیت انجام شد"
                };
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "متاسفانه پیشنهاد کلی محصولات با موفقیت انجام نشد"
                };
            }
        }

        //-----------------------------------------------------------

        public Result InsertToSuggestProduct(List<ProductScale> productScales, string userId, int? SkillPriority, int SkillCatId, int? VisionCatId)
        {
            SuggestedProduct suggestedProduct = new SuggestedProduct();
            List<SuggestedProduct> suggestedProducts = new List<SuggestedProduct>();
            Result Result = new Result();
            var SuggestedProduct = _unitOfWork.SuggestedProductGR.FindBy(f => f.UserId == userId).ToList();
            var ProductSeenInfo = _unitOfWork.ProductSeenInfoGR.FindBy(f => f.UserId == userId).ToList();
            foreach (var productScale in productScales)
            {
                suggestedProduct = new SuggestedProduct()
                {
                    ProductId = productScale.ProductId,
                    UserId = userId,
                    ProductPriorityFV = productScale.Priority,
                    SkillPriorityFV = SkillPriority,
                    SkillCatId = SkillCatId,
                    VisionCatId = VisionCatId,
                };
                //برای اینکه محصولاتی که در محصولات پیشنهادی کاربر قبلا ذخیره شده دوباره اضافه نشود
                if (!SuggestedProduct.Any(f => f.UserId == userId && f.ProductId == productScale.ProductId && f.SkillCatId == SkillCatId && f.VisionCatId == VisionCatId))
                {
                    //محصولاتی که قبلا خرید شده توسط کاربر اضافه نشود
                    if (!ProductSeenInfo.Any(a=>a.ProductId== productScale.ProductId))
                    {
                        suggestedProducts.Add(suggestedProduct);
                    }
                    

                }

            }

            if (suggestedProducts.Count > 0)
            {
                Result = _unitOfWork.SuggestedProductGR.AddRange(suggestedProducts);
            }
            else
            {
                Result = new Result()
                {
                    Statue = Enums.Statue.Success,
                    Message = "تعداد " + suggestedProducts.Count + " رکورد ذخیره شد "

                };
            }
            return Result;
        }

        public Result DeleteVisionSuggestedProduct(int VisionCatId, string UserId)
        {
            var deletingItem = _unitOfWork.SuggestedProductGR.FindBy(f => f.VisionCatId == VisionCatId &&
                f.UserId == UserId).ToList();
            return _unitOfWork.SuggestedProductGR.DeleteRange(deletingItem);
        }


        public Result DeleteProductFromSuggestion(string userid, int? productid)
        {
            Result result = new Result();

            if (string.IsNullOrEmpty(userid))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه کاربر را وارد نمایید";
                return result;
            }

            if (productid == null)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا شناسه محصول را وارد نمایید";
                return result;
            }

            //برای حذف محصولات پیشنهادی زمانیکه خواندن محصول کامل یا محصول خرید میشود میشود
            var suggests = _unitOfWork.SuggestedProductGR.FindBy(f =>
                f.ProductId == productid.Value && f.UserId == userid).ToList();
            _unitOfWork.SuggestedProductGR.DeleteRange(suggests);

            result.Statue = Enums.Statue.Success;
            result.Message = "با موفقیت انجام شد";
            return result;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;


namespace newFace.Shared.Repositories.Resource
{
    public interface ISuggestionRepository
    {
        //به دلیل ارور کامنت گردید ولی زمانی که کار مسعود با مشاوره تمام شده باید از یه همچین متدی استفاده کند(مسعود)

        //ResultSuggestion VisionSuggestion(List<Skill> skill);
        //ResultSuggestion ProductSuggestion(int categoryId, string userId);
        //ResultSuggestion FindVisionFromCategoryId(int CategoryId, float percent);
        List<ProductScale> SkillProducts(int CategoyId);
        Result SuggestProductByVisionCatId(int VisionCatId, string UserId);
        Result InsertToSuggestProduct(List<ProductScale> productScales, string userId, int? SkillPriority, int SkillCatId, int? VisionCatId);
        Result DeleteVisionSuggestedProduct(int VisionCatId, string UserId);

        Result DeleteProductFromSuggestion(string userid, int? productid);
    }
    public class ResultSuggestion : Result
    {
        public float CategoryPercent { get; set; }
        public Category Category { get; set; }
        public List<Category> CategoryList { get; set; }
        public List<Product> ProductList { get; set; }

    }

}

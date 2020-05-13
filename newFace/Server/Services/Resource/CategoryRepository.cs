using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
namespace newFace.Server.Services.Resource
{
    public class CategoryRepository : ICategoryRepository
    {

        private List<Category> categoryList1 = new List<Category>();
        private List<Category> categoryAllChild = new List<Category>();

        private IUnitOfWork _unitOfWork;

        public CategoryRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
      
        public List<Category> FindAllSkillinRel()
        {
            return _unitOfWork.Category_CategoryGR.GetAllIncluding(w => w.Children.CategoryType == CategoryTypeEnum.Skill, i => i.Children).Select(s => s.Children).Distinct().ToList();

        }
        //فرزندان سطح 1 یک  categoryId
        public List<Category> FindOneLevelChildList(int CategoyId)
        {
            return _unitOfWork.Category_CategoryGR.GetAllIncluding(f => f.ParentCatId == CategoyId, i => i.Children).Select(s => s.Children).ToList();
        }
        //تمام زیر مجموعه های یک categoryId
        public List<Category> FindAllChildList(int CategoyId, bool isNew = true)
        {
            if (isNew)
                categoryAllChild.Clear();
            var ChildList = FindOneLevelChildList(CategoyId);

            foreach (var category in ChildList)
            {
                if (category != null)
                    FindAllChildList(category.Id,false);
            }
            categoryAllChild.AddRange(ChildList);
            return categoryAllChild;
        }
        //یافتن پدر یک سطح بالا تر از خودش
        public List<Category> FindOneLevelParentList(int CategoyId)
        {
            return _unitOfWork.Category_CategoryGR.GetAllIncluding(f => f.ChildrenCatId == CategoyId && f.ParentCatId!=null, i => i.Parent).Select(s => s.Parent).ToList();
        }
        //یافتن تمام پدر های تمامی سطوح
        public List<Category> FindAllParentList(int CategoyId,bool isNew=true)
        {
            if (isNew)
                categoryList1.Clear();
            var oneLevelParentList = FindOneLevelParentList(CategoyId);

            foreach (var item in oneLevelParentList)
            {
                if (item != null)
                    FindAllParentList(item.Id,false);

            }
                categoryList1.AddRange(oneLevelParentList);
            return categoryList1.Distinct().ToList();
        }





    }
}
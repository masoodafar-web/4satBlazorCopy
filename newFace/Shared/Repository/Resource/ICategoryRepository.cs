using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace newFace.Shared.Repositories.Resource
{
    public interface ICategoryRepository
    {
      
        List<Category> FindAllSkillinRel();
        List<Category> FindOneLevelChildList(int CategoyId);
        List<Category> FindAllChildList(int CategoyId, bool isNew);
        List<Category> FindOneLevelParentList(int CategoyId);
        List<Category> FindAllParentList(int CategoyId, bool isNew = true);
    }

    public class ResultCategoryList : Result
    {
        public List<Category> CategoryList { get; set; }
    }
}

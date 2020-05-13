using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Education
{
   public interface IFavoriteRepository
    {
       ResultFavorite AddRemove(int? id, string userid, FavedType type, Enums.changetype changeType, string currentUserid);

        ResultFavorite GetAll(FavedType? type, string userid);

        bool IsFaved(int? id, string userid, FavedType type, string currentUserid);

        int GetUserFavedCount(string userid);

    }

}

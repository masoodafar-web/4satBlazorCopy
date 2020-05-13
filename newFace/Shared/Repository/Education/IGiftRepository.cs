using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using Newtonsoft.Json;

namespace newFace.Shared.Repositories.Resource
{
    public interface IGiftRepository
    {
        Result Create(Gift gift);
        Result Edit(Gift gift);
        Result Delete(int? id);
        Result Delete(Gift gift);
        ResultGift GetById(int? id);
        ResultGift GetAll(string userid);

        int MySentGifts(string userid);

        int MyRecievedGifts(string userid);
    }
   
   
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Education
{
    public interface IProductSeenInfoRepository
    {
        Result Create(ProductSeenInfo ProductSeeninfo);

        Result Edit(ProductSeenInfo ProductSeeninfo);

        Result Delete(int? Id);
        ResultProductSeeninfo GetById(int? Id);
        ResultProductSeeninfo GetAll();
        //Result Save(string message);

        bool IsReadBefore(int productId, string userId);

        double CompleteReadProduct(int productId, string userId, Enums.StatusTypeQuestion? StatusTypeQuestion);

        bool CompleteVideo(int videoId, string userId);

    }
 
}


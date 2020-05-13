using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Resource
{
    public interface IPointRepository
    {
        Result PointLog(Point model);
        Result AddPoint(string UserId, long Point);
    }
}

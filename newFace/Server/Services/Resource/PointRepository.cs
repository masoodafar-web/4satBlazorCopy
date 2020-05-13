using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repositories.Resource;
using Microsoft.AspNetCore.Identity;

namespace newFace.Server.Services.Resource
{

    public class PointRepository : IPointRepository
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;
        public PointRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Result PointLog(Point model)
        {
            return _unitOfWork.PointGR.Add(model);
        }
        public Result AddPoint(string UserId,long Point)
        {
           
                var user=UserManager.FindByIdAsync(UserId).Result;
                user.Point += Point;
                var result=UserManager.UpdateAsync(user).Result;
                return new Result { Message = result.Errors.FirstOrDefault().Description, Statue = (Enums.Statue)result.Succeeded.GetHashCode() }; ;

           
        }
    }
}
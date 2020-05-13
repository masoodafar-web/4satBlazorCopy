using System.Collections.Generic;
using System.Linq;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Server.Services.Resource;
using newFace.Shared.Repositories.Resource;
using newFace.Shared.Repositories.Shop;
using Microsoft.EntityFrameworkCore;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Services.Shop
{
    public class ShopRepository : IShopRepository
    {
        private readonly IGiftRepository _giftRepository;
        private IUnitOfWork _unitOfWork;

        public ShopRepository(IGiftRepository giftRepository, IUnitOfWork unitOfWork)
        {
            _giftRepository = giftRepository;
            _unitOfWork = unitOfWork;
        }
        //public ResultShop GetUserProductsCount(string userid, ProductType? productType, BuyType BuyType)
        //{
        //    ResultShop result = new ResultShop();

        //    var factorforsale = _unitOfWork.FactorforsaleProductGR.Count(w => w.Bill.Status == 1 && w.UserId == userid && (productType == null ? w.Id != 0 : w.Products.Type == productType) && w.BuyType == BuyType);

        //    result.Count = factorforsale;
        //    result.Statue = Enums.Statue.Success;
        //    return result;
        //}
        public ResultShop GetProducts(string userid, ProductType? productType , BuyType BuyType)
        {
            ResultShop result = new ResultShop();

            var factorforsale =_unitOfWork.FactorforsaleProductGR.GetAllIncluding(w => w.Bill.Status == 1 &&  w.UserId == userid && (productType == null ? w.Id != 0 : w.Products.Type == productType) && w.BuyType == BuyType, i => i.Products).Select(s => s.Products);            
            var products = factorforsale
                .Include(p => p.Books)
                .Include(p => p.Books).ThenInclude(c => c.Author)
                .Include(p => p.Books).ThenInclude(c => c.Publishers)
                .Include(p => p.Books).ThenInclude(c => c.Speakers)
                .Include(p => p.Books).ThenInclude(c => c.Translators)
                .Include(p => p.Courses)
                .Include(p => p.Courses).ThenInclude(c => c.Teacher)
                .Include(p => p.Exams)
                .Include(p => p.Exams).ThenInclude(s => s.Designer)
                .Include(p => p.Exams).ThenInclude(s => s.Questions)
                .ToList();

            result.Products.AddRange(products);
            result.Statue = Enums.Statue.Success;
            return result;
        }

        


    }
}
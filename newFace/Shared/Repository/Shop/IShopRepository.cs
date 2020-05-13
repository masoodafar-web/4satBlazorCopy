using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Repositories.Shop
{
   public interface IShopRepository
    {
         ResultShop GetProducts(string userid, ProductType? producttype, BuyType BuyType);
         //ResultShop GetUserProductsCount(string userid, ProductType? productType, BuyType BuyType);


    }



}

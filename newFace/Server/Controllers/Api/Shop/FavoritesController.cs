using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using newFace.Server.Utility;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Resource;
using newFace.Utility;

namespace newFace.Controllers.Api.Shop
{
    [ApiController]
    public class FavoritesController :Controller
    {
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;


        public FavoritesController(IFavoriteRepository favoriteRepository, IUserRepository userRepository)
        {
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
        }


        [HttpPost, Route("api/FavoritesLists")]
        public ResultFavorite List([FromBody]Request model)
        {
            if (!String.IsNullOrEmpty(model.Token))
            {
                model.UserId = _userRepository.GetByToken(model.Token).User.Id;
                var result = _favoriteRepository.GetAll(model.FavedType, model.UserId);
                if (model.FavedType == FavedType.Product)
                {
                    var q = result.Products.Select(s => new
                    {
                        //SuggestedProduct = s,
                        product = new
                        {
                            Type = s.Type,
                            Id = s.Id,
                            Img = s.Img,
                            Title = s.Title,
                            Credit = s.ProductScale.FirstOrDefault().Credit,
                            Description = s.Description,
                            LevelName = s.ProductScale.FirstOrDefault().Levels.Name,
                            Price = s.Price,
                            PriceWithDiscount = s.PriceWithDiscount,
                            IsFavorite = s.IsFavorite
                        },
                        book = s.Books.Select(se => new { AuthorFullName = se.Author.FullName, PublishersFullName = se.Publishers.FullName }).FirstOrDefault(),
                        course = s.Courses.Select(se => se.Teacher.FullName).FirstOrDefault(),
                        exam = s.Exams.Select(se => se.Designer.FullName).FirstOrDefault(),
                        SKills = s.ProductScale.Select(select => select.Category.Title).ToList()

                    }).DistinctBy(d => d.product.Id).ToList();

                    return new ResultFavorite()
                    {
                        Statue = Enums.Statue.Success,
                        Message = "موارد به درستی ارسال شد",
                        AnyObjectForfavarite = q
                    };
                }
                else
                {
                    return result;
                }
            }
            else
            {
                return new ResultFavorite()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "شناسه کاربر اشتباه است"
                };
            }

        }


        [HttpPost, Route("api/FavoriteAddRemove")]
        public Result addremove([FromBody] Request model)
        {
            if (!String.IsNullOrEmpty(model.Token))
            {
                model.UserId = _userRepository.GetByToken(model.Token).User.Id;
                var result = _favoriteRepository.AddRemove(model.Id, model.ReciverUserId, model.FavedType.Value, model.changeType,
                    model.UserId);
                return result;
            }
            else
            {
                return new Result()
                {
                    Statue = Enums.Statue.Failure,
                    Message = "شناسه کاربر اشتباه است"
                };
            }
        }
    }
}

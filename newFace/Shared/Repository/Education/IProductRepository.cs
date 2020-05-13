using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Repositories.Education
{
    public interface IProductRepository
    {
        Result CreateBook(ProductViewModels ProductViewModels, IFormFile FileText, IFormFile FileAudio);
        Result CreateCourse(ProductViewModels ProductViewModels, IFormFile sample, List<VideoFileViewModel> Videoes);
        Result CreateExam(ProductViewModels productViewModels);
        Result CreateQuestion(QuestionAnswerViewModel questionAnswerViewModel, int? correctanswer);
        ResultShop GetProductsSummary(int? pageNumber, int? returnCount, Enums.ProductSearchType? productSearchType, ProductType? productType, int? categoryId, int? producterId,string UserId,BuyType? buyType,GiftType? giftType, string textSearch);
        Task<Result> EditBook(ProductViewModels ProductViewModels, IFormFile FileText, IFormFile FileAudio);
        Task<Result> EditCourse(ProductViewModels ProductViewModel, IFormFile Sample, List<VideoFileViewModel> Videos);
        Task<Result> EditExam(ProductViewModels productViewModels);
        Result Delete(int? Id);
        Task<ResultProduct> GetById(int? Id, string loginUserId);

        ResultProduct GetByName(string name);

        ResultProduct GetByCatId(int? id);
        ResultProduct GetAll(ProductType? producttype);
    }
   

}

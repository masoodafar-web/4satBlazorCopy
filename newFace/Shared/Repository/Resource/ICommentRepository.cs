using System.Collections.Generic;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Resource
{
    public interface ICommentRepository
    {
        Result Create(Comment post);
        Result Edit(Comment post);
        ResultComment Delete(int? id);
        //Result Delete(Comment post);
        ResultComment GetById(int? id);
        ResultComment GetAll();
        ResultComment GetCommnetCount(int? postId, int? productId);
        ResultComment GetProductCommnet(int productId);
        ResultComment GetCommnetsLazyLoad(int? productId, int? postId,int? blogId, int pageNumber = 0);

        Result CalculateProductRate(int productId, int rank);


    }
   

}

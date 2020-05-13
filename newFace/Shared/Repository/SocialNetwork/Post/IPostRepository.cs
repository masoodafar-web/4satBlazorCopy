using System.Collections.Generic;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;

namespace newFace.Shared.Repositories.Resource
{
    public interface IPostRepository
    {
        ResultPost GetAll();
        ResultPost GetAllByType(PostType postType);
        ResultPost GetAll(int pageNumber, int returnPostCount);
        ResultPost GetAllByType(PostType postType, int pageNumber, int returnPostCount, string userId, bool? Like, bool? CDate, bool? Rate, bool? Seen);
        ResultPost GetById(int? id, string loginUserId);

        ResultPost GetByIdForEdit(int? id);
        Result Create(Post post);
        Result Edit(Post post);
        Result Delete(int? id);
        Result RemovePostFiles(int? id);
        Result Delete(Post post);
        LikeResultViewModel LikeDislike(Like model);
        Result Seen(Seen model);

        ResultPost SearchAll(string userId, int? categoryId, int? levelId, int? asdType, int? favoriteRank, PostType postType, int pageNumber, int returnPostCount, string LoginUserId, bool? Like, bool? CDate, bool? Rate, bool? Seen, bool? Favorite = null);
        Result SeenPostList(List<Post> Posts, string userId);
        Result SeenPost(Post Post, string userId);
        Result SeenPostList(List<int> Posts, string userId);
        Result SeenPost(int Post, string userId);
        ResultPost PostChangeRequestChecker(int? postId, int? levelId, bool isExist, int? categoryId, string UserId);
    }
 
}

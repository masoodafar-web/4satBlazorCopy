using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Education;

namespace newFace.Shared.Models.ViewModels
{
    public class LayoutMainViewModel
    {
        public ApplicationUser User { get; set; }
        public Post Post { get; set; }
        public Comment Comment { get; set; }

        public List<Post> PostList { get; set; }

        public Blog Blog { get; set; }

        public List<Blog> Blogs { get; set; }=new List<Blog>();

        public Product Product { get; set; }
        public ProductVm ProductVm { get; set; }

        FavoriteVm _fvm = new FavoriteVm();

        public FavoriteVm FavoriteVm { get { return _fvm; } set { value= _fvm; } }

        List<FavoriteVm> _fvms =new List<FavoriteVm>();

        public List<FavoriteVm> ListFavorites { get { return _fvms; } set { value = _fvms; } }
        public List<Favorite> Favorites = new List<Favorite>();

        public List<Product> Products { get; set; }

        public List<ProductSummaryViewModels> RelatedProducts { get; set; }

        public IEnumerable<ChatContactViewModels> ChatContactViewModels { get; set; }


    }
}
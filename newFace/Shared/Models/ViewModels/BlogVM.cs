using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Resource.BlogContent;

namespace newFace.Shared.Models.ViewModels
{
    public class BlogVM
    {
        public int Id { get; set; }

        public Blog Blog { get; set; }

        public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();

        public List<BlogRelation> Files { get; set; }=new List<BlogRelation>();

        public List<BlogRelation> Tags { get; set; }=new List<BlogRelation>();

        public List<BlogRelation> KeyWords { get; set; }=new List<BlogRelation>();

        public List<Comment> Comments { get; set; }=new List<Comment>();

    }
}
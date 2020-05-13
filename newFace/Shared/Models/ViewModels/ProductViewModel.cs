using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class ProductViewModel
    {
        public Comment Comment { get; set; }
        public List<Comment> Comments { get; set; }
        public Product Product { get; set; }
        public List<Product> Products { get; set; }
    }
    public class ProductVm
    {
        public Product Product { get; set; }
        public Book Book { get; set; }
        public Exam Exam { get; set; }
        public Course Course { get; set; }
        public int Star1 { get; set; }
        public int Star2 { get; set; }
        public int Star3 { get; set; }
        public int Star4 { get; set; }
        public int Star5 { get; set; }

        public double PercentStar1 { get; set; }
        public double PercentStar2 { get; set; }
        public double PercentStar3 { get; set; }
        public double PercentStar4 { get; set; }
        public double PercentStar5 { get; set; }
        public int RateCount { get; set; } = 0;
        public bool IsBuyed { get; set; }

        //اینو فقط بخواطر آرش گذاشتم اگه میخوای فوش بدی واسه شلوغیه مدل به آرش فوش بده از طرف مسعود:-)
        public List<ProductSummaryViewModels> RelatedProducts { get; set; } = new List<ProductSummaryViewModels>();

    }
}
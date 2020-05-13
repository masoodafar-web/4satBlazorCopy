using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using static newFace.Shared.Models.Product;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Models.ViewModels
{
    public class ProductViewModels
    {
        public int Id { get; set; }
        public Product Product { get; set; }

        public Book Book { get; set; }

        public Course Course { get; set; }

        public List<VideoFileViewModel> Videos { get; set; } = new List<VideoFileViewModel>();

        public List<Video> Edit_Course_Videos { get; set; } = new List<Video>();

        public Exam Exam { get; set; }

        public List<QuestionAnswerViewModel> QuestionAnswerViewModelList { get; set; } = new List<QuestionAnswerViewModel>();

        public ProductScale ProductScale { get; set; }

        public List<ProductScale> ProductScales { get; set; } = new List<ProductScale>();


        public List<int> Priority { get; set; }
        public List<int> CatId { get; set; }
        public List<int> LevelId { get; set; }
        public List<double> Percent { get; set; }
        public List<double> Credit { get; set; }
    }


    public class ProductSummaryViewModels
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Designer { get; set; }

        public string Teacher { get; set; }

        public string Img { get; set; }

        public double Price { get; set; }
        public double ShareHolderPercent { get; set; }
        public double ShareHolderUnit { get; set; }
        public double? PriceWithDiscount { get; set; }

        public ProductType Type { get; set; }

        public string TypeDisplayName { get; set; }
        
   

        //-------------------------------------------------Detail product
        //public string Description { get; set; }

        //public int LanguageId { get; set; }
        //public int LanguageDisplayName { get; set; }
        ////-------------------------------------------------Book
        public string Author { get; set; }
        //public int? AuthorId { get; set; }

        public string Translator { get; set; }
        //public int? TranslatorId { get; set; }

        public string Publisher { get; set; }
        //public int? PublisherId { get; set; }

        public string Speaker { get; set; }
        //public int? SpeakerId { get; set; }

        //public int? PageCount { get; set; }

        //public string Barcode { get; set; }

        //public string FileAudio { get; set; }

        //public string FileText { get; set; }

        public float Rate { get; set; }
        public string UserSend { get; set; }
        public string UserRecive { get; set; }




    }

}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Models.ViewModels.CategoryViewModel;
using newFace.Shared.Repositories.Education;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Models.Resource
{
    public class ComboBoxResult
    {
        public int Id { get; set; }
        public string StringId { get; set; }
        public string Title { get; set; }
        public string ShowTitle { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

    }
    public class Request
    {
        public int? Id { get; set; }
        public string Token { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }

        public double Price { get; set; }

        public string ReciverUserId { get; set; }

        public FavedType? FavedType { get; set; }
        public Enums.changetype changeType { get; set; }
        public Enums.ReturnFrom ReturnFrom { get; set; }
        public ProductType? productType { get; set; }
    }
    public class Result
    {
        public Enums.Statue Statue { get; set; } = Enums.Statue.Failure;
        public string Message { get; set; } = "";

        public List<string> Messages { get; set; } = new List<string>();
    }
    public class ResultPayResult : Result
    {
        public Bill Bill { get; set; }
    }
    public class RequestPayResult : Request
    {
        public string Status { get; set; }
        public string Authority { get; set; }
        public string FactorId { get; set; }
        public string TotalPrice { get; set; }
    }
    public class RequestCart:Request
    {
        public CartType CartType { get; set; }

        public string ReciverUserId { get; set; }

        public int? ShareholderPercent { get; set; }
    }
    public class ResultObject : Result
    {
        public object AnyObject { get; set; }
    }
    public class JsonResulte : Result
    {
        public int intValue { get; set; }
    }
    public class BlogResult : Result
    {
        //public Blog Blog { get; set; }

        //public List<BlogCategory> BlogCategories { get; set; } = new List<BlogCategory>();

        //public List<Comment> Comments { get; set; }=new List<Comment>();

        //public List<BlogRelation> BlogTags { get; set; }=new List<BlogRelation>();

        //public List<BlogRelation> BlogKeyWords { get; set; }=new List<BlogRelation>();

        //public List<BlogRelation> BlogMetaTags { get; set; }=new List<BlogRelation>();

        //public List<BlogRelation> BlogFiles { get; set; }=new List<BlogRelation>();


        //public List<Blog> Blogs { get; set; }=new List<Blog>();

        public BlogVM BlogDetail { get; set; }

        public List<BlogVM> BlogList { get; set; } = new List<BlogVM>();

    }
    public class RequestShop : Request
    {
        public ProductType? productType { get; set; }
        public Enums.ProductSearchType? productSearchType { get; set; }
        public BuyType? buyType { get; set; }
        public GiftType? giftType { get; set; }
        public int PageNumber { get; set; } = 0;
        public int PageCount { get; set; } = 10;
        public string Search { get; set; }
        public int? CatId { get; set; }
        public string Name { get; set; }
        public string FilterParam { get; set; }
        public int? ProducterId { get; set; }
    }

    public class ResultShop : Result
    {
        public List<Product> Products = new List<Product>();

        public List<ShopHomeSlider> Sliders { get; set; } = new List<ShopHomeSlider>();

        public List<ProductSummaryViewModels> SuggestionProducts { get; set; } = new List<ProductSummaryViewModels>();

        public List<ProductSummaryViewModels> NewProducts { get; set; } = new List<ProductSummaryViewModels>();

        public List<ProductSummaryViewModels> BestSellerProducts { get; set; } = new List<ProductSummaryViewModels>();

        public List<ProductSummaryViewModels> ProductsSummary { get; set; } = new List<ProductSummaryViewModels>();

        public List<ProductSummaryViewModels> RelatedProducts { get; set; } = new List<ProductSummaryViewModels>();

        public List<Shareholder> ShareholderList { get; set; } = new List<Shareholder>();

        public Product Product { get; set; }
        public ProductVm ProductVm { get; set; }

        public int? CategoryId { get; set; }

        public int Count { get; set; }

    }

    public class ResultUserSearch : Result
    {
        public List<UserNameAndUserId> UserNameAndUserIds { get; set; } = new List<UserNameAndUserId>();
        public List<ComboBoxResult> ComboBoxItems { get; set; } = new List<ComboBoxResult>();
    }

    public class ResultProducter : Result
    {
        public Producter Producter { get; set; }

        public List<ProductSummaryViewModels> Books { get; set; } = new List<ProductSummaryViewModels>();
        public List<ProductSummaryViewModels> Exams { get; set; } = new List<ProductSummaryViewModels>();
        public List<ProductSummaryViewModels> Courses { get; set; } = new List<ProductSummaryViewModels>();

        public List<Producter> Producters = new List<Producter>();
        public List<ProducterViewModel> ProducterViewModels = new List<ProducterViewModel>();
        public ProducterViewModel ProducterViewModel = new ProducterViewModel();
    }
    public class ResultJobResumeVM : Result
    {
        public JobResumeViewModel JobResumeViewModel { get; set; }
        public List<JobResumeViewModel> JobResumeViewModels { get; set; }
    }
    public class RequestJobResume : Request
    {
        public JobResumeViewModel JobResumeViewModel { get; set; }
        public List<JobResumeViewModel> JobResumeViewModels { get; set; }
    }
    public class ResultWorkSample : Result
    {
        public WorkSampleViewModel WorkSampleViewModel { get; set; }
        public List<WorkSampleViewModel> WorkSampleViewModels { get; set; }
    }
    public class RequestWorkSample : Request
    {
        public WorkSampleViewModel WorkSampleViewModel { get; set; }
        public List<WorkSampleViewModel> WorkSampleViewModels { get; set; }
    }
    public class ResultVision : Result
    {
        public Vision Vision { get; set; }
        public List<Vision> VisionList { get; set; }

    }
    public class ResultPercent : Result
    {
        public float NewPercent { get; set; }
        public Enums.Statue UpdateStatuse { get; set; }
    }
    public class ResultJobResume : Result
    {
        public JobResume JobResume { get; set; }
    }
    public class ResultJobResumeList : Result
    {
        public List<JobResume> JobResumeList { get; set; }
    }
    public class UserNameResult : Result
    {
        public List<UserNameAndUserId> UserNameAndUserIds { get; set; }
    }
    public class ResultUser : Result
    {
        public ApplicationUser User { get; set; }

        public ProfileEditViewModel UserVM { get; set; }
        public List<ApplicationUser> UserList { get; set; }

        public List<EducationalRecord> EducationalRecords { get; set; }

        public List<WorkSample> WorkSamples { get; set; }

        public List<JobResume> JobResumes { get; set; }

        public List<Skill> Skills { get; set; }
        public ProfileEditViewModel ProfileEditViewModel { get; set; }
        public SocialNetworkViewModel SocialNetworkViewModel { get; set; }
        //برای قسمت ریست پسوورد اپ که بعد از مرحله تایید کد تایید برای ریست پسوورد فرستاده میشه
        public string CodeToken { get; set; }

    }
    public class ResultBook : Result
    {
        public Book Book { get; set; }

        public List<Book> Books { get; set; }
    }
    public class ResultCart : Result
    {
        public Cart Cart { get; set; }

        public List<Cart> Carts { get; set; }

        public CartViewModel CartViewModel { get; set; }

        public Bill Bill { get; set; }

        public Wallet Wallet { get; set; }

    }
    public class RequestExam : Request
    {
        List<UserAnswerVM> _u = new List<UserAnswerVM>();
        public List<UserAnswerVM> UserAnswerVms { get { return _u; } set { value = _u; } }

        public int? ExamId { get; set; }

        public Enums.StatusTypeQuestion? StatusTypeQuestion { get; set; }
    }

    public class ResultExam : Result
    {
        public Exam Exam { get; set; }

        public List<Exam> Exams { get; set; }

        public ExamResult ExamResult { get; set; }

        List<ExamResultVM> _e = new List<ExamResultVM>();

        public List<ExamResultVM> ExamResultVMtList
        {
            get { return _e; }
            set { value = _e; }
        }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
    public class ResultFavorite : Result
    {
        public List<Favorite> Favorites = new List<Favorite>();

        public List<Product> Products = new List<Product>();

        public List<Post> Posts = new List<Post>();

        public List<ProfileEditViewModel> Users = new List<ProfileEditViewModel>();

        public Enums.changetype Changetype { get; set; }
        public object AnyObjectForfavarite { get; set; }


        public int CountUserFaved { get; set; }

        public int TotalFavedCount { get; set; }

    }
    public class ResultGift : Result
    {
        public Gift Gift { get; set; }
        [JsonIgnore]
        public List<Gift> GiftList { get; set; }
        public List<GiftViewModel> GiftViewModel { get; set; }
    }
    public class ResultProduct : Result
    {
        public ResultProduct()
        {
            ProductsSummary = new List<ProductSummaryViewModels>();
        }
        public Product Product { get; set; }
        public ProductVm ProductVm { get; set; }

        List<Product> _p = new List<Product>();

        public List<Product> ProductList
        {
            get { return _p; }
            set { value = _p; }
        }
        public IQueryable<Product> Products { get; set; }

        public List<ProductSummaryViewModels> ProductsSummary { get; set; }

        public ProductType? ProductType { get; set; }
    }

    public class RequestProduct : Request
    {
        public int? VideoId { get; set; }

        public int? ProductId { get; set; }

    }
    public class ResultProductScale : Result
    {
        public ProductScale ProductScale { get; set; }

        public List<ProductScale> ProductScales { get; set; } = new List<ProductScale>();

        public List<ProductSummaryViewModels> Products { get; set; } = new List<ProductSummaryViewModels>();
    }

    public class ResultProductSeeninfo : Result
    {
        public ProductSeenInfo ProductSeeninfo { get; set; }

        public List<ProductSeenInfo> ProductSeeninfoes { get; set; }
    }
    public class ResultQuestion : Result
    {
        public Question Question { get; set; }

        public List<Question> Questions { get; set; }


    }

    public class ResultQuestionAnswerList : Result
    {
        public ExamViewModel QuestionAnswerList { get; set; }
    }
    public class ResultShareholder : Result
    {
        public Shareholder Shareholder { get; set; }
        public List<Shareholder> ShareholderList { get; set; }
        public int Count { get; set; }

    }
    public class ResultCommission : Result
    {
        public Commission Commission { get; set; }
        public List<Commission> CommissionList { get; set; }

    }
    public class ResultComment : Result
    {
        public int CommentCount { get; set; }

        public Comment Comment { get; set; }

        public List<Comment> CommentList { get; set; } = new List<Comment>();

        public int? CommentId { get; set; }

        public int? PostId { get; set; }

    }
    public class ResultFile : Result
    {
        public string FilePath { get; set; }
        public List<string> ResizeFilePaths { get; set; }

        public string FileType { get; set; }

        public string FileName { get; set; }

        public int FileSize { get; set; }

        public string VideoThumbnail { get; set; }
    }
    public class ResultPayment : Result
    {
        public string UrlReturn { get; set; }

        public string RefrenceCode { get; set; }
        public BillType? PaymentType { get; set; }

    }
  
    public class ResultTransaction : Result
    {
        public Bill Bill { get; set; }

        public List<Bill> Bills { get; set; }

        List<BillViewModel> _b = new List<BillViewModel>();
        public List<BillViewModel> BillViewModels { get { return _b; } set { value = _b; } }
    }
    public class ResultWallet : Result
    {
        public List<Wallet> Wallets { get; set; }
    }
    public class ResultPost : Result
    {
        public Post Post { get; set; }
      public List<PostViewModel> PostList { get; set; } = new List<PostViewModel>();

        public LikeResultViewModel LikeResultViewModel { get; set; }

    }
    public class ResultChat : Result
    {
        public Chat Chat { get; set; }
        public List<Chat> ChatList { get; set; } = new List<Chat>();
        public IQueryable<Chat> ChatIQueryableList { get; set; }

        public List<ChatContactViewModels> ChatContactList { get; set; } = new List<ChatContactViewModels>();

        public int UnSeenCount { get; set; }

        public bool IsSearch { get; set; }
    }
    public class IdentityMessage
    {
        /// <summary>Destination, i.e. To email, or SMS phone number</summary>
        public virtual string Destination { get; set; }

        /// <summary>Subject</summary>
        public virtual string Subject { get; set; }

        /// <summary>Message contents</summary>
        public virtual string Body { get; set; }
    }

    #region Api
    public class ProductsRequest : Request
    {
        public BuyType BuyType { get; set; }
    }
    public class RequestComment : Request
    {
        public int? DeleteId { get; set; }

        public Comment Comment { get; set; }

        public int? PageNumber { get; set; }

        public int? ProductId { get; set; }

        public int? PostId { get; set; }
        public int? BlogId { get; set; }
        


    }
    public class RequestHi : Request
    {
        public bool IsWebRequest { get; set; } = false;
        public int Version { get; set; }
        public bool IsSeenUpdateMsg { get; set; } = false;
    }

    #region AutoComplate

    public class ReqAutoComplate
    {
        public string text { get; set; }
        public string type { get; set; }
    }
    public class ResultAutoComplate : Result
    {
        public List<Resources> Resourceses { get; set; }

    }
    public class ReqResources
    {
        public string type { get; set; }
        public string Name { get; set; }

    }
    #endregion


    #region FieldAndOrientation

    public class ReqGetField
    {
        public string text { get; set; }
        public string parentId { get; set; }
    }
    public class ResultGetField : Result
    {
        public List<FieldAndOrientationVM> FieldAndOrientations { get; set; }
        public FieldAndOrientation FieldAndOrientation { get; set; }

    }
    public class ResultResources : Result
    {
        public List<Resources> Resourceses { get; set; }
        public Resources Resources { get; set; }

    }

    #endregion

    #region CategoryTypeEnum

    public class RequestCategoryTypeEnum
    {
        public CategoryTypeEnum? CategoryTypeEnum { get; set; }
    }
    #endregion

    #region GeneralFilterReq&Res

    public class GeneralFilterReq : Request
    {
        public string FilterText { get; set; }
        public int? Id { get; set; }
    }
    public class GeneralItemRes : Result
    {
        public List<SelectListItem> SelectListItem { get; set; }
    }

    #endregion
    public class ResultCat_CatViewModel : Result
    {
        public List<Category_CategoryViewModel> Categories { get; set; } = new List<Category_CategoryViewModel>();

    }
    public class ResultHi : Result
    {

        public ProfileEditViewModel User { get; set; }

        public List<Category_CategoryViewModel> Categories { get; set; } = new List<Category_CategoryViewModel>();

        public List<Country> Countries { get; set; } = new List<Country>();

        public List<Province> Provinces { get; set; } = new List<Province>();

        public List<City> Cities { get; set; } = new List<City>();

        public List<Resources> Resources { get; set; } = new List<Resources>();

        public List<Level> Levels { get; set; } = new List<Level>();
        public object ProfilDropdownEnums { get; set; } = new object();
        public int? Cartcount { get; set; }
        public string UserPoint { get; set; }
        public string UserCredit { get; set; }
        public int? unSeenCount { get; set; }
        public int? BillCount { get; set; }
        public int? ProductsCount { get; set; }
        public int? ShareholderListCount { get; set; }
        public int? CountFaved { get; set; }
        public int? MySentGifts { get; set; }
        public int? MyRecievedGifts { get; set; }
        public int? AdviceVisionCount { get; set; }
        public int? UserInfoComplatePercent { get; set; }
        public int? LoginUserGenId { get; set; }
        public long WalletCredit { get; set; }
        public AppUpdate AppUpdate { get; set; }



    }


    #endregion
}

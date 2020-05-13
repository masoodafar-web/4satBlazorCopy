using System;
using System.Threading;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Advice;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.General;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.User;

namespace newFace.Shared.Repositories.Generic
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Post> PostGR { get; }
        IGenericRepository<Skill> SkillGR { get; }
        IGenericRepository<UserGeneology> UserGeneologyGR { get; }
        IGenericRepository<UserSetting> UserSettingGR { get; }
        IGenericRepository<ProductScale> ProductScaleGR { get; }
        IGenericRepository<ProductSeenInfo> ProductSeenInfoGR { get; }
        IGenericRepository<UserCategory> UserCategoryGR { get; }
        IGenericRepository<Category_Category> Category_CategoryGR { get; }
        IGenericRepository<Category> CategoryGR { get; }
        IGenericRepository<SuggestedProduct> SuggestedProductGR { get; }
        IGenericRepository<FinancialAdvice> FinancialAdviceGR { get; }
        IGenericRepository<City> CityGR { get; }
        IGenericRepository<Province> ProvinceGR { get; }
        IGenericRepository<ProjectSetting> ProjectSettingGR { get; }
        IGenericRepository<Vision> VisionGR { get; }
        IGenericRepository<Book> BookGR { get; }
        IGenericRepository<Exam> ExamGR { get; }
        IGenericRepository<Course> CourseGR { get; }
        IGenericRepository<Product> ProductGR { get; }
        IGenericRepository<FactorforsaleProduct> FactorforsaleProductGR { get; }
        IGenericRepository<Cart> CartGR { get; }
        IGenericRepository<Shareholder> ShareholderGR { get; }
        IGenericRepository<Level> LevelGR { get; }
        IGenericRepository<VideoSeenInfo> VideoSeenInfoGR { get; }
        IGenericRepository<Wallet> WalletGR { get; }
        IGenericRepository<Bill> BillGR { get; }
        IGenericRepository<Gift> GiftGR { get; }
        IGenericRepository<Question> QuestionGR { get; }
        IGenericRepository<Answer> AnswerGR { get; }
        IGenericRepository<ExamResult> ExamResultGR { get; }
        IGenericRepository<UserAnswer> UserAnswerGR { get; }
        IGenericRepository<Favorite> FavoriteGR { get; }
        IGenericRepository<Producter> ProducterGR { get; }
        IGenericRepository<ProducterType> ProducterTypeGR { get; }
        IGenericRepository<Commission> CommissionGR { get; }
        IGenericRepository<GeneologyType> GeneologyTypeGR { get; }
        IGenericRepository<GeneologyPlan> GeneologyPlanGR { get; }
        IGenericRepository<PlanUni> PlanUniGR { get; }
        IGenericRepository<PlanUniLevel> PlanUniLevelGR { get; }
        IGenericRepository<CommissionHistory> CommissionHistoryGR { get; }
        IGenericRepository<PlanDelta> PlanDeltaGR { get; }
        IGenericRepository<PlanBinary> PlanBinaryGR { get; }
        IGenericRepository<SellPerMonth> SellPerMonthGR { get; }
        IGenericRepository<CommissionPerMonth> CommissionPerMonthGR { get; }
        IGenericRepository<PlanBreakAWay> PlanBreakAWayGR { get; }
        IGenericRepository<PlanBreakAWayLevel> PlanBreakAWayLevelGR { get; }
        IGenericRepository<DividendAmountHistory> DividendAmountHistoryGR { get; }
        IGenericRepository<Comment> CommentGR { get; }
        IGenericRepository<Blog> BlogGR { get; }
        IGenericRepository<Point> PointGR { get; }
        IGenericRepository<ShopHomeSlider> ShopHomeSliderGR { get; }
        IGenericRepository<Seen> SeenGR { get; }
        IGenericRepository<Like> LikeGR { get; }
        IGenericRepository<PostChangeRequest> PostChangeRequestGR { get; }
        IGenericRepository<Chat> ChatGR { get; }
        IGenericRepository<ChatContact> ChatContactGR { get; }
        IGenericRepository<Video> VideoGR { get; }
        IGenericRepository<UserPushToken> UserPushTokenGR { get; }
        IGenericRepository<Notifi> NotifiGR { get; }
        IGenericRepository<NotifiLog> NotifiLogGR { get; }
        IGenericRepository<JobResume> JobResumeGR { get; }
        IGenericRepository<EducationalRecord> EducationalRecordGR { get; }
        IGenericRepository<WorkSample> WorkSampleGR { get; }
        IGenericRepository<Country> CountryGR { get; }
        IGenericRepository<Resources> ResourcesGR { get; }
        IGenericRepository<FieldAndOrientation> FieldAndOrientationGR { get; }
        IGenericRepository<AppUpdate> AppUpdateGR { get; }


        Result SaveChanges();
        Task<Result> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void Dispose(bool disposing);

    }
}
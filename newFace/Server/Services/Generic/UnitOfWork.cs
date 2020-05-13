using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using newFace.Server.Data;
using newFace.Shared.Models;
using newFace.Shared.Models.Advice;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.General;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.User;
using newFace.Shared.Repositories.Generic;
using Point = newFace.Shared.Models.Point;

namespace newFace.Server.Services.Generic
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly newFaceDbContext _context;
        private bool _disposed;

        public UnitOfWork(newFaceDbContext context)
        {
            _context = context;
        }

        #region Initializ

        private IGenericRepository<Post> _postGR;
        private IGenericRepository<Skill> _skillGR;
        private IGenericRepository<UserGeneology> _userGeneologytGR;
        private IGenericRepository<UserSetting> _userSettingGR;
        private IGenericRepository<ProductScale> _productScaleGR;
        private IGenericRepository<ProductSeenInfo> _productSeenInfoGR;
        private IGenericRepository<UserCategory> _userCategoryGR;
        private IGenericRepository<Category_Category> _category_CategoryGR;
        private IGenericRepository<Category> _categoryGR;
        private IGenericRepository<SuggestedProduct> _suggestedProductGR;
        private IGenericRepository<FinancialAdvice> _financialAdviceGR;
        private IGenericRepository<City> _cityGR;
        private IGenericRepository<Province> _provinceGR;
        private IGenericRepository<ProjectSetting> _projectSettingGR;
        private IGenericRepository<Vision> _visionGR;
        private IGenericRepository<Book> _bookGR;
        private IGenericRepository<Exam> _examGR;
        private IGenericRepository<Course> _courseGR;
        private IGenericRepository<Product> _productGR;
        private IGenericRepository<FactorforsaleProduct> _factorforsaleProductGR;
        private IGenericRepository<Cart> _cartGR;
        private IGenericRepository<Shareholder> _shareholderGR;
        private IGenericRepository<Level> _levelGR;
        private IGenericRepository<VideoSeenInfo> _videoSeenInfoGR;
        private IGenericRepository<Wallet> _walletGR;
        private IGenericRepository<Bill> _billGR;
        private IGenericRepository<Gift> _giftGR;
        private IGenericRepository<Question> _questionGR;
        private IGenericRepository<Answer> _answerGR;
        private IGenericRepository<ExamResult> _examResultGR;
        private IGenericRepository<UserAnswer> _userAnswerGR;
        private IGenericRepository<Favorite> _favoriteGR;
        private IGenericRepository<Producter> _producterGR;
        private IGenericRepository<ProducterType> _producterTypeGR;
        private IGenericRepository<Commission> _commissionGR;
        private IGenericRepository<GeneologyType> _geneologyTypeGR;
        private IGenericRepository<GeneologyPlan> _geneologyPlanGR;
        private IGenericRepository<PlanUni> _planUniGR;
        private IGenericRepository<PlanUniLevel> _planUniLevelGR;
        private IGenericRepository<CommissionHistory> _commissionHistoryGR;
        private IGenericRepository<PlanDelta> _planDeltaGR;
        private IGenericRepository<PlanBinary> _planBinaryGR;
        private IGenericRepository<SellPerMonth> _sellPerMonthGR;
        private IGenericRepository<CommissionPerMonth> _commissionPerMonthGR;
        private IGenericRepository<PlanBreakAWay> _planBreakAWayGR;
        private IGenericRepository<PlanBreakAWayLevel> _planBreakAWayLevelGR;
        private IGenericRepository<DividendAmountHistory> _dividendAmountHistoryGR;
        private IGenericRepository<Comment> _commentGR;
        private IGenericRepository<Blog> _blogGR;
        private IGenericRepository<Point> _pointGR;
        private IGenericRepository<ShopHomeSlider> _shopHomeSliderGR;
        private IGenericRepository<Seen> _seenGR;
        private IGenericRepository<Like> _likeGR;
        private IGenericRepository<PostChangeRequest> _postChangeRequestGR;
        private IGenericRepository<Chat> _chatGR;
        private IGenericRepository<ChatContact> _chatContactGR;
        private IGenericRepository<Video> _videoGR;
        private IGenericRepository<UserPushToken> _userPushTokenGR;
        private IGenericRepository<Notifi> _notifiGR;
        private IGenericRepository<NotifiLog> _notifiLogGR;
        private IGenericRepository<JobResume> _jobResumeGR;
        private IGenericRepository<EducationalRecord> _educationalRecordGR;
        private IGenericRepository<WorkSample> _workSampleGR;
        private IGenericRepository<Country> _countryGR;
        private IGenericRepository<Resources> _resourcesGR;
        private IGenericRepository<FieldAndOrientation> _fieldAndOrientationGR;
        private IGenericRepository<AppUpdate> _appUpdateGR;


        public IGenericRepository<Post> PostGR
        {
            get { return _postGR = _postGR ?? new GenericRepository<Post>(_context); }
        }
        public IGenericRepository<Skill> SkillGR
        {
            get { return _skillGR = _skillGR ?? new GenericRepository<Skill>(_context); }
        }
        public IGenericRepository<UserGeneology> UserGeneologyGR
        {
            get { return _userGeneologytGR = _userGeneologytGR ?? new GenericRepository<UserGeneology>(_context); }
        }
        public IGenericRepository<UserSetting> UserSettingGR
        {
            get { return _userSettingGR = _userSettingGR ?? new GenericRepository<UserSetting>(_context); }
        }
        public IGenericRepository<ProductScale> ProductScaleGR
        {
            get { return _productScaleGR = _productScaleGR ?? new GenericRepository<ProductScale>(_context); }
        }
        public IGenericRepository<ProductSeenInfo> ProductSeenInfoGR
        {
            get { return _productSeenInfoGR = _productSeenInfoGR ?? new GenericRepository<ProductSeenInfo>(_context); }
        }
        public IGenericRepository<UserCategory> UserCategoryGR
        {
            get { return _userCategoryGR = _userCategoryGR ?? new GenericRepository<UserCategory>(_context); }
        }
        public IGenericRepository<Category_Category> Category_CategoryGR
        {
            get { return _category_CategoryGR = _category_CategoryGR ?? new GenericRepository<Category_Category>(_context); }
        }
        public IGenericRepository<Category> CategoryGR
        {
            get { return _categoryGR = _categoryGR ?? new GenericRepository<Category>(_context); }
        }
        public IGenericRepository<SuggestedProduct> SuggestedProductGR
        {
            get { return _suggestedProductGR = _suggestedProductGR ?? new GenericRepository<SuggestedProduct>(_context); }
        }
        public IGenericRepository<FinancialAdvice> FinancialAdviceGR
        {
            get { return _financialAdviceGR = _financialAdviceGR ?? new GenericRepository<FinancialAdvice>(_context); }
        }
        public IGenericRepository<City> CityGR
        {
            get { return _cityGR = _cityGR ?? new GenericRepository<City>(_context); }
        }
        public IGenericRepository<Province> ProvinceGR
        {
            get { return _provinceGR = _provinceGR ?? new GenericRepository<Province>(_context); }
        }
        public IGenericRepository<ProjectSetting> ProjectSettingGR
        {
            get { return _projectSettingGR = _projectSettingGR ?? new GenericRepository<ProjectSetting>(_context); }
        }
        public IGenericRepository<Vision> VisionGR
        {
            get { return _visionGR = _visionGR ?? new GenericRepository<Vision>(_context); }
        }
        public IGenericRepository<Book> BookGR
        {
            get { return _bookGR = _bookGR ?? new GenericRepository<Book>(_context); }
        }
        public IGenericRepository<Exam> ExamGR
        {
            get { return _examGR = _examGR ?? new GenericRepository<Exam>(_context); }
        }
        public IGenericRepository<Course> CourseGR
        {
            get { return _courseGR = _courseGR ?? new GenericRepository<Course>(_context); }
        }
        public IGenericRepository<Product> ProductGR
        {
            get { return _productGR = _productGR ?? new GenericRepository<Product>(_context); }
        }
        public IGenericRepository<FactorforsaleProduct> FactorforsaleProductGR
        {
            get { return _factorforsaleProductGR = _factorforsaleProductGR ?? new GenericRepository<FactorforsaleProduct>(_context); }
        }
        public IGenericRepository<Cart> CartGR
        {
            get { return _cartGR = _cartGR ?? new GenericRepository<Cart>(_context); }
        }
        public IGenericRepository<Shareholder> ShareholderGR
        {
            get { return _shareholderGR = _shareholderGR ?? new GenericRepository<Shareholder>(_context); }
        }
        public IGenericRepository<Level> LevelGR
        {
            get { return _levelGR = _levelGR ?? new GenericRepository<Level>(_context); }
        }
        public IGenericRepository<VideoSeenInfo> VideoSeenInfoGR
        {
            get { return _videoSeenInfoGR = _videoSeenInfoGR ?? new GenericRepository<VideoSeenInfo>(_context); }
        }
        public IGenericRepository<Wallet> WalletGR
        {
            get { return _walletGR = _walletGR ?? new GenericRepository<Wallet>(_context); }
        }
        public IGenericRepository<Bill> BillGR
        {
            get { return _billGR = _billGR ?? new GenericRepository<Bill>(_context); }
        }
        public IGenericRepository<Gift> GiftGR
        {
            get { return _giftGR = _giftGR ?? new GenericRepository<Gift>(_context); }
        }
        public IGenericRepository<Question> QuestionGR
        {
            get { return _questionGR = _questionGR ?? new GenericRepository<Question>(_context); }
        }
        public IGenericRepository<Answer> AnswerGR
        {
            get { return _answerGR = _answerGR ?? new GenericRepository<Answer>(_context); }
        }
        public IGenericRepository<ExamResult> ExamResultGR
        {
            get { return _examResultGR = _examResultGR ?? new GenericRepository<ExamResult>(_context); }
        }
        public IGenericRepository<UserAnswer> UserAnswerGR
        {
            get { return _userAnswerGR = _userAnswerGR ?? new GenericRepository<UserAnswer>(_context); }
        }
        public IGenericRepository<Favorite> FavoriteGR
        {
            get { return _favoriteGR = _favoriteGR ?? new GenericRepository<Favorite>(_context); }
        }
        public IGenericRepository<Producter> ProducterGR
        {
            get { return _producterGR = _producterGR ?? new GenericRepository<Producter>(_context); }
        }
        public IGenericRepository<ProducterType> ProducterTypeGR
        {
            get { return _producterTypeGR = _producterTypeGR ?? new GenericRepository<ProducterType>(_context); }
        }
        public IGenericRepository<Commission> CommissionGR
        {
            get { return _commissionGR = _commissionGR ?? new GenericRepository<Commission>(_context); }
        }
        public IGenericRepository<GeneologyType> GeneologyTypeGR
        {
            get { return _geneologyTypeGR = _geneologyTypeGR ?? new GenericRepository<GeneologyType>(_context); }
        }
        public IGenericRepository<GeneologyPlan> GeneologyPlanGR
        {
            get { return _geneologyPlanGR = _geneologyPlanGR ?? new GenericRepository<GeneologyPlan>(_context); }
        }
        public IGenericRepository<PlanUni> PlanUniGR
        {
            get { return _planUniGR = _planUniGR ?? new GenericRepository<PlanUni>(_context); }
        }
        public IGenericRepository<PlanUniLevel> PlanUniLevelGR
        {
            get { return _planUniLevelGR = _planUniLevelGR ?? new GenericRepository<PlanUniLevel>(_context); }
        }
        public IGenericRepository<CommissionHistory> CommissionHistoryGR
        {
            get { return _commissionHistoryGR = _commissionHistoryGR ?? new GenericRepository<CommissionHistory>(_context); }
        }
        public IGenericRepository<PlanDelta> PlanDeltaGR
        {
            get { return _planDeltaGR = _planDeltaGR ?? new GenericRepository<PlanDelta>(_context); }
        }
        public IGenericRepository<PlanBinary> PlanBinaryGR
        {
            get { return _planBinaryGR = _planBinaryGR ?? new GenericRepository<PlanBinary>(_context); }
        }
        public IGenericRepository<SellPerMonth> SellPerMonthGR
        {
            get { return _sellPerMonthGR = _sellPerMonthGR ?? new GenericRepository<SellPerMonth>(_context); }
        }
        public IGenericRepository<CommissionPerMonth> CommissionPerMonthGR
        {
            get { return _commissionPerMonthGR = _commissionPerMonthGR ?? new GenericRepository<CommissionPerMonth>(_context); }
        }
        public IGenericRepository<PlanBreakAWay> PlanBreakAWayGR
        {
            get { return _planBreakAWayGR = _planBreakAWayGR ?? new GenericRepository<PlanBreakAWay>(_context); }
        }
        public IGenericRepository<PlanBreakAWayLevel> PlanBreakAWayLevelGR
        {
            get { return _planBreakAWayLevelGR = _planBreakAWayLevelGR ?? new GenericRepository<PlanBreakAWayLevel>(_context); }
        }
        public IGenericRepository<DividendAmountHistory> DividendAmountHistoryGR
        {
            get { return _dividendAmountHistoryGR = _dividendAmountHistoryGR ?? new GenericRepository<DividendAmountHistory>(_context); }
        }
        public IGenericRepository<Comment> CommentGR
        {
            get { return _commentGR = _commentGR ?? new GenericRepository<Comment>(_context); }
        }
        public IGenericRepository<Blog> BlogGR
        {
            get { return _blogGR = _blogGR ?? new GenericRepository<Blog>(_context); }
        }
        public IGenericRepository<Point> PointGR
        {
            get { return _pointGR = _pointGR ?? new GenericRepository<Point>(_context); }
        }
        public IGenericRepository<ShopHomeSlider> ShopHomeSliderGR
        {
            get { return _shopHomeSliderGR = _shopHomeSliderGR ?? new GenericRepository<ShopHomeSlider>(_context); }
        }
        public IGenericRepository<Seen> SeenGR
        {
            get { return _seenGR = _seenGR ?? new GenericRepository<Seen>(_context); }
        }
        public IGenericRepository<Like> LikeGR
        {
            get { return _likeGR = _likeGR ?? new GenericRepository<Like>(_context); }
        }
        public IGenericRepository<PostChangeRequest> PostChangeRequestGR
        {
            get { return _postChangeRequestGR = _postChangeRequestGR ?? new GenericRepository<PostChangeRequest>(_context); }
        }
        public IGenericRepository<Chat> ChatGR
        {
            get { return _chatGR = _chatGR ?? new GenericRepository<Chat>(_context); }
        }
        public IGenericRepository<ChatContact> ChatContactGR
        {
            get { return _chatContactGR = _chatContactGR ?? new GenericRepository<ChatContact>(_context); }
        }
        public IGenericRepository<Video> VideoGR
        {
            get { return _videoGR = _videoGR ?? new GenericRepository<Video>(_context); }
        }
        public IGenericRepository<UserPushToken> UserPushTokenGR
        {
            get { return _userPushTokenGR = _userPushTokenGR ?? new GenericRepository<UserPushToken>(_context); }
        }
        public IGenericRepository<Notifi> NotifiGR
        {
            get { return _notifiGR = _notifiGR ?? new GenericRepository<Notifi>(_context); }
        }
        public IGenericRepository<NotifiLog> NotifiLogGR
        {
            get { return _notifiLogGR = _notifiLogGR ?? new GenericRepository<NotifiLog>(_context); }
        }
        public IGenericRepository<JobResume> JobResumeGR
        {
            get { return _jobResumeGR = _jobResumeGR ?? new GenericRepository<JobResume>(_context); }
        }
        public IGenericRepository<EducationalRecord> EducationalRecordGR
        {
            get { return _educationalRecordGR = _educationalRecordGR ?? new GenericRepository<EducationalRecord>(_context); }
        }
        public IGenericRepository<WorkSample> WorkSampleGR
        {
            get { return _workSampleGR = _workSampleGR ?? new GenericRepository<WorkSample>(_context); }
        }
        public IGenericRepository<Country> CountryGR
        {
            get { return _countryGR = _countryGR ?? new GenericRepository<Country>(_context); }
        }
        public IGenericRepository<Resources> ResourcesGR
        {
            get { return _resourcesGR = _resourcesGR ?? new GenericRepository<Resources>(_context); }
        }
        public IGenericRepository<FieldAndOrientation> FieldAndOrientationGR
        {
            get { return _fieldAndOrientationGR = _fieldAndOrientationGR ?? new GenericRepository<FieldAndOrientation>(_context); }
        }
        public IGenericRepository<AppUpdate> AppUpdateGR
        {
            get { return _appUpdateGR = _appUpdateGR ?? new GenericRepository<AppUpdate>(_context); }
        }
        #endregion

        public Result SaveChanges()
        {
            Result Result = new Result();
            try
            {
                if (Convert.ToBoolean(_context.SaveChanges()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات با موفقیت انجام شد";
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات با موفقیت انجام نشد";
                    return Result;
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Result;
            }
        }
        public Task<Result> SaveChangesAsync()
        {
            Result Result = new Result();
            try
            {
                if (Convert.ToBoolean(_context.SaveChangesAsync()))
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "عملیات با موفقیت انجام شد";
                    return Task.FromResult(Result);
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "متاسفانه عملیات با موفقیت انجام نشد";
                    return Task.FromResult(Result);
                }
            }
            catch (Exception ex)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = ex.Message;
                return Task.FromResult(Result);
            }
        }
        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
}
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Advice;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.General;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.SocialNetwork;
using newFace.Shared.Models.User;
using newFace.Shared.Models.ViewModels;
using static newFace.Shared.Models.Resource.Resources;

namespace newFace.Server.Data
{
    public class newFaceDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public newFaceDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }
        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //for may cause cycles or multiple cascade paths. Specify ON DELETE....
            modelBuilder.Entity<EducationalRecord>()
                .HasOne(e => e.OrientationFromFAndO)
                .WithMany(f => f.EducationalRecordsOrention)
                .HasForeignKey(e => e.OrientationId)
                .OnDelete(DeleteBehavior.Restrict);

            //for may cause cycles or multiple cascade paths. Specify ON DELETE....
            modelBuilder.Entity<JobResume>()
                .HasOne(j => j.JobPosition)
                .WithMany(r => r.JobPosition)
                .HasForeignKey(j => j.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);

            //for may cause cycles or multiple cascade paths. Specify ON DELETE....
            modelBuilder.Entity<UserAnswer>()
                .HasOne(uA => uA.ExamResult)
                .WithMany(exRs => exRs.UserAnswersLists)
                .HasForeignKey(uA => uA.ExamResultId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Resources>()
                .HasData(
                    new Resources { Id = 1, Name = "عمومی", ResourcesType = ResourcesTypeEnum.Level },
                    new Resources { Id = 2, Name = "تخصصی", ResourcesType = ResourcesTypeEnum.Level },
                    new Resources { Id = 3, Name = "نیمه تخصصی", ResourcesType = ResourcesTypeEnum.Level },
                    new Resources { Id = 4, Name = "فوق تخصصی", ResourcesType = ResourcesTypeEnum.Level },
                    new Resources { Id = 5, Name = "فارسی", ResourcesType = ResourcesTypeEnum.Language },
                    new Resources { Id = 6, Name = "انگلیسی", ResourcesType = ResourcesTypeEnum.Language }
                );
            modelBuilder.Entity<Level>()
                .HasData(
                    new Level { Id = 1, Name = "مقدماتی", Number = 1 },
                    new Level { Id = 2, Name = "متوسط", Number = 2 },
                    new Level { Id = 3, Name = "پیشرفته", Number = 3 },
                    new Level { Id = 4, Name = "فوق پیشرفته", Number = 4 }
                );
            modelBuilder.Entity<PointType>()
                .HasData(
                    new PointType {Id = 1,  Name = "Like" },
                    new PointType { Id = 2, Name = "DisLike" }
                );
            modelBuilder.Entity<Microsoft.AspNetCore.Identity.IdentityRole>()
                .HasData(
                    new Microsoft.AspNetCore.Identity.IdentityRole { Id = "1", Name = "Administrator" },
                    new Microsoft.AspNetCore.Identity.IdentityRole { Id = "2", Name = "Admin" },
                    new Microsoft.AspNetCore.Identity.IdentityRole { Id = "3", Name = "Operator" },
                    new Microsoft.AspNetCore.Identity.IdentityRole { Id = "4", Name = "User" }
                );

            modelBuilder.Entity<GeneologyType>()
                .HasData(
                    new GeneologyType {Id = 1,  Title = "معرف", CalculationTime = CalculationTime.MomentOfSale, RowType = RowType.Sell, SystemType = SystemType.ForsatReagent, Type = GeneologyTypeEnum.ForsatReagent },
                    new GeneologyType {Id = 2,  Title = "مدیر توسعه بیمه", CalculationTime = CalculationTime.MomentOfSale, RowType = RowType.Sell, SystemType = SystemType.Insurance, Type = GeneologyTypeEnum.DevelopmentManagerInsurance },
                    new GeneologyType {Id = 3,  Title = "مدیر آموزش بیمه", CalculationTime = CalculationTime.MomentOfSale, RowType = RowType.Sell, SystemType = SystemType.Insurance, Type = GeneologyTypeEnum.DirectorOfEducationInsurance },
                    new GeneologyType {Id = 4,  Title = "مدیر توسعه بورس", CalculationTime = CalculationTime.MomentOfSale, RowType = RowType.Sell, SystemType = SystemType.Exchange, Type = GeneologyTypeEnum.DevelopmentManagerExchange },
                    new GeneologyType { Id = 5, Title = "مدیر آموزش بورس", CalculationTime = CalculationTime.MomentOfSale, RowType = RowType.Sell, SystemType = SystemType.Exchange, Type = GeneologyTypeEnum.DirectorOfEducationExchange }
                );
            modelBuilder.Entity<Category>()
                .HasData(
                    new Category { Id = 1, Title = "فرصت", Img = null, CategoryType = CategoryTypeEnum.Subject, CategoryFinancialType = CategoryFinancialTypeEnum.Null }
                );
            modelBuilder.Entity<ProjectSetting>()
                .HasData(
                    new ProjectSetting { Id = 1, DefultCategoryId = 1, Logo = null, Title = "فرصت", MaxCapacity = 15 }
                    );
            //modelBuilder.Entity<Chat>()
            //    .HasOne<ApplicationUser>(chat => chat.Sender)
            //    .WithMany(user => user.Chat)
            //    .HasForeignKey(chat => chat.SenderId);
            //modelBuilder.Entity<Chat>()
            //    .HasOne<ApplicationUser>(chat => chat.Receiver)
            //    .WithMany(user => user.Chat1)
            //    .HasForeignKey(chat => chat.ReceiverId);
            //modelBuilder.Entity<Comment>()
            //    .HasMany(e => e.FirstCommentsChilds)
            //    .WithOptional(e => e.FirstCommentParent)
            //    .HasForeignKey(e => e.FirstParentId);

            //modelBuilder.Entity<Comment>()
            //    .HasMany(e => e.CommentsChilds)
            //    .WithOptional(e => e.CommentParent)
            //    .HasForeignKey(e => e.ParentId);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.Comments)
            //    .WithOptional(e => e.Product)
            //    .HasForeignKey(e => e.ProductId);

            //modelBuilder.Entity<Producter>()
            //    .HasMany(e => e.Books)
            //    .WithOptional(e => e.Author)
            //    .HasForeignKey(e => e.AuthorId);

            //modelBuilder.Entity<Producter>()
            //    .HasMany(e => e.Courses)
            //    .WithOptional(e => e.Teacher)
            //    .HasForeignKey(e => e.TeacherId);

            //modelBuilder.Entity<Producter>()
            //    .HasMany(e => e.Exams)
            //    .WithOptional(e => e.Designer)
            //    .HasForeignKey(e => e.DesignerId);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.FactorforsaleProducts)
            //    .WithRequired(e => e.Users)
            //    .HasForeignKey(e => e.UserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Product>()
            //    .HasMany(e => e.Shareholders)
            //    .WithRequired(e => e.Product)
            //    .HasForeignKey(e => e.ProductId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.Shareholders)
            //    .WithRequired(e => e.Users)
            //    .HasForeignKey(e => e.UserId);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.Vision)
            //    .WithRequired(e => e.Users)
            //    .HasForeignKey(e => e.UserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.DevelopmentManagerUsers)
            //    .WithOptional(e => e.DevelopmentManager)
            //    .HasForeignKey(e => e.DevelopmentManagerUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.DirectorOfEducationUsers)
            //    .WithOptional(e => e.DirectorOfEducation)
            //    .HasForeignKey(e => e.DirectorOfEducationUserId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ApplicationUser>()
            //    .HasMany(e => e.Commissions)
            //    .WithRequired(e => e.SubsetUser)
            //    .HasForeignKey(e => e.SubsetId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<FieldAndOrientation>()
            //    .HasMany(e => e.EducationalRecordsfield)
            //    .WithRequired(e => e.FieldFromFAndO)
            //    .HasForeignKey(e => e.FieldId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<FieldAndOrientation>()
            //    .HasMany(e => e.EducationalRecordsOrention)
            //    .WithRequired(e => e.OrientationFromFAndO)
            //    .HasForeignKey(e => e.OrientationId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Resources>()
            //    .HasMany(e => e.JobPosition)
            //    .WithRequired(e => e.JobPosition)
            //    .HasForeignKey(e => e.JobPositionId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Resources>()
            //    .HasMany(e => e.University)
            //    .WithRequired(e => e.University)
            //    .HasForeignKey(e => e.UniversityId)
            //    .WillCascadeOnDelete(false);
            //modelBuilder.Entity<Resources>()
            //    .HasMany(e => e.Company)
            //    .WithRequired(e => e.Company)
            //    .HasForeignKey(e => e.CompanyId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<ExamResult>()
            //    .HasMany(e => e.UserAnswersLists)
            //    .WithRequired(e => e.ExamResult)
            //    .HasForeignKey(e => e.ExamResultId)
            //    .WillCascadeOnDelete(false);



        }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Cat_Post> Cat_Posts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatContact> ChatContacts { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<EducationalRecord> EducationalRecords { get; set; }
        public virtual DbSet<Exam> Exams { get; set; }
        public virtual DbSet<FactorforsaleProduct> FactorforsaleProducts { get; set; }
        public virtual DbSet<Gift> Gifts { get; set; }
        public virtual DbSet<JobResume> JobResumes { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostChangeRequest> PostChangeRequests { get; set; }
        public virtual DbSet<Producter> Producters { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductScale> ProductScales { get; set; }
        public virtual DbSet<ProductSeenInfo> ProductSeenInfoes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Shareholder> Shareholders { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Vision> Visions { get; set; }
        public virtual DbSet<WorkSample> WorkSamples { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<FieldAndOrientation> FieldAndOrientations { get; set; }
        public virtual DbSet<SuggestedProduct> SuggestedProducts { get; set; }
        public virtual DbSet<InsuranceInput> InsuranceInputs { get; set; }
        public virtual DbSet<DividendAmountHistory> DividendAmountHistories { get; set; }
        public virtual DbSet<CommissionPerMonth> CommissionPerMonths { get; set; }
        public virtual DbSet<CommissionHistory> CommissionHistories { get; set; }
        public virtual DbSet<SellPerMonth> SellPerMonths { get; set; }
        public virtual DbSet<ExchangeInput> ExchangeInputs { get; set; }
        public virtual DbSet<BankInput> BankInputs { get; set; }
        public virtual DbSet<UserSetting> UserSettings { get; set; }
        public virtual DbSet<ProjectSetting> ProjectSettings { get; set; }


        //--------------------------------- کشور - استان - شهر  ----------------------------------------------------------
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<City> Cities { get; set; }

        //--------------------------------- جنسیت - وضعیت خدمت - وضعیت شغلی - وضعیت تاهل -------------------------------
        //public virtual DbSet<Gender> Genders { get; set; }
        //public virtual DbSet<SolderStatus> SolderStatuses { get; set; }
        //public virtual DbSet<JobStatus> JobStatuses { get; set; }
        //public virtual DbSet<MaritalStatuse> MaritalStatuses { get; set; }

        public virtual DbSet<Point> Points { get; set; }
        public virtual DbSet<PointType> PointTypes { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Seen> Seens { get; set; }
        //public virtual DbSet<Level> Levels { get; set; }

        public virtual DbSet<Resources> Resources { get; set; }

        public virtual DbSet<Commission> Commissions { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<ShopHomeSlider> ShopHomeSliders { get; set; }

        public DbSet<Wallet> Wallets { get; set; }

        public DbSet<Blog> Blogs { get; set; }

        public DbSet<UserAnswer> UserAnswers { get; set; }

        public DbSet<ExamResult> ExamResults { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        public DbSet<CategoryLevel> CategoryLevels { get; set; }

        public DbSet<GeneologyPlan> GeneologyPlans { get; set; }

        public DbSet<GeneologyType> GeneologyTypes { get; set; }

        public DbSet<PlanDelta> PlanDeltas { get; set; }

        public DbSet<PlanUni> PlanUnis { get; set; }

        public DbSet<PlanUniLevel> PlanUniLevels { get; set; }

        public DbSet<PlanBreakAWayLevel> PlanBreakAWayLevels { get; set; }

        public DbSet<PlanBreakAWay> PlanBreakAWays { get; set; }

        public DbSet<PlanBinary> PlanBinaries { get; set; }

        public DbSet<ChatLog> ChatLogs { get; set; }

        public DbSet<FinancialAdvice> FinancialAdvices { get; set; }
        public DbSet<UserCategory> UserCategories { get; set; }
        public DbSet<AppUpdate> AppUpdates { get; set; }

        public DbSet<Notifi> Notifis { get; set; }
        public DbSet<NotifiLog> NotifiLogs { get; set; }
        public DbSet<UserPushToken> UserPushTokens { get; set; }

    }





}

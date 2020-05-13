using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.User;
using newFace.Shared.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
//
using Microsoft.AspNetCore.Mvc;

namespace newFace.Shared.Models
{
    public class ApplicationUser: IdentityUser
    {
        public ApplicationUser()
        {
            UserInfoComplatePercent = 5.555555555555554;
            CDate = DateTime.Now;
        }


        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "نام مستعار")]
        public string NickName { get; set; }

        //[JsonIgnore]
        [Display(Name = "امتیاز")]
        public double Point { get; set; }

        [Display(Name = "سن")]
        public int? Age { get; set; }

        [JsonIgnore]
        [Display(Name = "کدملی")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public string NationalCode { get; set; }


        [Display(Name = "تلفن ثابت")]
        [JsonIgnore]
        public string Phone { get; set; }

        [JsonIgnore]
        [Display(Name = "تاریخ تولد")]
        public DateTime? BirthDate { get; set; }

        [JsonIgnore]
        public string Desc { get; set; }


        [Display(Name = "تصویر پروفایل")]
        public string Img { get; set; }

        [JsonIgnore]
        public double UserInfoComplatePercent { get; set; }

        [JsonIgnore]
        [Display(Name = "آدرس")]
        public string Address { get; set; }

        [JsonIgnore]
        public int? Type { get; set; }

        [Display(Name = "اعتبار کیف پول")]
        public long WalletCredit { get; set; }

        //--------------------------------------------------------
        [JsonIgnore]
        public bool Active { get; set; }

        [JsonIgnore]
        public DateTime? CDate { get; set; }

        [JsonIgnore]
        public DateTime? MDate { get; set; }

        [JsonIgnore]
        public bool? IsDeleted { get; set; }

        [JsonIgnore]
        public string CUser { get; set; }

        [JsonIgnore]
        public string MUser { get; set; }

        [JsonIgnore]
        public string DUser { get; set; }

        [JsonIgnore]
        public DateTime? DDate { get; set; }
        [Display(Name = "اعتبار کاربر")]
        public double Credit { get; set; }
        //---------------------------------------------------- شبکه اجتماعی ----------------------------------------------
        [JsonIgnore]
        public string GooglePlus { get; set; }

        [JsonIgnore]
        public string WebSite { get; set; }

        [JsonIgnore]
        public string GitHub { get; set; }

        [JsonIgnore]
        public string LinkedIn { get; set; }

        [JsonIgnore]
        public string WhatsApp { get; set; }

        [JsonIgnore]
        public string Instageram { get; set; }

        [JsonIgnore]
        public string Telegram { get; set; }

        [JsonIgnore]
        public string Twitter { get; set; }

        [JsonIgnore]
        public string AboutMe { get; set; }

        //------------------------------------------------------------------------------------------------------------------
        [JsonIgnore]
        [Display(Name = " شهر")]
        public int? CityId { get; set; }
        //////[ScriptIgnore]
        [JsonIgnore]
        [ForeignKey("CityId")]
        public virtual City City { get; set; }
        //------------------------------------------------------------------------------------------------------------------
        [Display(Name = "جنسیت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.Gender? GenderId { get; set; }

        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت سلامت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.HealthStatus? HealthStatusId { get; set; }

        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت خدمت")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.SolderStatus? SolderStatusId { get; set; }
        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت تحصیلی")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.EducationalStatus? EducationalStatusId { get; set; }
        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت شغلی")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.JobStatus? JobStatusId { get; set; }

        //-------------------------------------------------------------------------------------------------------------------
        [Display(Name = "وضعیت تاهل")]
        [DisplayFormat(NullDisplayText = "ثبت نشده است")]
        public Enums.MaritalStatuse? MaritalStatusId { get; set; }

        //------------------------------------------------------------------------------------------------------------------


        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Answer> Answers { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Bill> Bill { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("Sender")]
        public virtual ICollection<Chat> SendChat { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("Receiver")]
        public virtual ICollection<Chat> RecivChat { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public ICollection<Comment> Comment { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<EducationalRecord> EducationalRecords { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<FactorforsaleProduct> FactorforsaleProducts { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("SendUsers")]
        public virtual ICollection<Gift> SendGift { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("ResiveUsers")]
        public virtual ICollection<Gift> RecivGift { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<JobResume> JobResumes { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Post> Post { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<PostChangeRequest> PostChangeRequests { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Shareholder> Shareholders { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Commission> Commissions { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<UsersEpubBookInfo> UsersEpubBookInfos { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Skill> Skills { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("Users")]
        public virtual ICollection<Vision> Vision { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("DirectorOfEducation")]
        public virtual ICollection<Vision> DirectorOfEducationUsers { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        [InverseProperty("DevelopmentManager")]
        public virtual ICollection<Vision> DevelopmentManagerUsers { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<WorkSample> WorkSamples { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<Like> Likes { get; set; }

        ////[ScriptIgnore]
        [JsonIgnore]
        public virtual ICollection<UserCategory> UserCategorys { get; set; }

        [JsonIgnore]
        public List<Favorite> Favorites { get; set; }
        public bool IsFavorite { get; set; }

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override string PasswordHash
        //{
        //    get { return base.PasswordHash; }
        //    set { base.PasswordHash = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override string Email
        //{
        //    get { return base.Email; }
        //    set { base.Email = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override int AccessFailedCount
        //{
        //    get { return base.AccessFailedCount; }
        //    set { base.AccessFailedCount = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override bool EmailConfirmed
        //{
        //    get { return base.EmailConfirmed; }
        //    set { base.EmailConfirmed = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override bool LockoutEnabled
        //{
        //    get { return base.LockoutEnabled; }
        //    set { base.LockoutEnabled = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override DateTimeOffset? LockoutEnd
        //{
        //    get { return base.LockoutEnd; }
        //    set { base.LockoutEnd = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override bool TwoFactorEnabled
        //{
        //    get { return base.TwoFactorEnabled; }
        //    set { base.TwoFactorEnabled = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override bool PhoneNumberConfirmed
        //{
        //    get { return base.PhoneNumberConfirmed; }
        //    set { base.PhoneNumberConfirmed = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //[Remote("IsDuplicatePhoneNumber", "Account", ErrorMessage = "شماره موبایل وارد شده تکراری میباشد")]
        //public override string PhoneNumber
        //{
        //    get { return base.PhoneNumber; }
        //    set { base.PhoneNumber = value; }
        //}


        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override string SecurityStamp
        //{
        //    get { return base.SecurityStamp; }
        //    set { base.SecurityStamp = value; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override string UserName
        //{
        //    get { return base.UserName; }
        //    set { base.UserName = value; }
        //}

        ////[ScriptIgnore]
        //[JsonIgnore]
        //public override ICollection<IdentityUserClaim> Claims
        //{
        //    get { return base.Claims; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override ICollection<IdentityUserLogin> Logins
        //{
        //    get { return base.Logins; }
        //}

        //////[ScriptIgnore]
        //[JsonIgnore]
        //public override ICollection<IdentityUserRole> Roles
        //{
        //    get { return base.Roles; }
        //}


    }
}

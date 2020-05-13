using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using newFace.Shared;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Models.ViewModels.CategoryViewModel;

namespace newFace.Server.Utility
{
    public static class ViewModelConvert
    {

        #region UserVM
        public static ApplicationUser ConvertUserVmToUser(this ProfileEditViewModel model)
        {
            ApplicationUser user = new ApplicationUser()
            {
                Id = model.UserId,
                FullName = model.FullName,
                PhoneNumber = model.PhoneNumber,
                Phone = model.Phone,
                NickName = model.NickName,
                UserName = model.UserName,
                NationalCode = model.NationalCode,
                CityId = model.CityId,
                GenderId = model.GenderId,
                SolderStatusId = model.SolderStatusId,
                JobStatusId = model.JobStatusId,
                MaritalStatusId = model.MaritalStatusId,
                Email = model.Email,
                Img = model.Img,
                Address = model.Address,
                BirthDate = String.IsNullOrEmpty(model.BirthDate) ? (DateTime?)null : model.BirthDate.ToDateTime(),
                AboutMe = model.AboutMe,
                Age = String.IsNullOrEmpty(model.BirthDate) ? 0 : model.BirthDate.ToDateTime().CalculateAge(),
                HealthStatusId = model.HealthStatusId

            };
            if (model.GenderId == Enums.Gender.Women)
            {
                user.SolderStatusId = null;
            }
            return user;
        }
        public static ProfileEditViewModel ConvertUserToUserVm(this ApplicationUser user)
        {
            ProfileEditViewModel editViewModel = new ProfileEditViewModel()
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                Address = user.Address,
                BirthDate = user.BirthDate == null ? "" : Convert.ToDateTime(user.BirthDate).MiladiToJalali(),
                CityId = user.CityId,
                CountryId = user.City?.Province?.CountryId,
                ProvinceId = user.City?.ProvinceId,
                GenderId = user.GenderId,
                AboutMe = user.AboutMe,
                Img = user.Img,
                JobStatusId = user.JobStatusId,
                MaritalStatusId = user.MaritalStatusId,
                PhoneNumber = user.PhoneNumber,
                Phone = user.Phone,
                NationalCode = user.NationalCode,
                NickName = user.NickName,
                SolderStatusId = user.SolderStatusId,
                City = user.City,
                Email = user.Email,
                GenderName = user.GenderId == null ? "" : user.GenderId.GetDisplayName(),
                SolderStatusName = user.SolderStatusId == null ? "" : user.SolderStatusId.GetDisplayName(),
                HealthStatusName = user.HealthStatusId == null ? "" : user.HealthStatusId.GetDisplayName(),
                JobStatusName = user.JobStatusId == null ? "" : user.JobStatusId.GetDisplayName(),
                MaritalStatusName = user.MaritalStatusId == null ? "" : user.MaritalStatusId.GetDisplayName(),
                CountryName = user.City == null ? "" : user.City.Province.Country.Name,
                ProvinceName = user.City == null ? "" : user.City.Province.Name,
                CityName = user.City == null ? "" : user.City.Name,
                SecurityStamp = user.SecurityStamp,
                Credit = user.Credit,
                Point = user.Point,
                HealthStatusId = user.HealthStatusId,
                IsFavorite = user.IsFavorite,
                FavoriteCount = user.Favorites == null ? 0 : user.Favorites.Count

            };
            return editViewModel;
        }
        #endregion

        #region SocialNetworkVM
        public static SocialNetworkViewModel ConvertUserToSocialNetworkVM(this ApplicationUser user)
        {
            SocialNetworkViewModel editViewModel = new SocialNetworkViewModel()
            {
                UserId = user.Id,
                GitHub = user.GitHub,
                GooglePlus = user.GooglePlus,
                Instageram = user.Instageram,
                LinkedIn = user.LinkedIn,
                Telegram = user.Telegram,
                Twitter = user.Twitter,
                WebSite = user.WebSite,
                WhatsApp = user.WhatsApp
            };
            return editViewModel;
        }
        public static ApplicationUser ConvertSocialNetworkVMToUser(this SocialNetworkViewModel model, ApplicationUser user)
        {
            user.GitHub = model.GitHub;
            user.GooglePlus = model.GooglePlus;
            user.Instageram = model.Instageram;
            user.LinkedIn = model.LinkedIn;
            user.Telegram = model.Telegram;
            user.Twitter = model.Twitter;
            user.WebSite = model.WebSite;
            user.WhatsApp = model.WhatsApp;
            return user;
        }

        #endregion

        #region Category

        public static Category ViewModelToCategory(this CategoryViewModels categoryViewModels)
        {
            return new Category()
            {
                Id = categoryViewModels.Id,
                Img = categoryViewModels.Img ?? "/Content/img/default_logo.jpg",
                CategoryFinancialType = categoryViewModels.CategoryFinancialType != CategoryFinancialTypeEnum.Null ? categoryViewModels.CategoryFinancialType : (CategoryFinancialTypeEnum?)null,
                CategoryType = categoryViewModels.CategoryType,
                Title = categoryViewModels.Title
            };

        }
        public static CategoryViewModels CategoryToViewModel(this Category category)
        {
            return new CategoryViewModels()
            {
                Id = category.Id,
                Title = category.Title,
                CategoryFinancialType = category.CategoryFinancialType != null ? category.CategoryFinancialType.Value : CategoryFinancialTypeEnum.Null,
                CategoryFinancialTypeName = category.CategoryFinancialType != null ? EnumHelper<CategoryFinancialTypeEnum>.GetDisplayValue(category.CategoryFinancialType.Value) : "",
                CategoryType = category.CategoryType,
                CategoryTypeName = EnumHelper<CategoryTypeEnum>.GetDisplayValue(category.CategoryType),
                Img = category.Img
            };

        }
        public static List<CategoryViewModels> ToCategoriesViewModel(this List<Category> categories)
        {
            return categories.Select(s => new CategoryViewModels()
            {
                Id = s.Id,
                Title = s.Title,
                CategoryFinancialType = s.CategoryFinancialType != null ? s.CategoryFinancialType.Value : CategoryFinancialTypeEnum.Null,
                CategoryFinancialTypeName = s.CategoryFinancialType != null ? s.CategoryFinancialType.GetDisplayName() : "",
                CategoryType = s.CategoryType,
                CategoryTypeName = EnumHelper<CategoryTypeEnum>.GetDisplayValue(s.CategoryType),
                Img = s.Img
            }).ToList();
        }
        public static List<Category_Category> ToCategoryCategories(this List<Category_CategoryViewModel> categoryCategoryViewModels)
        {
            return categoryCategoryViewModels.Select(s => new Category_Category()
            {
                Id = s.Id,
                ChildrenCatId = s.ChildrenCatId,
                ParentCatId = s.ParentCatId,
                Percent = s.Percent,
                Priority = s.Priority
            }).ToList();

        }
        public static List<Category_CategoryViewModel> ToCategory_CategoryViewModels(this List<Category_Category> categoryCategories)
        {

            return categoryCategories.Select(s => new Category_CategoryViewModel()
            {
                Id = s.Id,
                ChildrenCatId = s.ChildrenCatId,
                ParentCatId = (s.ParentCatId == null ? (int?)null : s.ParentCatId),
                ParentCatName = (s.Parent == null ? (string)null : s.Parent.Title),
                ChildrenCatName = s.Children.Title,
                Percent = s.Percent,
                Priority = s.Priority,
                CategoryType = s.Children.CategoryType,
                HasChild = s.Children.Parents.Any()

            }).ToList();
        }
        public static Category_Category ToCategoryCategorie(this Category_CategoryViewModel categoryCategoryViewModel)
        {
            return new Category_Category()
            {
                Id = categoryCategoryViewModel.Id,
                ChildrenCatId = categoryCategoryViewModel.ChildrenCatId,
                ParentCatId = categoryCategoryViewModel.ParentCatId,
                Percent = categoryCategoryViewModel.Percent,
                Priority = categoryCategoryViewModel.Priority
            };

        }
        public static Category_CategoryViewModel ToCategory_CategoryViewModel(this Category_Category categoryCategorie)
        {

            return new Category_CategoryViewModel()
            {
                Id = categoryCategorie.Id,
                ChildrenCatId = categoryCategorie.ChildrenCatId,
                ParentCatId = (categoryCategorie.ParentCatId == null ? (int?)null : categoryCategorie.ParentCatId),
                ParentCatName = (categoryCategorie.Parent == null ? (string)null : categoryCategorie.Parent.Title),
                ChildrenCatName = categoryCategorie.Children.Title,
                Percent = categoryCategorie.Percent,
                Priority = categoryCategorie.Priority,
                CategoryType = categoryCategorie.Children.CategoryType,
                HasChild = categoryCategorie.Children.Parents.Any()

            };
        }
        #endregion

        #region JobresumeViewModel
        public static List<JobResume> ConvertJobResumeVMsToJobResumes(this List<JobResumeViewModel> jobResumeViewModels)
        {
            return jobResumeViewModels.Select(jobResumeViewModel=>new JobResume()
            {
                Id = jobResumeViewModel.Id,
                StartDate = jobResumeViewModel.StartDate.ToDateTime(),
                EndDate = string.IsNullOrEmpty(jobResumeViewModel.EndDate) ? (DateTime?)null : jobResumeViewModel.EndDate.ToDateTime(),
                CompanyId = jobResumeViewModel.CompanyId,
                Desc = jobResumeViewModel.Desc,
                JobPositionId = jobResumeViewModel.JobPositionId,
                JobTitle = jobResumeViewModel.JobTitle,
                UserId = jobResumeViewModel.UserId

            }).ToList();
        }
        public static JobResume ConvertJobResumeVMToJobResume(this JobResumeViewModel jobResumeViewModel)
        {
            return new JobResume()
            {
                Id = jobResumeViewModel.Id,
                StartDate = jobResumeViewModel.StartDate.ToDateTime(),
                EndDate = string.IsNullOrEmpty(jobResumeViewModel.EndDate) ? (DateTime?)null : jobResumeViewModel.EndDate.ToDateTime(),
                CompanyId = jobResumeViewModel.CompanyId,
                Desc = jobResumeViewModel.Desc,
                JobPositionId = jobResumeViewModel.JobPositionId,
                JobTitle = jobResumeViewModel.JobTitle,
                UserId = jobResumeViewModel.UserId

            };
        }

        public static List<JobResumeViewModel> ConvertJobResumesToJobResumeVMs(this List<JobResume> jobResumes)
        {
            return jobResumes.Select(jobResume=>new JobResumeViewModel()
            {
                Id = jobResume.Id,
                StartDate = jobResume.StartDate.MiladiToJalali(),
                EndDate = jobResume.EndDate == null ? "بدون تاریخ" : Convert.ToDateTime(jobResume.EndDate).MiladiToJalali(),
                CompanyId = jobResume.CompanyId,
                Desc = jobResume.Desc,
                JobPosition = jobResume.JobPosition.Name,
                JobPositionId = jobResume.JobPositionId,
                JobTitle = jobResume.JobTitle,
                UserId = jobResume.UserId,
                Company = jobResume.Company.Name,
            }).ToList();
        }

        public static JobResumeViewModel ConvertJobResumeToJobResumeVM(this JobResume jobResume)
        {
            return new JobResumeViewModel()
            {
                Id = jobResume.Id,
                StartDate = jobResume.StartDate.MiladiToJalali(),
                EndDate = jobResume.EndDate == null ? "بدون تاریخ" : Convert.ToDateTime(jobResume.EndDate).MiladiToJalali(),
                CompanyId = jobResume.CompanyId,
                Desc = jobResume.Desc,
                JobPosition = jobResume.JobPosition.Name,
                JobPositionId = jobResume.JobPositionId,
                JobTitle = jobResume.JobTitle,
                UserId = jobResume.UserId,
                Company = jobResume.Company.Name,
            };
        }
        #endregion

        #region educationalRecordViewModel

        public static EducationalRecord ConvertEducationalVMToEducational(this EducationalRecordViewModel educationalRecordViewModel)
        {
            return new EducationalRecord()
            {
                Id = educationalRecordViewModel.Id,
                StartDate = educationalRecordViewModel.StartDate.ToDateTime(),
                EndDate = string.IsNullOrEmpty(educationalRecordViewModel.EndDate) ? (DateTime?)null : educationalRecordViewModel.EndDate.ToDateTime(),
                Desc = educationalRecordViewModel.Desc,
                UserId = educationalRecordViewModel.UserId,
                Grade = educationalRecordViewModel.Grade,
                OrientationId = educationalRecordViewModel.OrientationId,
                Average = educationalRecordViewModel.Average,
                UniversityId = educationalRecordViewModel.UniversityId,
                FieldId = educationalRecordViewModel.FieldId,

            };
        }
        
        public static List<EducationalRecord> ConvertEducationalVMsToEducationals(this List<EducationalRecordViewModel> educationalRecordViewModels)
        {
            return educationalRecordViewModels.Select(educationalRecordViewModel => new EducationalRecord()
            {
                Id = educationalRecordViewModel.Id,
                StartDate = educationalRecordViewModel.StartDate.ToDateTime(),
                EndDate = string.IsNullOrEmpty(educationalRecordViewModel.EndDate)
                    ? (DateTime?) null
                    : educationalRecordViewModel.EndDate.ToDateTime(),
                Desc = educationalRecordViewModel.Desc,
                UserId = educationalRecordViewModel.UserId,
                Grade = educationalRecordViewModel.Grade,
                OrientationId = educationalRecordViewModel.OrientationId,
                Average = educationalRecordViewModel.Average,
                UniversityId = educationalRecordViewModel.UniversityId,
                FieldId = educationalRecordViewModel.FieldId,

            }).ToList();
        }
        public static EducationalRecordViewModel ConvertEducationalToEducationalVM(this EducationalRecord educationalRecord)
        {

            return new EducationalRecordViewModel()
            {
                Id = educationalRecord.Id,
                StartDate = educationalRecord.StartDate.MiladiToJalali(),
                EndDate = educationalRecord.EndDate == null ? "بدون تاریخ" : Convert.ToDateTime(educationalRecord.EndDate).MiladiToJalali(),
                Desc = educationalRecord.Desc,
                UserId = educationalRecord.UserId,
                Grade = educationalRecord.Grade,
                OrientationId = educationalRecord.OrientationId,
                Average = educationalRecord.Average,
                University = educationalRecord.University.Name,
                UniversityId = educationalRecord.UniversityId,
                FieldId = educationalRecord.FieldId,
                GradeName = educationalRecord.Grade.GetDisplayName(),
                FieldName = educationalRecord.FieldFromFAndO.Name,
                OrientationName = educationalRecord.OrientationFromFAndO.Name
            };
        }
        public static List<EducationalRecordViewModel> ConvertEducationalsToEducationalVMs(this List<EducationalRecord> educationalRecords)
        {

            return educationalRecords.Select(educationalRecord => new EducationalRecordViewModel()
            {
                Id = educationalRecord.Id,
                StartDate = educationalRecord.StartDate.MiladiToJalali(),
                EndDate = educationalRecord.EndDate == null
                    ? "بدون تاریخ"
                    : Convert.ToDateTime(educationalRecord.EndDate).MiladiToJalali(),
                Desc = educationalRecord.Desc,
                UserId = educationalRecord.UserId,
                Grade = educationalRecord.Grade,
                OrientationId = educationalRecord.OrientationId,
                Average = educationalRecord.Average,
                University = educationalRecord.University.Name,
                UniversityId = educationalRecord.UniversityId,
                FieldId = educationalRecord.FieldId,
                GradeName = educationalRecord.Grade.GetDisplayName(),
                FieldName = educationalRecord.FieldFromFAndO.Name,
                OrientationName = educationalRecord.OrientationFromFAndO.Name
            }).ToList();
        }
        #endregion

        #region SkillViewModel

        public static Skill ConvertSkillViewModelToSkill(this SkillViewModel skillViewModel)
        {
            return new Skill()
            {
                UserId = skillViewModel.UserId,
                SkillType = skillViewModel.SkillType,
                Credit = skillViewModel.Credit,
                Percent = skillViewModel.Percent,
                CategoryId = skillViewModel.CategoryId,
                Id = skillViewModel.Id,
                Category = skillViewModel.Category,
                IsUpdate = skillViewModel.IsUpdate,
                Lvl1 = skillViewModel.Lvl1,
                Lvl2 = skillViewModel.Lvl2,
                Lvl3 = skillViewModel.Lvl3,
                Lvl4 = skillViewModel.Lvl4,
                LevelId = skillViewModel.LevelId,
                IsPassRatingExam = skillViewModel.IsPassRatingExam
            };
        }

        public static SkillViewModel ConvertskillToSkillViewModel(this Skill skill)
        {
            return new SkillViewModel
            {
                Id = skill.Id,
                CategoryId = skill.CategoryId,
                CategoryName = skill.Category.Title,
                Percent = skill.Percent,
                Credit = skill.Credit,
                SkillType = skill.SkillType,
                UserId = skill.UserId,
                IsUpdate = skill.IsUpdate,
                Lvl1 = skill.Lvl1,
                Lvl2 = skill.Lvl2,
                Lvl3 = skill.Lvl3,
                Lvl4 = skill.Lvl4,
                LevelId = skill.LevelId,
                LevelName = skill.Level.Name,
                IsPassRatingExam = skill.IsPassRatingExam

            };
        }
        public static List<SkillViewModel> ConvertskillsToSkillViewModels(this List<Skill> skills)
        {
            var Result = skills.Select(skill => new SkillViewModel
            {
                Id = skill.Id,
                CategoryId = skill.CategoryId,
                CategoryName = skill.Category.Title,
                Percent = skill.Percent,
                Credit = skill.Credit,
                SkillType = skill.SkillType,
                UserId = skill.UserId,
                IsUpdate = skill.IsUpdate,
                Lvl1 = skill.Lvl1,
                Lvl2 = skill.Lvl2,
                Lvl3 = skill.Lvl3,
                Lvl4 = skill.Lvl4,
                LevelId = skill.LevelId,
                LevelName = skill.Level.Name,
                IsPassRatingExam = skill.IsPassRatingExam
            }).ToList();
            return Result;
        }
        #endregion

        #region WorkSample
        public static List<WorkSample> ConvertWorkSampleVMsToWorkSamples(this List<WorkSampleViewModel> workSampleViewModels)
        {
            return workSampleViewModels.Select(workSampleViewModel=>new WorkSample
            {
                UserId = workSampleViewModel.UserId,
                Desc = workSampleViewModel.Desc,
                Title = workSampleViewModel.Title,
                Id = workSampleViewModel.Id,
                Date = workSampleViewModel.Date.ToDateTime(),
                CategoryId = workSampleViewModel.CategoryId,
                ImgAddress = workSampleViewModel.ImgAddress,
                ImgThumbnail = workSampleViewModel.ImgThumbnail

            }).ToList();
        }
        public static WorkSample ConvertWorkSampleVMToWorkSample(this WorkSampleViewModel workSampleViewModel)
        {
            return new WorkSample
            {
                UserId = workSampleViewModel.UserId,
                Desc = workSampleViewModel.Desc,
                Title = workSampleViewModel.Title,
                Id = workSampleViewModel.Id,
                Date = workSampleViewModel.Date.ToDateTime(),
                CategoryId = workSampleViewModel.CategoryId,
                ImgAddress = workSampleViewModel.ImgAddress,
                ImgThumbnail = workSampleViewModel.ImgThumbnail

            };
        }

        public static List<WorkSampleViewModel> ConvertWorkSamplesToWorkSampleVMs(this List<WorkSample> workSamples)
        {
            return workSamples.Select(workSample=>new WorkSampleViewModel
            {
                UserId = workSample.UserId,
                Desc = workSample.Desc,
                Title = workSample.Title,
                Id = workSample.Id,
                CategoryName = workSample.Category.Title,
                CategoryId = workSample.CategoryId,
                Date = workSample.Date.MiladiToJalali(),
                ImgAddress = workSample.ImgAddress,
                ImgThumbnail = workSample.ImgThumbnail
            }).ToList();
        } 

        public static WorkSampleViewModel ConvertWorkSampleToWorkSampleVM(this WorkSample workSample)
        {
            return new WorkSampleViewModel
            {
                UserId = workSample.UserId,
                Desc = workSample.Desc,
                Title = workSample.Title,
                Id = workSample.Id,
                CategoryName = workSample.Category.Title,
                CategoryId = workSample.CategoryId,
                Date = workSample.Date.MiladiToJalali(),
                ImgAddress = workSample.ImgAddress,
                ImgThumbnail = workSample.ImgThumbnail
            };
        }

        #endregion

        #region categoryLevel

        public static List<CategoryLevelViewModel> ToCategoryLevelViewModels(this List<CategoryLevel> categoryLevel)
        {
            return categoryLevel.Select(s => new CategoryLevelViewModel()
            {
                Id = s.Id,
                CategoryId = s.CategoryId,
                CategoryName = s.Category.Title,
                Min = s.Min,
                Max = s.Max,
                Title = s.Title
            }).ToList();
        }
        public static CategoryLevelViewModel ToCategoryLevelViewModel(this CategoryLevel categoryLevel)
        {
            return new CategoryLevelViewModel()
            {
                Id = categoryLevel.Id,
                CategoryId = categoryLevel.CategoryId,
                CategoryName = categoryLevel.Category.Title,
                Min = categoryLevel.Min,
                Max = categoryLevel.Max,
                Title = categoryLevel.Title
            };
        }
        public static CategoryLevel ToCategoryLevel(this CategoryLevelViewModel categoryLevelViewModel)
        {
            return new CategoryLevel()
            {
                Id = categoryLevelViewModel.Id,
                CategoryId = categoryLevelViewModel.CategoryId,
                Min = categoryLevelViewModel.Min,
                Max = categoryLevelViewModel.Max,
                Title = categoryLevelViewModel.Title
            };
        }
        #endregion

        #region Producter
        public static List<ProducterViewModel> ToProducterViewModels(this List<Producter> producters)
        {
            return producters.Select(s => new ProducterViewModel()
            {
                Id = s.Id,
                Img = s.Img,
                Description = s.Description,
                FullName = s.FullName,
                EnumType = s.ProducterTypes.FirstOrDefault().Type
            }).ToList();
        }
        public static ProducterViewModel ToProducterViewModel(this Producter producter)
        {
            return new ProducterViewModel()
            {
                Id = producter.Id,
                Img = producter.Img,
                FullName = producter.FullName,
                Description = producter.Description,
                EnumType = producter.ProducterTypes.FirstOrDefault().Type
            };
        }
        public static Producter ToProducter(this ProducterViewModel producterViewModel)
        {
            return new Producter()
            {
                Id = producterViewModel.Id,
                Img = producterViewModel.Img,
                Description = producterViewModel.Description,
                FullName = producterViewModel.FullName
            };
        }
        #endregion

        #region Post
        public static List<Post> ToPosts(this List<PostViewModel> postViewModels)
        {
            return postViewModels.Select(p => new Post()
            {
                AdsType = p.AdsType,
                CategoryId = p.CategoryId.Value,
                CDate = p.CDate,
                CommentCount = p.CommentCount,
                Desc = p.Desc,
                DisLike = p.DisLike,
                DocumentFile = p.DocumentFile,
                File = p.File,
                Id = p.Id,
                Img = p.Img,
                ImgThumbnail = p.ImgThumbnail,
                IsDeleted = p.IsDeleted,
                LevelId = p.LevelId.Value,
                Like = p.Like,
                MDate = p.MDate,
                Seen = p.Seen,
                Rate = p.Rate,
                Title = p.Title,
                Type = p.Type,
                UserId = p.UserId,
                Video = p.Video,
                VideoThumbnail = p.VideoThumbnail,
                UserCredit = p.UserCredit,
            }).ToList();
        }
        public static List<PostViewModel> ToPostViewModels(this List<Post> posts, string LoginUserId)
        {

            return posts.Select(p => new PostViewModel()
            {
                AdsType = p.AdsType,
                Category = p.Category,
                CategoryId = p.CategoryId,
                CDate = p.CDate,
                CommentCount = p.Comment != null ? p.Comment.Count() : 0,
                Desc = p.Desc,
                DisLike = p.DisLike,
                DocumentFile = p.DocumentFile,
                File = p.File,
                Id = p.Id,
                Img = p.Img,
                ImgThumbnail = p.ImgThumbnail,
                IsDeleted = p.IsDeleted,
                LevelId = p.LevelId,
                Levels = p.Levels,
                Like = p.Like,
                MDate = p.MDate,
                Seen = p.Seen,
                Rate = p.Rate,
                Title = p.Title,
                Type = p.Type,
                Users = p.Users,
                UserId = p.UserId,
                Video = p.Video,
                VideoThumbnail = p.VideoThumbnail,
                PostChangeRequests = string.IsNullOrEmpty(LoginUserId) ? null : p.PostChangeRequests?.Where(pc => pc.UserId == LoginUserId).ToList(),
                IsFavorite = string.IsNullOrEmpty(LoginUserId) ? false : p.Favorites.Any(f => f.UserId == LoginUserId),
                IsLike = string.IsNullOrEmpty(LoginUserId) ? false : p.Likes.Any(a => a.UserId == LoginUserId && a.IsLike == true),
                IsDisLike = string.IsNullOrEmpty(LoginUserId) ? false : p.Likes.Any(a => a.UserId == LoginUserId && a.IsLike == false),
            }).ToList();
        }
        public static Post ToPost(this PostViewModel postViewModel)
        {
            return new Post()
            {
                AdsType = postViewModel.AdsType,
                CategoryId = postViewModel.CategoryId.Value,
                CDate = postViewModel.CDate,
                CommentCount = postViewModel.CommentCount,
                Desc = postViewModel.Desc,
                DisLike = postViewModel.DisLike,
                DocumentFile = postViewModel.DocumentFile,
                File = postViewModel.File,
                Id = postViewModel.Id,
                Img = postViewModel.Img,
                ImgThumbnail = postViewModel.ImgThumbnail,
                IsDeleted = postViewModel.IsDeleted,
                LevelId = postViewModel.LevelId.Value,
                Like = postViewModel.Like,
                MDate = postViewModel.MDate,
                Seen = postViewModel.Seen,
                Rate = postViewModel.Rate,
                Title = postViewModel.Title,
                Type = postViewModel.Type,
                UserId = postViewModel.UserId,
                Video = postViewModel.Video,
                VideoThumbnail = postViewModel.VideoThumbnail,
                UserCredit = postViewModel.UserCredit,
            };
        }
        public static PostViewModel ToPostViewModel(this Post post, string LoginUserId)
        {
            return new PostViewModel()
            {
                AdsType = post.AdsType,
                Category = post.Category,
                CategoryId = post.CategoryId,
                CDate = post.CDate,
                CommentCount = post.Comment != null ? post.Comment.Count() : 0,
                Desc = post.Desc,
                DisLike = post.DisLike,
                DocumentFile = post.DocumentFile,
                File = post.File,
                Id = post.Id,
                Img = post.Img,
                ImgThumbnail = post.ImgThumbnail,
                IsDeleted = post.IsDeleted,
                LevelId = post.LevelId,
                Levels = post.Levels,
                Like = post.Like,
                MDate = post.MDate,
                Seen = post.Seen,
                Rate = post.Rate,
                Title = post.Title,
                Type = post.Type,
                Users = post.Users,
                UserId = post.UserId,
                Video = post.Video,
                VideoThumbnail = post.VideoThumbnail,
                PostChangeRequests = string.IsNullOrEmpty(LoginUserId) ? null : post.PostChangeRequests?.Where(pc => pc.UserId == LoginUserId).ToList(),
                IsFavorite = string.IsNullOrEmpty(LoginUserId) ? false : post.Favorites.Any(f => f.UserId == LoginUserId),
                IsLike = string.IsNullOrEmpty(LoginUserId) ? false : post.Likes.Any(a => a.UserId == LoginUserId && a.IsLike == true),
                IsDisLike = string.IsNullOrEmpty(LoginUserId) ? false : post.Likes.Any(a => a.UserId == LoginUserId && a.IsLike == false),
            };
        }

        #endregion

        //var config = new MapperConfiguration(this cfg => {
        //    cfg.CreateMap<Source, Dest>(this );
        //});

        //IMapper mapper = config.CreateMapper(this );
        //var source = new Source(this );
        //var dest = mapper.Map<Source, Dest>(this source);
    }
}
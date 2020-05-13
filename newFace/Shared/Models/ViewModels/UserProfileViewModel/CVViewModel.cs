using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels.UserProfileViewModel
{
    public class CVViewModel
    {
        public ApplicationUser User { get; set; }

        public List<JobResumeViewModel> JobResumes { get; set; } = new List<JobResumeViewModel>();

        public List<EducationalRecordViewModel> EducationalRecords = new List<EducationalRecordViewModel>();

        public List<Skill> Skills { get; set; } = new List<Skill>();

        public List<WorkSampleViewModel> WorkSamples = new List<WorkSampleViewModel>();

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.ViewModels
{
    public class UserInformationViewModel
    {
        public ApplicationUser User { get; set; }

        List<JobResume> _jobResumes = new List<JobResume>();
        public List<JobResume> JobResumesList { get => _jobResumes;set => value=_jobResumes;}

        List<EducationalRecord> _educationalRecords=new List<EducationalRecord>();
        public List<EducationalRecord> EducationalRecordList { get => _educationalRecords; set => value = _educationalRecords; }

        List<Skill> _skills=new List<Skill>();
        public List<Skill> SkillList { get => _skills; set => value = _skills; }

        List<Vision> _visions =new List<Vision>();
        public List<Vision> Visions { get => _visions; set => value = _visions; }
    }
}
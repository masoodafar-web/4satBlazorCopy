namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Education;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public partial class Video: BaseEntity
    {
      
        public string File { get; set; }

        public string VideoThumbnail { get; set; }

        public int CourseId { get; set; }

        public double Size { get; set; }

        public DateTime VideoTime { get; set; }

        // [StringLength(50)]
        [DisplayFormat(NullDisplayText = "‰«„‘Œ’")]
        public string Title { get; set; }

        public Course Courses { get; set; }

        public List<VideoSeenInfo> VideoSeenInfos { get; set; }
    }
}

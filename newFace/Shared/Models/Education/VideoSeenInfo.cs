using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Education
{
    public class VideoSeenInfo : BaseEntity
    {
        public string UserId { get; set; }

        //-----------------------------------------

        public int VideoId { get; set; }

        [ForeignKey("VideoId")]
        public Video Video { get; set; }

        //-----------------------------------------

        public bool IsComplete { get; set; }

    }
}
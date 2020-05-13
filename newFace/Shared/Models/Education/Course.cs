using newFace.Shared.Models.Resource;

namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public  class Course: BaseEntity
    {

        [Display(Name = "نمونه دوره")]
        [Required(ErrorMessage = "لطفا {0} را وارد نمایید")]
        public string SampleofCourse { get; set; }
        [Display(Name = "نام محصول")]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }

        [Display(Name = "مدرس")]
        public int? TeacherId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Producter Teacher { get; set; }

        public virtual ICollection<Video> Videos { get; set; }
    }
    public class ResultCourse : Result
    {
        public Course Course { get; set; }

        public List<Course> Courses { get; set; }

        public List<Video> Videos { get; set; }
    }
}

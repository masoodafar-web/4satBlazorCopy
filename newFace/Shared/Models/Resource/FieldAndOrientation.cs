using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Resource
{
    public class FieldAndOrientation: BaseEntity
    {

        [Display(Name = "نام")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string Name { get; set; }
        public int? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public FieldAndOrientation fieldAndOrientation { get; set; }

        [InverseProperty("FieldFromFAndO")]
        public virtual List<EducationalRecord> EducationalRecordsfield { get; set; }
        [InverseProperty("OrientationFromFAndO")]
        public virtual List<EducationalRecord> EducationalRecordsOrention { get; set; }

    }
    public class FieldAndOrientationVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
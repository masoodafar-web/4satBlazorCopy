
namespace newFace.Shared.Models
{
    using newFace.Shared.Models.Resource;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class EducationalRecord: BaseEntity
    {
  
        public int FieldId { get; set; }
        [ForeignKey("FieldId")]
        public virtual FieldAndOrientation FieldFromFAndO { get; set; }

        
        public int OrientationId { get; set; }
        [ForeignKey("OrientationId")]
        public virtual FieldAndOrientation OrientationFromFAndO { get; set; }

        public int UniversityId { get; set; }
        [JsonIgnore]
        [ForeignKey("UniversityId")]
        public virtual Resources University { get; set; }

        public double? Average { get; set; }

        
        public DateTime StartDate { get; set; }

        
        public DateTime? EndDate { get; set; }

        public Enums.EducationalStatus Grade { get; set; }
        public string Desc { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual ApplicationUser Users { get; set; }
    }
}

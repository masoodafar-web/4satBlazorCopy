using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class City: BaseEntity
    {
  
        [Display(Name = "نام شهر")]
        public string Name { get; set; }
        //----------------------------------------------------------------------------------------------------
        //ForeignKey
        [Display(Name = "نام استان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int ProvinceId { get; set; }

        [JsonIgnore]
        [ForeignKey("ProvinceId")]
        public virtual Province Province { get; set; }
        //----------------------------------------------------------------------------------------------------

    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class Province: BaseEntity
    {

        [Display(Name = "نام استان")]
        public string Name { get; set; }
        //----------------------------------------------------------------------------------------------------
        //ForeignKey

        [Display(Name = "نام کشور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public int CountryId { get; set; }

        [JsonIgnore]
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        //----------------------------------------------------------------------------------------------------

        [JsonIgnore]
        public List<City> Cities { get; set; }
    }
}
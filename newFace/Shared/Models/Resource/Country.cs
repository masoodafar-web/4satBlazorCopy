using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace newFace.Shared.Models.Resource
{
    public class Country: BaseEntity
    {

        [Display(Name = "نام کشور")]
        public string Name { get; set; }
        //----------------------------------------------------------------------------------------------------

        [JsonIgnore]
        public List<Province> Provinces { get; set; }


    }
}
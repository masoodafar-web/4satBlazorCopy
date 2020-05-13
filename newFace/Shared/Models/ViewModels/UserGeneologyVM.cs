using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Financial;

namespace newFace.Shared.Models.ViewModels
{
    public class UserGeneologyVM
    {
        public int Id { get; set; }
        [Display(Name = "نام کاربر")]
        public string Current_User_Name { get; set; }
        [Display(Name = "نام کاربر پدر")]
        public string Parent_Name { get; set; }
        [Display(Name = "نوع ژنولوژی")]
        public string GeneologyType { get; set; }
    }
}
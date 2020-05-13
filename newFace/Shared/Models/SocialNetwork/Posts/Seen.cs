using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models
{
    public class Seen: BaseEntity
    {

        public string UserId { get; set; }
        //----------------------------------------------------------------------------------------------------
        public int PostId { get; set; }
        //----------------------------------------------------------------------------------------------------
        public DateTime Date { get; set; }                                                               

    }
}
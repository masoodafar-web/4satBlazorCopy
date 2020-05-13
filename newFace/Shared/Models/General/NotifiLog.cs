using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.General
{
    public class NotifiLog : BaseEntity
    {
        public NotifiLog()
        {
            Date = DateTime.Now;
        }

        public int NotifiId { get; set; }
        //--------------------------------------------------------
        public DateTime Date { get; set; }
        //--------------------------------------------------------
        public string Result { get; set; }

    }
}
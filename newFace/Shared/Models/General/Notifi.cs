using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using newFace.Shared.Models.Resource;
using Newtonsoft.Json;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Models.General
{
    public class Notifi : BaseEntity
    {
        public Notifi()
        {
            Date = DateTime.Now;
        }

        [Display(Name = "عنوان")]
        public string Title { get; set; }
        //-------------------------------------------------------

        [Display(Name = "متن پیام")]
        public string Text { get; set; }
        //--------------------------------------------------------
        public string ReceiverId { get; set; }

        [JsonIgnore]
        [ForeignKey("ReceiverId")]
        public ApplicationUser Receiver { get; set; }
        //--------------------------------------------------------
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public ApplicationUser Sender { get; set; }
        //--------------------------------------------------------
        public int? CommentId { get; set; }
        //--------------------------------------------------------
        public int? PostId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
        //--------------------------------------------------------
        public NotifiType NotifiType { get; set; }
        //--------------------------------------------------------
        public DateTime Date { get; set; }

    }
}
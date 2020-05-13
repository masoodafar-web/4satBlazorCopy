namespace newFace.Shared.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Chat: BaseEntity
    {
        public Chat()
        {
            Date = DateTime.Now;
        }
  
        [Display(Name = "متن پیام")]
        public string Text { get; set; }
        //--------------------------------------------------------
        public string SenderId { get; set; }

        [ForeignKey("SenderId")]
        public virtual ApplicationUser Sender { get; set; }
        //--------------------------------------------------------
        public string ReceiverId { get; set; }

        [ForeignKey("ReceiverId")]
        public virtual ApplicationUser Receiver { get; set; }
        //--------------------------------------------------------
        public DateTime Date { get; set; }
        //--------------------------------------------------------
        public string File { get; set; }

        public string FileType { get; set; }

        public string FileName { get; set; }

        public string VideoThumbnail { get; set; }

        public string ImageThumbnail { get; set; }

        public long FileSize { get; set; }
        //--------------------------------------------------------
        public bool Seen { get; set; }
        //--------------------------------------------------------
        public bool IsDeleted { get; set; }

        public DateTime? DateIsDeleted { get; set; }
        //--------------------------------------------------------
        public int? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public virtual Chat ParentChat { get; set; }
    }




}

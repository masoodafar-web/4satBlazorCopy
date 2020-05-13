namespace newFace.Shared.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ChatContact : BaseEntity
    {
        public ChatContact()
        {
            UpdateDate = DateTime.Now;
        }

        //--------------------------------------------------------
        public string UserId { get; set; }

        //[ForeignKey("UserId")]
        //public ApplicationUser User { get; set; }
        //--------------------------------------------------------
        public string ContactId { get; set; }

        [ForeignKey("ContactId")]
        public virtual ApplicationUser Contact { get; set; }
        //--------------------------------------------------------
        public int UnSeenCount { get; set; }
        //--------------------------------------------------------
        public int MyProperty { get; set; }
        //--------------------------------------------------------
        public DateTime UpdateDate { get; set; }
        //--------------------------------------------------------
        public int? ChatId { get; set; }

        [ForeignKey("ChatId")]
        public virtual Chat Chat { get; set; }
    }




}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace newFace.Shared.Models.Education
{
    public class Favorite : BaseEntity
    {
        //-------------------------------------------------
        public FavedType FavedType { get; set; }
        //-------------------------------------------------
        public string UserId { get; set; }
        //-------------------------------------------------
        public int? ProductFavedId { get; set; }

        [ForeignKey("ProductFavedId")]
        public Product Product { get; set; }
        //-------------------------------------------------
        public string UserFavedId { get; set; }

        [ForeignKey("UserFavedId")]
        public ApplicationUser User { get; set; }
        //-------------------------------------------------
        public int? PostFavedId { get; set; }

        [ForeignKey("PostFavedId")]
        public Post Post { get; set; }
        //-------------------------------------------------
        public DateTime DateFaved =DateTime.Now;
        //-------------------------------------------------

    }

    public enum FavedType
    {
        //0
        Post,
        //1
        User,
        //2
        Product
    }
}
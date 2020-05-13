namespace newFace.Shared.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    public partial class Cat_Post: BaseEntity
    {

        public int PostId { get; set; }

        public int CatId { get; set; }

        public DateTime Date { get; set; }

        public virtual Category Category { get; set; }

        public virtual Post Post { get; set; }
    }
}

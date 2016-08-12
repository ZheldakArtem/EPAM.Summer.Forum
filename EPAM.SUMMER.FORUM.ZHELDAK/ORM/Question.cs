namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Question
    {

        public Question()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Column("Question")]
        public string Question1 { get; set; }

        public DateTime? DateOfQuestion { get; set; }

        public int? UserId { get; set; }

        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual User User { get; set; }
    }
}

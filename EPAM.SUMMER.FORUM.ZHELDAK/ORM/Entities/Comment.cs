using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    public class Comment
    {
        public int Id { get; set; }

        [Column("Comment")]
        public string Comment_ { get; set; }

        public DateTime? DataOfComment { get; set; }

        public int? UserId { get; set; }

        public int? QuestionId { get; set; }

        public bool IsRight { get; set; }

        public virtual Question Question { get; set; }

        public virtual User User { get; set; }
    }
}

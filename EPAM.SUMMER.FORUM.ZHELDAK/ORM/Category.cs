using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    public partial class Category
    {
        public Category()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace ORM
{
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Questions = new HashSet<Question>();
            Roles = new HashSet<Role>();
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        public DateTime? Birthday { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}

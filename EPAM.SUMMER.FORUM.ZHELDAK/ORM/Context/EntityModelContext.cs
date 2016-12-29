using System;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ORM
{
    public partial class EntityModelContext : DbContext
    {
        public EntityModelContext()
            : base("name=EntityModel")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Questions)
                .WithOptional(e => e.Category)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Question>()
                .HasMany(e => e.Comments)
                .WithOptional(e => e.Question)
                .WillCascadeOnDelete();

            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithMany(e => e.Roles)
                .Map(m => m.ToTable("UsersRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<User>()
                .Property(e => e.MimeType)
                .IsUnicode(false);
        }
    }
}

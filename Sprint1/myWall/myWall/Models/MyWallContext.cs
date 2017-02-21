namespace myWall.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyWallContext : DbContext
    {
        public MyWallContext()
            : base("name=MyWallContext")
        {
        }

        public virtual DbSet<Annotation> Annotations { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<CalloborationAccount> CalloborationAccounts { get; set; }
        public virtual DbSet<CalloborationCenter> CalloborationCenters { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Diagram> Diagrams { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Wall> Walls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Diagrams)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Walls)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Groups)
                .WithMany(e => e.AspNetUsers)
                .Map(m => m.ToTable("User_Group").MapLeftKey("UserId").MapRightKey("GroupId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.CalloborationAccounts)
                .WithMany(e => e.AspNetUsers)
                .Map(m => m.ToTable("UserCalloboration").MapLeftKey("UserId").MapRightKey("CallobId"));

            modelBuilder.Entity<CalloborationAccount>()
                .HasMany(e => e.CalloborationCenters)
                .WithRequired(e => e.CalloborationAccount)
                .HasForeignKey(e => e.CallobId);

            modelBuilder.Entity<CalloborationCenter>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.CalloborationCenter)
                .HasForeignKey(e => e.CallobId);

            modelBuilder.Entity<Diagram>()
                .HasMany(e => e.Posts)
                .WithMany(e => e.Diagrams)
                .Map(m => m.ToTable("Post_Diagram").MapLeftKey("DiagramId").MapRightKey("PostId"));

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Walls)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("Wall_Group").MapLeftKey("GroupId").MapRightKey("WallId"));

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Answers)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Post)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("Post_Tag").MapLeftKey("PostId").MapRightKey("TagId"));

            modelBuilder.Entity<Wall>()
                .HasMany(e => e.Posts)
                .WithRequired(e => e.Wall)
                .WillCascadeOnDelete(false);
        }
    }
}

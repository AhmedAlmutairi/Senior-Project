namespace myWall.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class MyWallContext : DbContext
    {
        public MyWallContext()
            : base("name=SQLAzureConnection")
        {
        }

        public virtual DbSet<Annotation> Annotations { get; set; }
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<CalloborationAccount> CalloborationAccounts { get; set; }
        public virtual DbSet<CalloborationCenter> CalloborationCenters { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Diagram> Diagrams { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<User_Group> User_Group { get; set; }
        public virtual DbSet<UserCalloboration> UserCalloborations { get; set; }
        public virtual DbSet<Wall> Walls { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CalloborationAccount>()
                .HasMany(e => e.CalloborationCenters)
                .WithRequired(e => e.CalloborationAccount)
                .HasForeignKey(e => e.CallobId);

            modelBuilder.Entity<CalloborationAccount>()
                .HasMany(e => e.UserCalloborations)
                .WithRequired(e => e.CalloborationAccount)
                .HasForeignKey(e => e.CallobId);

            modelBuilder.Entity<Chat>()
                .Property(e => e.File)
                .IsFixedLength();

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

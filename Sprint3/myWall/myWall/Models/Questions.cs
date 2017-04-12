namespace myWall
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Questions : DbContext
    {
        public Questions()
            : base("name=Questions")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
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
        public virtual DbSet<User_Group> User_Group { get; set; }
        public virtual DbSet<UserCalloboration> UserCalloborations { get; set; }
        public virtual DbSet<Wall> Walls { get; set; }
        public virtual DbSet<Questions> Question { get; set; }


        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.Walls)
                .WithOptional(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<CalloborationAccount>()
                .HasMany(e => e.CalloborationCenters)
                .WithOptional(e => e.CalloborationAccount)
                .HasForeignKey(e => e.CalloborationAccount_Id);

            modelBuilder.Entity<CalloborationAccount>()
                .HasMany(e => e.UserCalloborations)
                .WithOptional(e => e.CalloborationAccount)
                .HasForeignKey(e => e.CalloborationAccount_Id);

            modelBuilder.Entity<CalloborationCenter>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.CalloborationCenter)
                .HasForeignKey(e => e.CalloborationCenter_Id);

            modelBuilder.Entity<Diagram>()
                .HasMany(e => e.Posts)
                .WithMany(e => e.Diagrams)
                .Map(m => m.ToTable("DiagramPosts"));

            modelBuilder.Entity<Group>()
                .HasMany(e => e.Walls)
                .WithMany(e => e.Groups)
                .Map(m => m.ToTable("GroupWalls"));

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("TagPosts"));
        }
    }
}

namespace FakeNews
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HeadlineDB : DbContext
    {
        public HeadlineDB()
            : base("name=HeadlineDB")
        {
        }

        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Headline> Headlines { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}

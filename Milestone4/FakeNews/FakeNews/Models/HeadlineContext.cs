namespace FakeNews.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class HeadlineContext : DbContext
    {
        public HeadlineContext()
            : base("name=HeadlineContext")
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

using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace myWall.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Walls = new HashSet<Wall>();
            Posts = new HashSet<Post>();
            
        }

        


        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string Question { get; set; }
        public string Hint { get; set; }
        public string Answer { get; set; }

        public virtual ICollection<Wall> Walls { get; set; }
        public virtual ICollection<Post> Posts { get; set; }

       
    }

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        


        public ApplicationDbContext()
            : base("SQLAzureConnection", throwIfV1Schema: false)
            
        {
            Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Wall> Walls { get; set; }
        //public IEnumerable<object> Posts { get; internal set; }

         public virtual DbSet<Post> Posts { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().
                HasMany(m => m.Walls).WithOptional(m => m.AspNetUsers).
                HasForeignKey(p => p.UserId);

           // modelBuilder.Entity<ApplicationUser>().
             //   HasMany(m => m.Posts).WithOptional(m => m.AspNetUsers).
               // HasForeignKey(p => p.UserId);



        }
    }
}
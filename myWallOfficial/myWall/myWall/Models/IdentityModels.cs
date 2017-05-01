using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System;
using System.Diagnostics;
using System.Data.Entity.Infrastructure;
using System.Text;

namespace myWall.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            Walls = new HashSet<Wall>();
            Posts = new HashSet<Post>();
           // Chats = new HashSet<Chat>();
            
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

        public string ConnectionId { get; set; }

        public virtual ICollection<Wall> Walls { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
       // public virtual ICollection<Chat> Chats { get; set; }


    }

    

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            
            catch (DbUpdateException dbu)
            {
                var exception = HandleDbUpdateException(dbu);
                throw exception;
            }
        }

        private Exception HandleDbUpdateException(DbUpdateException dbu)
        {
            var builder = new StringBuilder("A DbUpdateException was caught while saving changes. ");

            try
            {
                foreach (var result in dbu.Entries)
                {
                    builder.AppendFormat("Type: {0} was part of the problem. ", result.Entity.GetType().Name);
                }
            }
            catch (Exception e)
            {
                builder.Append("Error parsing DbUpdateException: " + e.ToString());
            }

            string message = builder.ToString();
            return new Exception(message, dbu);
        }

        public ApplicationDbContext()

           : base("SQLAzureConnection", throwIfV1Schema: false)
        //SQLAzureConnection

         // : base("MyWallContext", throwIfV1Schema: false)
            

        {
            //Database.SetInitializer<ApplicationDbContext>(null);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


        public virtual DbSet<Wall> Walls { get; set; }
     

         public virtual DbSet<Post> Posts { get; set; }

         public virtual DbSet<Chat> Chats { get; set; }



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
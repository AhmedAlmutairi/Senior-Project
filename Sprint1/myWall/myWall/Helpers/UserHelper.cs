using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myWall.Models;
using myWall.UserRepository;

namespace myWall.Helpers
{
    public static class UserHelper
    {
        //ApplicationDbContext userContext;
        public static IRepository GetUserRepository()
        {
            return new UserRepository.UserRepository();
        }

        public static List<ApplicationUser> UserLookup(string nameOrEmail)
        {
            ApplicationDbContext userContext = new ApplicationDbContext();

            RegexUtilities ru = new RegexUtilities();
            List<ApplicationUser> users = new List<ApplicationUser>();


            //if (ru.IsValidEmail(nameOrEmail))
            //{
            //    users.AddRange(userContext.ApplicationUsers.Where(u => u.Email.Equals(nameOrEmail, StringComparison.CurrentCultureIgnoreCase)).ToList());
            //}
            //else
            //{
            //    users.AddRange(userContext.ApplicationUsers.Where(u => u.UserName.Equals(nameOrEmail, StringComparison.CurrentCultureIgnoreCase)).ToList());
            //}

            return users;
        }
    }
}
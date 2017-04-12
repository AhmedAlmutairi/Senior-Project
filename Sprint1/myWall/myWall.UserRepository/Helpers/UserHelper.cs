using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myWall.Model.EFModel;

namespace myWall.UserRepository.Helpers
{
    public static class UserHelper
    {
        //ApplicationDbContext userContext;


        public static List<AspNetUser> UserLookup(string nameOrEmail)
        {
            UsersContext userContext = new UsersContext();

            RegexUtilities ru = new RegexUtilities();
            List<AspNetUser> users = new List<AspNetUser>();


            if (ru.IsValidEmail(nameOrEmail))
            {
                users.AddRange(userContext.AspNetUsers.Where(u => u.Email.Equals(nameOrEmail, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }
            else
            {
                users.AddRange(userContext.AspNetUsers.Where(u => u.UserName.Equals(nameOrEmail, StringComparison.CurrentCultureIgnoreCase)).ToList());
            }

            return users;
        }
    }
}
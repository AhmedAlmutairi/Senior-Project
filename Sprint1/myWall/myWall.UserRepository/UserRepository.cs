using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myWall.Model.EFModel;
using myWall.UserRepository.Helpers;

namespace myWall.UserRepository
{
    public class UserRepository : IRepository
    {
        public List<AspNetUser> LookupUser(string nameOrEmail)
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
                users.AddRange(userContext.AspNetUsers.Where(u => u.UserName.Contains(nameOrEmail)).ToList());
            }

            return users;
        }
    }
}

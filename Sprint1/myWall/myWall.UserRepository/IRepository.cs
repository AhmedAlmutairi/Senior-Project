using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using myWall.Model.EFModel;

namespace myWall.UserRepository
{
    public interface IRepository
    {
        List<AspNetUser> LookupUser(string nameOrEmail);


    }
}

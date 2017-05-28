using myWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myWall;

namespace myWall.ViewModel
{
    public class WallandUserVM
    {
        public IEnumerable<ApplicationUser> Users { get; set; }
        public IEnumerable<Wall> Walls { get; set; }
    }
}
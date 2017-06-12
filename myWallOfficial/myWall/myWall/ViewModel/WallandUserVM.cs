using myWall.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using myWall;
using PagedList;
using PagedList.Mvc;


namespace myWall.ViewModel
{
    public class WallandUserVM
    {
        public IPagedList<ApplicationUser> Users { get; set; }
        public IPagedList<Wall> Walls { get; set; }
    }
}
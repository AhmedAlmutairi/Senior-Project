using myWall.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace myWall.Models
{
    public class Combinewallpost
    {
        public IEnumerable<Wall> wall { get; set; }
        public IEnumerable<ContentViewModel> content { get; set; }
}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace myWall
{
        public class DrawHub : Hub
        {
            public DrawHub()
            {

            }
            public void DrawSomeThings(int x, int y, int z, string w)
            {
                Clients.Others.draw(x, y, z, w);
            }

            public void ClearCanvas()
            {
                Clients.All.clearCanvas();
            }
        }


    }

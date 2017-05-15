using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRHtml5.Hubs
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace myWall
{
    [HubName("drawHub")]
    public class DrawHub : Hub
    {
        public void Move(int prevX, int prevY, int currX, int currY)
        {
            Clients.All.DrawFromHub(prevX, prevY, currX, currY);
        }
        public void DrawFromHub()
        {
            // Will be fired in 
        }
    }
}

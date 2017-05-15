using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace myWall
{
    public class whiteboardHub : Hub
    {
        public void Send(string name, string message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(name, message);
        }

        public void DrawShape(int shape, string fillColor, string color,
                int size, int x, int y, int x1, int y1)
        {
            Clients.All.Draw(shape, fillColor, color, size, x, y, x1, y1);
        }
    }
}



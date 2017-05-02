using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using myWall.Models;
using System.Collections;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace myWall
{
    public class ChatHub : Hub
    {
       /* public override System.Threading.Tasks.Task OnConnected()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string userName = Context.User.Identity.Name;

            var allUsers = db.Users.ToList();
            var messages = db.Chats.ToList();
            Clients.AllExcept(userName).onNewUserConnected(userName);
            return Clients.Caller.connected(userName, allUsers, messages);

            
        }*/

        


        public void SendMessageToAll(string UserName, string message)
        {
            ApplicationDbContext dc = new ApplicationDbContext();
            UserName = Context.User.Identity.Name;
            //var currentmsg = dc.Chats.Select(m => m.Message).ToList();
            // store last 100 messages in cache
            AddAllMessageinCache(UserName, message);

            // Broad cast message
            Clients.All.NewMessage(UserName, message);
        }

        private void AddAllMessageinCache(string UserName, string message)
        {

           var userId = Context.User.Identity.GetUserId();
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {

                var messageDetail = new Chat
                {
                    
                    UserId = userId,
                    userName = UserName,
                    Message = message
                    
                    
                };


                
                    dc.Chats.Add(messageDetail);
                    //dc.Entry(messageDetail).State = EntityState.Modified;
                    dc.SaveChanges();
                    
                    
               
            }
        }

    }
}
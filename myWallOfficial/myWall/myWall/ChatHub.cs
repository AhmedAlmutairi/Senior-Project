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
        MyWallDB DB = new MyWallDB();
        /*public void LetsChat(string Cl_Name, string Cl_Message)
        {
            Cl_Name = Context.User.Identity.Name;
            Clients.All.NewMessage(Cl_Name, Cl_Message);
        }*/


        /*public void Connect(string userName, string userId)
        {
            //emailIDLoaded = email;
            //var id = Context.ConnectionId;
            userName = Context.User.Identity.Name;
            userId = Context.User.Identity.GetUserId();
            using (MyWallDB dc = new MyWallDB())
            {
                var item = dc.Chats.FirstOrDefault(x => x.userName == userName);
                if (item != null)
                {
                    dc.Chats.Remove(item);
                    dc.SaveChanges();

                    // Disconnect
                    Clients.All.onUserDisconnectedExisting(item.userName);
                }

                var users = dc.Chats.ToList();
                //var context = new ApplicationDbContext();
                if (users.Where(x => x.userName == userName).ToList().Count == 0)
                {
                    var userdetails = new Chat 
                    {
                        //ConnectionId = id,
                        userName = userName,
                        
                    };
                    dc.Chats.Add(userdetails);
                    dc.SaveChanges();

                    // send to caller
                    var connectedUsers = dc.Chats.ToList();
                    var CurrentMessage = dc.Chats.ToList();
                    Clients.Caller.onConnected(userName, connectedUsers, CurrentMessage);
                }

                // send to all except caller client
                Clients.AllExcept(userName).onNewUserConnected(userName);
            }
        }*/

        public void SendMessageToAll(string UserName, string message)
        {
            UserName = Context.User.Identity.Name;
            
            // store last 100 messages in cache
            AddAllMessageinCache(UserName, message);

            // Broad cast message
            Clients.All.NewMessage(UserName, message);
        }

        private void AddAllMessageinCache(string UserName, string message)
        {
            //UserName = Context.User.Identity.Name;
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var messageDetail = new Chat
                {
                   
                    userName = UserName,
                    Message = message
                    
                    
                };


                //try
                //{
                    dc.Chats.Add(messageDetail);
                    dc.Entry(messageDetail).State = EntityState.Modified;
                    dc.SaveChanges();
                    
                    
                    
                //}

                /*catch (Exception ec)
                {
                    Console.WriteLine(ec.Message);
                }*/
            }
        }

    }
}
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using myWall.Models;




namespace myWall
{
    public class ChatHub : Hub
    {

        public override System.Threading.Tasks.Task OnConnected()
        {
            // TODO Refactor this call to a class instance
            ApplicationDbContext db = new ApplicationDbContext();
            string userName = Context.User.Identity.Name;
            //return null;

            //var url = Clients.Caller.getRoom();
            //var room = ParseUrlForRoom(url);
            //// var allUsers = db.Users.ToList();
            var messages = db.Chats.ToList();
            //var messages = GetMessageCache(room);
            //// var wall = db.Walls.ToList();
            ////Clients.AllExcept(userName).onNewUserConnected(userName);

            return Clients.Caller.connected(userName, messages);
            //return Clients.Caller.connected(userName);


        }

        public System.Threading.Tasks.Task OnConnected(string room)
        {
            return OnConnected();
        }

        public string[] GetMessageCache(string room)
        {
            room = ParseUrlForRoom(room);
            int id;
            var parse = int.TryParse(room, out id);

            IQueryable<Chat> messages = null;

            if (id > 0)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                messages = from c in db.Chats
                           where c.WallId == id
                           select c;
            }

            Chat[] chat_array = messages.ToArray<Chat>();
            string[] msg_array = new string[chat_array.Count()];
            for (int i = 0; i < chat_array.Count(); i++)
            {
                msg_array[i] = chat_array[i].Message;
            }

            return msg_array;

        }

        public void SendMessageToAll(string UserName, string message)
        {
            // TODO Refactor this call to a class instance
            ApplicationDbContext dc = new ApplicationDbContext();
            UserName = Context.User.Identity.Name;

            AddMessageToCache(UserName, message);

            // Broad cast message
            Clients.All.NewMessage(UserName, message);
        }

        private void AddMessageToCache(string UserName, string message)
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
                dc.SaveChanges();
            }
        }

        private void AddMessageToCache(string UserName, string message, int? room)
        {
            var userId = Context.User.Identity.GetUserId();
            using (ApplicationDbContext dc = new ApplicationDbContext())
            {
                var time = System.DateTime.Now;
                var messageDetail = new Chat
                {

                    UserId = userId,
                    userName = UserName,
                    Message = message,
                    WallId = room,

                };
                dc.Chats.Add(messageDetail);
                dc.SaveChanges();
            }
        }

        public async Task JoinRoom(string room)
        {
            room = ParseUrlForRoom(room);
            await Groups.Add(Context.ConnectionId, room);
            Clients.Group(room).addChatMessage(Context.User.Identity.Name + " Joined Channel");

        }

        public async Task LeaveRoom(string room)
        {
            room = ParseUrlForRoom(room);
            try
            {
                await Groups.Remove(Context.ConnectionId, room);
                Clients.Group(room).addChatMessage(Context.User.Identity.Name + "Left Channel");
            }
            catch (TaskCanceledException)
            {

            }
        }

        public Task SendMessageToGroup(string room, string user, string message)
        {
            // TODO Refactor this call to a class instance
            ApplicationDbContext db = new ApplicationDbContext();
            //room = db.Walls.Select(x => x.Id).ToList().First().ToString();
            room = ParseUrlForRoom(room);
            int id;
            var parse = int.TryParse(room, out id);
            user = Context.User.Identity.Name;
            Wall wall = null;
            if (id > 0)
            {
                var walls = from w in db.Walls
                            where w.Id == id
                            select w;
                wall = walls.First();
            }

            if (wall == null)
            {
                AddMessageToCache(user, message);
            }
            else
            {
                AddMessageToCache(user, message, wall.Id);
            }
            //Clients.All.NewMessage(user, message);
            return Clients.Group(room).addChatMessage(user, message);
        }

        private string ParseUrlForRoom(string url)
        {
            var list = url.Split('/').ToList();
            var room = list.Last();
            return room;
        }
    }
}

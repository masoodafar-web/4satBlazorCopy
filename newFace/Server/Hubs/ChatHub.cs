using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using newFace.Server.Data;
using newFace.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using newFace.Server.Push;
using Microsoft.EntityFrameworkCore;

namespace newFace.Server.Hubs
{
    public class ChatHub : Hub
    {
        private readonly newFaceDbContext _db = new newFaceDbContext();
        FireBaseNotification _notification = new FireBaseNotification();

        public Chat SendPrivateMessage(string fromUserId, string toUserId, string message ,int? parentId)
        {
            try
            {

                if (string.IsNullOrEmpty(fromUserId))
                {
                    Clients.Group("users/" + fromUserId).onNewPrivateMessage("کاربر فرستنده دریافت نشد");
                }
                if (string.IsNullOrEmpty(toUserId))
                {
                    Clients.Group("users/" + fromUserId).onNewPrivateMessage("کاربر گیرنده دریافت نشد");
                }
                if (string.IsNullOrEmpty(message))
                {
                    Clients.Group("users/" + fromUserId).onNewPrivateMessage("متن پیام دریافت نشد");
                }

                Chat chat = new Chat
                {
                    ReceiverId = toUserId,
                    SenderId = fromUserId,
                    Text = message,
                    ParentId = parentId
                };
                _db.Chats.Add(chat);

                var contact = _db.ChatContacts.FirstOrDefault(c => c.UserId == toUserId && c.ContactId == fromUserId);
                if (contact == null)
                {
                    ChatContact chatContact1 = new ChatContact
                    {
                        UserId = toUserId,
                        ContactId = fromUserId,
                        ChatId = chat.Id,
                        UnSeenCount = 1
                    };
                    _db.ChatContacts.Add(chatContact1);

                    ChatContact chatContact2 = new ChatContact
                    {
                        ChatId = chat.Id,
                        UserId = fromUserId,
                        ContactId = toUserId,
                    };
                    _db.ChatContacts.Add(chatContact2);
                }
                else
                {
                    ++contact.UnSeenCount;
                    contact.ChatId = chat.Id;
                    contact.UpdateDate = DateTime.Now;
                    _db.Entry(contact).State = EntityState.Modified;

                    var contact2 = _db.ChatContacts.FirstOrDefault(c => c.UserId == fromUserId && c.ContactId == toUserId);
                    if (contact2 != null)
                    {
                        contact2.ChatId = chat.Id;
                        contact2.UpdateDate = DateTime.Now;
                        _db.Entry(contact2).State = EntityState.Modified;
                    }
                }

                if (_db.SaveChanges() > 0)
                {
                    Clients.Group("users/" + toUserId).onNewPrivateMessage("", chat.Id, chat.SenderId);



                    _notification.SendByUserId(toUserId, "", chat.Id, FireBaseNotification.NotifiType.ChatNewMessage);


                    return chat;
                }
                else
                {
                    Clients.Group("users/" + fromUserId).onNewPrivateMessage("خطایی رخ داده است");
                }
            }
            catch (Exception e)
            {
                Clients.Group("users/" + fromUserId).onNewPrivateMessage(e.InnerException.Message);
            }
            return null;
        }

        public void IsDeleted(int chatId)
        {
            Clients.All().isDeleted(chatId);
        }

        [Authorize]
        public override Task OnConnected()
        {
            string userId = Context.User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                Groups.Add(Context.ConnectionId, "users/" + userId);
            }

            //لاگ اتصلات
            //_db.ChatLogs.Add(new ChatLog { ContextConnectionId = Context.ConnectionId, UserId = userId, IsConnected = true });
            //_db.SaveChanges();

            //آنلاین آفلاین
            //Clients.All.connection(userId, 1);

            return base.OnConnected();
        }



















        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    string userId = Context.User.Identity.GetUserId();
        //    Clients.All.connection(userId, 0);

        //    _db.ChatLogs.Add(new ChatLog { ContextConnectionId = Context.ConnectionId, UserId = userId, IsDisconnected = true });
        //    _db.SaveChanges();

        //    return base.OnDisconnected(stopCalled);
        //}


        //public override Task OnReconnected()
        //{
        //    string userId = Context.User.Identity.GetUserId();
        //    Clients.All.connection(userId, 1);

        //    _db.ChatLogs.Add(new ChatLog { ContextConnectionId = Context.ConnectionId, UserId = userId, IsReConnected = true });
        //    _db.SaveChanges();

        //    return base.OnReconnected();
        //}

    }
}
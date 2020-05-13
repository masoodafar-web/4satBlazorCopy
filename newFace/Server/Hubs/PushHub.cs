using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using newFace.Server.Data;
using newFace.Shared.Models;
using newFace.Shared.Models.General;
using newFace.Shared.Models.Resource;
using newFace.Shared.Repositories.Generic;
using newFace.Shared.Repository.Push;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Server.Push
{
    public class FireBaseNotification: IFireBaseNotification
    {
        //private readonly newFaceDbContext _db = new newFaceDbContext();
        private UserManager<ApplicationUser> UserManager;
        private IUnitOfWork _unitOfWork;

        public FireBaseNotification(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }
        readonly string _applicationId = "AAAAlzYNMj4:APA91bE17q4a5CcYzpzeixsFdmMbMzvacmbUKrdDntKsznjIufHZ2EtAUgXoErCO6mNx7gPpuKvnoMUpP-2krlgpWMFGwP07HZKKoey8ur5UDpuK6jVgujEkG7V_sNtNxN_cFy_OuJ5O";



        public bool SendByUserId(string userId, string senderId, int? objectId , NotifiType notifiType)
        {
            try
            {
                string title = "";
                string title2 = "";
                string body = "";
                string img = "";
                int? postId = null;
                int? commentId = null;

                ApplicationUser sender = new ApplicationUser();

                var userPushTokens = _unitOfWork.UserPushTokenGR.FindBy(r => r.UserId == userId).ToList();

                if (userPushTokens.Any())
                {

                    switch (notifiType)
                    {
                        case Enums.NotifiType.ChatNewMessage:

                            var chat = _unitOfWork.ChatGR.FirstOrDefault(c => c.Id == objectId);

                            if (chat != null)
                            {
                                sender = chat.Sender;
                                title = " پیام از " + chat.Sender.FullName;
                                body = chat.Text;
                                if (chat.FileType == "Image")
                                {
                                    img = chat.ImageThumbnail;
                                }
                                else if (chat.FileType == "Video")
                                {
                                    img = chat.VideoThumbnail;
                                }
                                else if (!string.IsNullOrEmpty( chat.FileType ))
                                {
                                    body = "یک فایل برای شما فرستادم";
                                }

                                title2 = title + body;

                            }

                            break;
                        case Enums.NotifiType.PostComment:

                            var comment = _unitOfWork.CommentGR.FirstOrDefault(c => c.Id == objectId);

                            if (comment != null)
                            {
                                commentId = objectId;

                                if (comment.ParentId != null)
                                {
                                    userId = comment.CommentParent.UserId;
                                }

                                postId = comment.PostId;

                                sender = comment.User;
                                title2 = " نظر داد " + comment.Desc;

                                title = " نظر از " + comment.User.FullName;
                                body = comment.Desc;

                            }

                            break;

                        case Enums.NotifiType.Postlike:

                            var like = _unitOfWork.LikeGR.GetSingleIncluding(c => c.Id == objectId, l => l.Users);

                            if (like != null)
                            {
                                postId = like.PostId;
                                sender = like.Users;

                                if (like.IsLike)
                                {
                                    title2 = " پست شما را پسندید ";
                                    title = like.Users.FullName + " پست شما را پسندید ";
                                }
                                else
                                {
                                    title2 = " پست شما را نپسندید ";
                                    title = like.Users.FullName + " پست شما را نپسندید ";
                                }
                            }

                            break;
                        case NotifiType.UserFollow:

                            sender = UserManager.FindByIdAsync(senderId).Result;

                            if (sender != null)
                            {
                                title2 = " شما را دنبال کرد ";
                                title = sender.FullName + " شما را دنبال کرد ";
                            }
                            else
                            {
                                return false;
                            }
                            break;

                        default:
                            title = "خطای ایجاد پیغام ناتیفیکیشن";
                            break;
                    }

                    var notifi = new Notifi
                    {
                        Title = title2,
                        ReceiverId = userId,
                        SenderId = sender.Id,
                        Date = DateTime.Now,
                        NotifiType = notifiType,
                        PostId = postId,
                        CommentId = commentId
                    };

                    if (notifi.ReceiverId == notifi.SenderId)
                    {
                        return true;
                    }

                   _unitOfWork.NotifiGR.Add(notifi);



                    foreach (var userPushToken in userPushTokens)
                    {
                        var notifiContent = new NotifiContent
                        {
                            notification = new notification()
                            {
                                title = title,
                                body = body,
                                image = img
                            },
                            //data = new data()
                            //{
                            //    image = img
                            //},
                            to = userPushToken.Token,
                            time_to_live = 300


                        };

                        WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                        tRequest.Method = "post";

                        tRequest.ContentType = "application/json";


                        string output = JsonConvert.SerializeObject(notifiContent);

                        Byte[] byteArray = Encoding.UTF8.GetBytes(output);

                        tRequest.Headers.Add($"Authorization: key={_applicationId}");


                        tRequest.ContentLength = byteArray.Length;


                        using (Stream dataStream = tRequest.GetRequestStream())
                        {

                            dataStream.Write(byteArray, 0, byteArray.Length);


                            using (WebResponse tResponse = tRequest.GetResponse())
                            {

                                using (Stream dataStreamResponse = tResponse.GetResponseStream())
                                {

                                    using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                    {

                                        String sResponseFromServer = tReader.ReadToEnd();

                                        string str = sResponseFromServer;

                                        var notifiLog = new NotifiLog
                                        {
                                            NotifiId = notifi.Id,
                                            Date = DateTime.Now,
                                            Result = str
                                        };

                                        _unitOfWork.NotifiLogGR.Add(notifiLog);

                                    }
                                }
                            }
                        }

                    }

                    _unitOfWork.SaveChanges();



                    return true;
                }

                else
                {
                    return false;

                }
            }
            catch (Exception e)
            {

                throw;
            }
        }


   


        private class NotifiContent
        {

            public notification notification { get; set; }
            //public data data { get; set; }
            public string to { get; set; }
            public int time_to_live { get; set; }
            public android android { get; set; }
        }

        private class data
        {
            public string image { get; set; }
        }

        private class notification
        {
            public string title { get; set; }

            public string body { get; set; }

            public string image { get; set; }

            public string icon { get; set; } = "/Content/img/icon/apple-icon-152x152-manifest-205.png";

            public string click_action { get; set; } = "Home/index";

            public string direction { get; set; } = "rtl";

        }
        private class android
        {
            public string ttl { get; set; }
        }
    }
}
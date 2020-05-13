using System;
using System.Collections.Generic;
using System.Linq;
using newFace.Server.Utility;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories;
using newFace.Shared.Repositories.Generic;

using Microsoft.AspNetCore.Identity;

namespace newFace.Server.Services
{
    public class ChatRepository : IChatRepository
    {

        private IUnitOfWork _unitOfWork;
        private UserManager<ApplicationUser> UserManager;

        public ChatRepository(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            UserManager = userManager;
        }
        public ResultChat GetChat(int? id, bool seen)
        {
            ResultChat result = new ResultChat();
            try
            {
                if (id == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی دریافت نشد";
                    return result;
                }


                //var chat = _db.Chats.Include(c=>c.Sender).FirstOrDefault(c => c.Id == id);
                var chat = _unitOfWork.ChatGR.GetSingleIncluding(c => c.Id == id, c => c.Sender , c =>c.ParentChat);
                if (chat != null)
                {
                    if (seen && !chat.Seen)
                    {
                        Seen(chat);
                    }

                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ChatList.Add(chat);
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }

        }

        public ResultChat DeleteChat(int? id , string userId)
        {
            ResultChat result = new ResultChat();
            try
            {
                if (id == null)
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی دریافت نشد";
                    return result;
                }

                var chat = _unitOfWork.ChatGR.GetSingleIncluding(c => c.Id == id, c => c.Sender);
                if (chat != null)
                {
                    if (userId == chat.SenderId)
                    {
                        chat.IsDeleted = true;
                        chat.DateIsDeleted = DateTime.Now;
                        _unitOfWork.ChatGR.Update(chat);

                        result.Statue = Enums.Statue.Success;
                        result.Message = "با موفقیت ارسال شد";
                        result.ChatList.Add(chat);
                        return result;
                    }
                    else
                    {
                        result.Statue = Enums.Statue.Failure;
                        result.Message = "فقط فرستنده پیام قادر به حذف آن است";
                        return result;
                    }


                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public ResultChat GetAllChatsOfTwoUser(string senderId, string receiverId, int? pageNumber)
        {
            ResultChat result = new ResultChat();
            try
            {
                if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی فرستنده یا گیرنده دریافت نشد";
                    return result;
                }


                //IQueryable<Chat> allChats = _db.Chats.Include(c => c.Sender).Where(c => (c.SenderId == senderId && c.ReceiverId == receiverId) || (c.SenderId == receiverId && c.ReceiverId == senderId));

                IQueryable<Chat> allChats;

                if (pageNumber == null)
                {
                    allChats = _unitOfWork.ChatGR.GetAllIncluding(c => (c.SenderId == senderId && c.ReceiverId == receiverId) || (c.SenderId == receiverId && c.ReceiverId == senderId), c => c.Sender).OrderByDescending(a => a.Id);
                }
                else
                {
                    allChats = _unitOfWork.ChatGR.GetAllIncluding(c => (c.SenderId == senderId && c.ReceiverId == receiverId) || (c.SenderId == receiverId && c.ReceiverId == senderId), c => c.Sender).OrderByDescending(a => a.Id).Pagination(pageNumber.Value, 10);
                }

                if (allChats.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ChatIQueryableList = allChats;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد";
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }

        }

        public ResultChat GetUnSeenChatsByUserId(string userId, IQueryable<Chat> allChats)
        {
            ResultChat result = new ResultChat();
            try
            {
                if (string.IsNullOrEmpty(userId))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی فرستنده دریافت نشد";
                    return result;
                }

                if (allChats.Any())
                {
                    List<Chat> notSeenList = allChats.Where(a => a.ReceiverId == userId && a.Seen == false).ToList();

                    if (notSeenList.Any())
                    {
                        result.Statue = Enums.Statue.Success;
                        result.Message = "با موفقیت ارسال شد";
                        result.ChatList = notSeenList;
                        return result;
                    }

                }

                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                return result;



            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }

        }

        public ResultChat GetChatsHistory(string senderId, string receiverId, int? pageNumber)
        {
            ResultChat result = new ResultChat();
            try
            {
                if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "ای دی فرستنده یا گیرنده دریافت نشد";
                    return result;
                }


                IQueryable<Chat> allChats = GetAllChatsOfTwoUser(senderId, receiverId, pageNumber).ChatIQueryableList;
                if (allChats != null && allChats.Any())
                {
                    List<Chat> notSeenList = GetUnSeenChatsByUserId(senderId, allChats).ChatList;
                    if (notSeenList != null)
                    {
                        foreach (Chat item in notSeenList)
                        {
                            Seen(item);
                        }
                        //Save("");
                    }

                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.ChatList = allChats.OrderBy(a=>a.Id).ToList();
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "موردی یافت نشد";
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }

        }

        public ResultChat GetChatContact(string userId, string search)
        {
            ResultChat result = new ResultChat();
            try
            {
                result.Statue = Enums.Statue.Success;
                if (!string.IsNullOrEmpty(search))
                {
                    result.ChatContactList = UserManager.Users.Where(u => (u.UserName.Contains(search) || u.FullName.Contains(search)) && u.Id != userId)
                                                              .Select(u => new ChatContactViewModels() 
                                                              { 
                                                                  UserId = u.Id,
                                                                  UserName = u.UserName,
                                                                  NickName = u.NickName,
                                                                  Avatar = u.Img,
                                                                  FullName = u.FullName,
                                                                  Credit = u.Credit
                                                              }).ToList();
                    result.IsSearch = true;
                    return result;
                }

                if (!string.IsNullOrEmpty(userId))
                {
                    //result.ChatContactList = _Service.GetAllIncluding(u => u.ReceiverId == userId || u.SenderId == userId, c => c.Sender)
                    //                                  .GroupBy(u => new { u.SenderId, u.ReceiverId })
                    //                                  .Select(u => new ChatContactViewModels()
                    //                                  {
                    //                                      UserId = u.FirstOrDefault().SenderId == userId ? u.FirstOrDefault().ReceiverId : u.FirstOrDefault().SenderId,
                    //                                      Avatar = u.FirstOrDefault().SenderId == userId ? u.FirstOrDefault().Receiver.Img : u.FirstOrDefault().Sender.Img,
                    //                                      NickName = u.FirstOrDefault().SenderId == userId ? u.FirstOrDefault().Receiver.NickName : u.FirstOrDefault().Sender.NickName,
                    //                                      UserName = u.FirstOrDefault().SenderId == userId ? u.FirstOrDefault().Receiver.UserName : u.FirstOrDefault().Sender.UserName,
                    //                                      UnSeenCount = u.Where(c => c.Seen == false && c.ReceiverId == userId).Count()
                    //                                  })
                    //                                  .GroupBy(u => u.UserId)
                    //                                  .Select(u => new ChatContactViewModels()
                    //                                  {
                    //                                      UserId = u.FirstOrDefault().UserId,
                    //                                      Avatar = u.FirstOrDefault().Avatar,
                    //                                      NickName = u.FirstOrDefault().NickName,
                    //                                      UserName = u.FirstOrDefault().UserName,
                    //                                      UnSeenCount = u.Sum(uu => uu.UnSeenCount)
                    //                                  })
                    //                                  .ToList();

                    result.ChatContactList = _unitOfWork.ChatContactGR.GetAllIncluding(c => c.UserId == userId, c => c.Contact)
                                                                .OrderByDescending(u => u.UpdateDate)
                                                                .Select(u => new ChatContactViewModels()
                                                                {
                                                                    Id = u.Id,
                                                                    UserId = u.ContactId,
                                                                    Avatar = u.Contact.Img,
                                                                    NickName = u.Contact.NickName,
                                                                    UserName = u.Contact.UserName,
                                                                    FullName = u.Contact.FullName,
                                                                    Credit = u.Contact.Credit,
                                                                    UnSeenCount = u.UnSeenCount,
                                                                    Chat = u.Chat,
                                                                    UpdateDate = u.UpdateDate,
                                                                })
                                                                .ToList();

                    return result;
                }

                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
                throw;
            }


        }

        public bool UpdateChatContact(string senderId, string receiverId , int chatId)
        {
            Result result = new Result();

            var contact = _unitOfWork.ChatContactGR.FirstOrDefault(c => c.UserId == receiverId && c.ContactId == senderId);
            if (contact == null)
            {
                ChatContact chatContact1 = new ChatContact
                {
                    UserId = receiverId,
                    ContactId = senderId,
                    ChatId = chatId,
                    UnSeenCount = 1
                };
                result = _unitOfWork.ChatContactGR.Add(chatContact1);
                if (result.Statue == Enums.Statue.Failure)
                {
                    return false;
                }

                ChatContact chatContact2 = new ChatContact
                {
                    ChatId = chatId,
                    UserId = senderId,
                    ContactId = receiverId,
                };
                result = _unitOfWork.ChatContactGR.Add(chatContact2);
                if (result.Statue == Enums.Statue.Failure)
                {
                    return false;
                }
            }
            else
            {
                contact.ChatId = chatId;
                contact.UpdateDate = DateTime.Now;
                ++contact.UnSeenCount;
                result = _unitOfWork.ChatContactGR.Update(contact);
                if (result.Statue == Enums.Statue.Failure)
                {
                    return false;
                }

                var contact2 = _unitOfWork.ChatContactGR.FirstOrDefault(c => c.UserId == senderId && c.ContactId == receiverId);
                if (contact2 != null)
                {
                    contact2.ChatId = chatId;
                    contact2.UpdateDate = DateTime.Now;
                    _unitOfWork.ChatContactGR.Update(contact2);
                }
            }

            return true;
        }

        public int UnSeenCount(string userId)
        {

            //var userChats = _Service.GetAll();
            //List<Chat> unSeenChatList = GetUnSeenChatsByUserId(userId, userChats).ChatList;

            return _unitOfWork.ChatContactGR.GetAll().Where(c => c.UserId == userId)?.Sum(c => (int?)c.UnSeenCount) ?? 0;

            //if (unSeenChatList != null)
            //{
            //    return unSeenChatList.Count;
            //}
            //return 0;


        }

        public Result Seen(Chat obj)
        {
            Result result = new Result();
            try
            {
                obj.Seen = true;
                _unitOfWork.ChatGR.Update(obj);

                var chatContact = _unitOfWork.ChatContactGR.FirstOrDefault(c => c.UserId == obj.ReceiverId && c.ContactId == obj.SenderId);
                if (chatContact != null && chatContact.UnSeenCount > 0)
                {
                    --chatContact.UnSeenCount;
                    _unitOfWork.ChatContactGR.Update(chatContact);
                }
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;

            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public ResultChat Create(Chat obj)
        {
            ResultChat result = new ResultChat();

            try
            {
                _unitOfWork.ChatGR.Add(obj);
                result.Statue = Enums.Statue.Success;
                result.Chat = obj;
                result.Message = "";
                return result;
            }
            catch (Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;
            }
        }

        //public Result Delete(int? id)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        if (id == null)
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "ای دی دریافت نشد";
        //            return result;
        //        }
        //        //Chat vision = _db.Chats.FirstOrDefault(p => p.Id == id.Value);
        //        Chat chat = _Service.GetById(id.Value);

        //        if (chat != null)
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "یافت نشد";
        //            return result;
        //        }

        //        _Service.Delete(chat);
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }

        //}

        //public Result Delete(Chat obj)
        //{
        //    Result result = new Result();

        //    try
        //    {
        //        _Service.Delete(obj);
        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;
        //    }
        //    catch (Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }
        //}

        //public Result Edit(Chat obj)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        //_db.Entry(obj).State = EntityState.Modified;
        //        _Service.Update(obj);

        //        result.Statue = Enums.Statue.Success;
        //        result.Message = "";
        //        return result;

        //    }
        //    catch (System.Exception e)
        //    {
        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;

        //    }
        //}



    }
}
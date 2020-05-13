using System.Collections.Generic;
using System.Linq;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories
{
    public interface IChatRepository
    {
        ResultChat GetChat(int? id , bool seen);
        ResultChat DeleteChat(int? id, string userId);
        ResultChat GetAllChatsOfTwoUser(string senderId, string receiverId, int? pageNumber);
        ResultChat GetUnSeenChatsByUserId(string userId, IQueryable<Chat> allChats);
        ResultChat GetChatsHistory(string senderId, string receiverId, int? pageNumber);
        ResultChat GetChatContact(string search,string userId);
        bool UpdateChatContact(string senderId, string receiverId, int chatId);

        int UnSeenCount(string userId);
        Result Seen(Chat obj);

        ResultChat Create(Chat obj);
        //Result Edit(Chat obj);
        //Result Delete(int? id);
        //Result Delete(Chat obj);
 

    }

}

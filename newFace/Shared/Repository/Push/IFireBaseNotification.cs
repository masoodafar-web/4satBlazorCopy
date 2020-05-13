using System;
using System.Collections.Generic;
using System.Text;
using static newFace.Shared.Models.Resource.Enums;

namespace newFace.Shared.Repository.Push
{
    public interface IFireBaseNotification
    {
        bool SendByUserId(string userId, string senderId, int? objectId, NotifiType notifiType);
    }
}

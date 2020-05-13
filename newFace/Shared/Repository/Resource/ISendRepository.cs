using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Resource
{
    public interface ISendRepository
    {
        bool Sms(string PhoneNumber, string Text);
        void SendEmail(IdentityMessage message);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Shop
{
    public interface IPaymentRepository
    {
        ResultPayment RequestPayment(string apiCode, string userId, double TotalPayment , string Description , int factorId, string WebsiteUrl, string CallbackUrl, Enums.ReturnFrom returnfrom);

        ResultPayment PaymentVerification(string apicode, string Authority, int TotalPayment);
    }
  
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Financial;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.Shop;
using newFace.Shared.Models.ViewModels;

namespace newFace.Shared.Repositories.Education
{
   public interface ICartRepository
    {

        Result Create(string UserId , int? ProductId, CartType cartType, string RecieverUserId,int? ShareholderPercent);

        bool IfExist(string UserId, int? ProductId , string ReciverUserId , CartType cartType);

        Result Delete(string UserId, int? CartId);


        Result EmptyCart(string UserId);

        ResultCart GetAll(string userid);

        //int CartCount(string userid);
        ResultCart GetCreditById(int? id);

        ResultCart Payment(string userid, BillType BillType);

        ResultCart CompeletePayment(int billId, string referenceCode);

        ResultCart CreateCredit(string userid, double Amount, TransactionTypeEnum transactionType);

        Result EditCredit(ApplicationUser user, int WalletId, string referenceCode);




    }
 
}

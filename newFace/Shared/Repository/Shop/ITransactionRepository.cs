using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Shop
{
   public  interface ITransactionRepository
   {

       ResultTransaction GetBillById(int? id);

        ResultTransaction ShowBill(int id, string userid);

        //int BillCount(string userid);

        ResultTransaction GetAll(string userid);
    }

    }

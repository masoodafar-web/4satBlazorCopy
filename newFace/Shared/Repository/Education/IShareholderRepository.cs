using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Resource
{
    public interface IShareholderRepository
    {
        //Result Create(Shareholder shareholder);
        //Result Edit(Shareholder shareholder);
        //Result Delete(int? id);
        //Result Delete(Shareholder shareholder);
        //ResultShareholder GetById(int? id);
        //ResultShareholder GetAll();

        ResultShareholder GetProductsOfShareholders(string userId);
        //ResultShareholder GetUserProductsOfShareholdersCount(string userId);

        //Result Save(string message);
    }
  
}

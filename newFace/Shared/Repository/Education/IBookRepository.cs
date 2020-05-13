using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;

namespace newFace.Shared.Repositories.Education
{
   public interface IBookRepository
    {
        Result Create(Book Book);

        Result Edit(Book Book);

        Result Delete(int? Id);
        ResultBook GetById(int? Id);
        ResultBook GetAll();

        

    }
 
}

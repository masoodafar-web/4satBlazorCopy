using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;


namespace newFace.Shared.Repositories.Education
{
    public interface IProductScaleRepository
    {
        Result Create(ProductScale ProductScale);

        Result Edit(ProductScale ProductScale);

        Result Delete(int? Id);
        ResultProductScale GetById(int? Id);
        ResultProductScale GetAll();

        ResultProductScale GetProductByCatIdChild(int catId, List<Category> categories);

        ResultProductScale GetProductByCatId(int catId , List<Category> categories);
        //چون درصد حذف شد دیگه نیازی به این متد نیست
        //Result CheckSum(int? CatId,double? percent);

        double GetProductCredit(int productId);

    }
}


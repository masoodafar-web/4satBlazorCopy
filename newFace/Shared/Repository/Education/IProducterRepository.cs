using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;

namespace newFace.Shared.Repositories.Education
{
    public interface IProducterRepository
    {
        ResultProducter AddProducter(ProducterViewModel producterViewModel);
        ResultProducter UpdateProducter(ProducterViewModel producterViewModel);
        ResultProducter DeleteProducter(ProducterViewModel producterViewModel);
    }
}

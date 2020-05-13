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
    public interface IQuestionRepository
    {
        Result Create(Question Question);

        Result Edit(Question Book);

        Result Delete(int? Id);
        ResultQuestion GetById(int? Id);
        ResultQuestion GetAll();

        ResultQuestionAnswerList GetAllByExamId(int? Id);

        //Result Save(string message);



    }
  
}

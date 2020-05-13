using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;

namespace newFace.Shared.Repositories.Education
{
    public interface IExamRepository
    {
        Result Create(Exam Exam);

        Result Edit(Exam Exam);

        Result Delete(int? Id);
        ResultExam GetById(int? Id);
        ResultExam GetAll();
        ResultExam SaveResultExam(string userid, List<UserAnswerVM> userAnswerVm , int? ExamId, Enums.StatusTypeQuestion? StatusTypeQuestion);
    }


   
}


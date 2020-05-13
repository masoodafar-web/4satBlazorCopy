using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Education;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;

namespace newFace.Server.Services.Education
{
    public class ExamRepository : IExamRepository
    {
      
        private readonly IProductSeenInfoRepository _productSeenInfoRepository;
        private IUnitOfWork _unitOfWork;

        public ExamRepository(IProductSeenInfoRepository productSeenInfoRepository, IUnitOfWork unitOfWork)
        {
            _productSeenInfoRepository = productSeenInfoRepository;
            _unitOfWork = unitOfWork;
        }


        public Result Create(Exam Exam)
        {
            Result result = new Result();
            if (Exam.ExamTime == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا زمان آزمون را ارسال کنید";
                return result;
            }

            //Video.CourseId = courseid.Value;

            return _unitOfWork.ExamGR.Add(Exam);

        }

        public Result Edit(Exam Exam)
        {
            Result result = new Result();


            if (Exam.ExamTime == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا زمان آزمون را ارسال کنید";
                return result;
            }


            return _unitOfWork.ExamGR.Update(Exam);


        }

        public ResultExam GetById(int? Id)
        {
            ResultExam Result = new ResultExam();

            if (Id != null)
            {
                Exam Exam = _unitOfWork.ExamGR.GetById(Id.Value);
                if (Exam != null)
                {
                    Result.Statue = Enums.Statue.Success;
                    Result.Message = "با موفقیت ارسال شد";
                    Result.Exam = Exam;
                    return Result;
                }
                else
                {
                    Result.Statue = Enums.Statue.Failure;
                    Result.Message = "ارسال نشد";
                    return Result;
                }

            }
            else
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = "آی دی دریافت نشد";
                return Result;
            }


        }

        public Result Delete(int? Id)
        {
            Result result = new Result();

            if (Id != null)
            {
                Exam Exam = _unitOfWork.ExamGR.GetById(Id.Value);
                return _unitOfWork.ExamGR.Delete(Exam);

            }
            else
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "آی دی دریافت نشد";
                return result;
            }

        }


        public ResultExam GetAll()
        {
            ResultExam result = new ResultExam();


            List<Exam> ExamList = _unitOfWork.ExamGR.GetAll().ToList();
            if (ExamList.Any())
            {
                result.Statue = Enums.Statue.Success;
                result.Message = "با موفقیت ارسال شد";
                result.Exams = ExamList;
                return result;
            }
            else
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "موردی یافت نشد!!";
                result.Exams = ExamList;
                return result;
            }



        }


        public ResultExam SaveResultExam(string userid, List<UserAnswerVM> userAnswerVm, int? ExamId, Enums.StatusTypeQuestion? StatusTypeQuestion)
        {
            ResultExam result = new ResultExam();


            if (StatusTypeQuestion == null || StatusTypeQuestion == Enums.StatusTypeQuestion.Normal)
            {
                userAnswerVm= userAnswerVm.Select(s =>
                    new UserAnswerVM
                    {
                        ExamId = ExamId.Value,
                        QuestionId = s.QuestionId,
                        SelectedChoice = s.SelectedChoice
                    }
                ).ToList();
            }

            if (string.IsNullOrEmpty(userid))
            {
                result.Message = "شناسه کاربر را ارسال کنید";
                result.Statue = Enums.Statue.Failure;

            }
            else if (userAnswerVm.Count == 0 || userAnswerVm==null)
            {
                result.Message = "لطفا پاسخ ها را ارسال کنید";
                result.Statue = Enums.Statue.Failure;
            }
            else
            {
                

                List<UserAnswer> ua = new List<UserAnswer>();
                foreach (var item in userAnswerVm)
                {
                    var useranswer = new UserAnswer
                    {
                        QuestionId = item.QuestionId,
                        Question = _unitOfWork.QuestionGR.GetById(item.QuestionId),
                        Answer = item.SelectedChoice,
                        Score = _unitOfWork.QuestionGR.GetById(item.QuestionId).Score,
                        UserId = userid
                    };
                    ua.Add(useranswer);
                }



                //if (save.Statue == Enums.Statue.Success)
                {
                    var questionGroups = userAnswerVm.GroupBy(g => g.ExamId).ToList();
                    List<ExamResult> ExamResultList = new List<ExamResult>();
                    foreach (var groups in questionGroups)
                    {
                        var exam = _unitOfWork.ExamGR.GetSingleIncluding(f => f.Id == groups.Key, i => i.Questions);
                        ExamResult ER = new ExamResult();
                        ER.ExamId = exam.Id;
                        ER.UserId = userid;
                        ER.CorrectAnswersCount = 0;
                        ER.ExamDateTime = DateTime.Now;
                        ER.ExamType = StatusTypeQuestion == null ? Enums.StatusTypeQuestion.Normal : Enums.StatusTypeQuestion.Rating;

                        var totalscore = ua.Where(w => w.Question.ExamId == groups.Key).Sum(s => s.Score);
                        foreach (var item2 in ua.Where(w => w.Question.ExamId == groups.Key))
                        {
                            var answer = _unitOfWork.AnswerGR.GetSingleIncluding(s => s.Id == item2.Answer, i => i.Questions);
                            ER.CorrectAnswerScore += (answer == null) ? 0 : answer.CorrectAnswer ? answer.Questions.Score : 0;
                            ER.CorrectAnswersCount += (answer == null) ? 0 : answer.CorrectAnswer ? 1 : 0;

                        }

                        //var questioncount = exam.Questions
                        // .Count(c => c.QuestionType == Question.Questiontype.RadioButton);

                        ER.CorrectAnswerPercent = ((float)ER.CorrectAnswerScore / (float)totalscore) * 100f;
                        ER.StatusExam = exam.AcceptancePerecentage <= ER.CorrectAnswerPercent
                            ? StatusExam.Pass
                            : StatusExam.Failed;
                        ER.Exam = _unitOfWork.ExamGR.FirstOrDefault(f => f.Id == exam.Id);
                        ExamResultList.Add(ER);
                    }

                    var ifPassed = ExamResultList.Sum(s => s.Exam.AcceptancePerecentage) / ExamResultList.Count;
                    ExamResult Examresult = new ExamResult
                    {
                        UserId = userid,
                        CorrectAnswerPercent = ExamResultList.Sum(s => s.CorrectAnswerPercent) / ExamResultList.Count,
                        CorrectAnswerScore = ExamResultList.Sum(s => s.CorrectAnswerScore),
                        CorrectAnswersCount = ExamResultList.Sum(s => s.CorrectAnswersCount),
                        ExamDateTime = DateTime.Now,
                        ExamId = null,
                        ExamResultId = null,
                        ExamType = Enums.StatusTypeQuestion.Rating,
                        StatusExam = (ExamResultList.Sum(s => s.CorrectAnswerPercent) / ExamResultList.Count) >= ifPassed ? StatusExam.Pass : StatusExam.Failed,

                    };



                    var saveresult = _unitOfWork.ExamResultGR.Add(Examresult);

                    if (saveresult.Statue == Enums.Statue.Success)
                    {

                        var userAnswersresult = ua.Select(s => new UserAnswer
                        {
                            Id = s.Id,
                            User = s.User,
                            Score = s.Score,
                            Answer = s.Answer,
                            QuestionId = s.QuestionId,
                            UserId = s.UserId,
                            ExamResultId = Examresult.Id

                        }).ToList();

                        var exams = ExamResultList.Select(
                            s => new ExamResult
                            {
                                StatusExam = s.StatusExam,
                                CorrectAnswerPercent = s.CorrectAnswerPercent,
                                CorrectAnswerScore = s.CorrectAnswerScore,
                                CorrectAnswersCount = s.CorrectAnswersCount,
                                ExamDateTime = s.ExamDateTime,
                                ExamId = s.ExamId,
                                ExamResultId = Examresult.Id,
                                ExamType = s.ExamType,
                                UserId = s.UserId
                            }
                        ).ToList();
                        if (StatusTypeQuestion == Enums.StatusTypeQuestion.Rating)
                        {
                            var save1 = _unitOfWork.ExamResultGR.AddRange(exams);
                            List<ExamResultVM> examResultVms = new List<ExamResultVM>();
                            foreach (var listexamresult in exams)
                            {
                                
                                    var exam = _unitOfWork.ExamGR.GetById(listexamresult.ExamId.Value);
                                    var productt = _unitOfWork.ProductGR.GetById(exam.ProductId);
                                    var producttscale = _unitOfWork.ProductScaleGR.FirstOrDefault(f=>f.ProductId==exam.ProductId);

                                    if (listexamresult.StatusExam == StatusExam.Pass)
                                    {


                                    //    ProductSeenInfo PSI = new ProductSeenInfo
                                    //{
                                    //    Credit = producttscale.Credit,
                                    //    Point = producttscale.Point,
                                    //    ProductId = productt.ProductId.Value,
                                    //    UserId = userid,
                                    //    IsComplete = true
                                        
                                    //};
                                    //var user = UserManager.FindById(userid);
                                    //user.Credit += PSI.Credit;
                                    //UserManager.Update(user);

                                    var productcompelete =
                                        _productSeenInfoRepository.CompleteReadProduct(productt.ProductId.Value,
                                            userid, StatusTypeQuestion);
                                    var examproductcompelete =
                                        _productSeenInfoRepository.CompleteReadProduct(productt.Id,
                                            userid, StatusTypeQuestion);


                                    ////اضافه شدن
                                    //_ProductSeenInfoService.Add(PSI);
                                }


                                ExamResultVM ervm = new ExamResultVM();
                                    ervm.Product = _unitOfWork.ProductGR.GetSingleIncluding(s=>s.Id== productt.ProductId, s=>s.Books,s=>s.Courses,s=>s.Exams,s=>s.ProductScale);
                                    ervm.ExamResult = listexamresult;
                                    ervm.ProductScale = producttscale;

                                    examResultVms.Add(ervm);

                                

                               
                            }

                            result.ExamResultVMtList.AddRange(examResultVms);
                        }
                        var save2 = _unitOfWork.UserAnswerGR.AddRange(userAnswersresult);

                        result.Statue = Enums.Statue.Success;
                        result.ExamResult = Examresult;
                        
                        result.Message = "آزمون با موفقیت انجام شد";

                    }
                }
            }

            return result;
        }




    }
}
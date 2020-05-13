using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using newFace.Shared.Models;
using newFace.Shared.Models.Resource;
using newFace.Shared.Models.ViewModels;
using newFace.Shared.Repositories.Education;
using newFace.Shared.Repositories.Generic;

namespace newFace.Server.Services.Education
{
    public class QuestionRepository : IQuestionRepository
    {
        private IUnitOfWork _unitOfWork;

        public QuestionRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Result Create(Question Question)
        {
            Result result = new Result();
            if (Question.ExamId == 0)
            {
                result.Statue = Enums.Statue.NullList;
                result.Message = "لطفا شناسه آزمون را ارسال کنید";
                return result;
            }
           
            try
            {

                _unitOfWork.QuestionGR.Add(Question);
                
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public Result Edit(Question Question)
        {
            Result result = new Result();

           

            if (string.IsNullOrEmpty(Question.QuesFile))
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = "لطفا متن سوال را ارسال کنید";
                return result;
            }
            try
            {

                //_db.Entry(Question).State = EntityState.Modified;

                _unitOfWork.QuestionGR.Update(Question);
                result.Statue = Enums.Statue.Success;
                result.Message = "";
                return result;
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public ResultQuestion GetById(int? Id)
        {
            ResultQuestion Result = new ResultQuestion();
            try
            {
                if (Id != null)
                {
                    Question Question = _unitOfWork.QuestionGR.GetById(Id.Value);
                    if (Question != null)
                    {
                        Result.Statue = Enums.Statue.Success;
                        Result.Message = "با موفقیت ارسال شد";
                        Result.Question = Question;
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
            catch (System.Exception e)
            {
                Result.Statue = Enums.Statue.Failure;
                Result.Message = e.Message;
                return Result;

            }
        }

        public Result Delete(int? Id)
        {
            Result result = new Result();
            try
            {
                if (Id != null)
                {
                    Question Question = _unitOfWork.QuestionGR.GetById(Id.Value);
                    _unitOfWork.QuestionGR.Delete(Question);
                    result.Statue = Enums.Statue.Success;
                    result.Message = "";
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.Failure;
                    result.Message = "آی دی دریافت نشد";
                    return result;
                }
            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }


        public ResultQuestion GetAll()
        {
            ResultQuestion result = new ResultQuestion();
            try
            {

                List<Question> QuestionList = _unitOfWork.QuestionGR.GetAll().ToList();
                if (QuestionList.Any())
                {
                    result.Statue = Enums.Statue.Success;
                    result.Message = "با موفقیت ارسال شد";
                    result.Questions  = QuestionList;
                    return result;
                }
                else
                {
                    result.Statue = Enums.Statue.NullList;
                    result.Message = "موردی یافت نشد!!";
                    result.Questions = QuestionList;
                    return result;
                }


            }
            catch (System.Exception e)
            {
                result.Statue = Enums.Statue.Failure;
                result.Message = e.Message;
                return result;

            }
        }

        public ResultQuestionAnswerList GetAllByExamId(int? id)
        {
            ResultQuestionAnswerList result = new ResultQuestionAnswerList();

            var questions = _unitOfWork.QuestionGR.GetAll().Where(w => w.ExamId == id).ToList();

            ExamViewModel EVM = new ExamViewModel();
            EVM.Exam = _unitOfWork.ExamGR.GetById(id.Value);
            foreach (var item in questions)
            {
                QuestionAnswerViewModel qavm = new QuestionAnswerViewModel();
                qavm.Question = item;
                qavm.AnswerList.AddRange(_unitOfWork.AnswerGR.GetAll().Where(w => w.QuestionId == item.Id).ToList());
                EVM.Questions.Add(qavm);
            }

            result.QuestionAnswerList = EVM;
            result.Statue = Enums.Statue.Success;
            return result;
        }

        //public Result Save(string message)
        //{
        //    Result result = new Result();
        //    try
        //    {
        //        if (Convert.ToBoolean(_db.SaveChanges()))
        //        {
        //            result.Statue = Enums.Statue.Success;
        //            result.Message = message + " با موفقیت انجام شد";
        //            return result;
        //        }
        //        else
        //        {
        //            result.Statue = Enums.Statue.Failure;
        //            result.Message = "عملیات" + message + " ناموفق بود";
        //            return result;
        //        }
        //    }
        //    catch (Exception e)
        //    {

        //        result.Statue = Enums.Statue.Failure;
        //        result.Message = e.Message;
        //        return result;
        //    }


        //}
    }
}
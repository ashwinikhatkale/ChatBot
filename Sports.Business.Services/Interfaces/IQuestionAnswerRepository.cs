using ChatBot.Business.Services.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Business.Services.Interfaces
{
    public interface IQuestionAnswerRepository
    {
        Task<QuestionAnswerModel> GetAnswer(long id);
        Task<List<SelectListItem>> GetQuestionsSelectList(long noticeId = 0);
        Task<List<QuestionAnswerModel>> GetQuestionAnswers();
        Task<List<QuestionAnswerModel>> GetQuestionAnswersByNoticeId(long noticeId);
        Task<List<QuestionAnswerModel>> GetPendingQueries();
        Task<List<QuestionAnswerModel>> GetStudentsPendingQueries(long userID);
        Task<bool> Insert(QuestionAnswerModel model);
        Task<bool> InsertStudentQuestions(QuestionAnswerModel model, long userId);
        Task<bool> Update(QuestionAnswerModel model);
        Task<bool> Delete(long id);
        Task<StaticMediaModel> GetFile(long fileId);
    }
}

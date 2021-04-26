using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using ChatBot.Controllers;

namespace ChatBot.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
    public class ChatBotController : BaseController
    {
        private readonly IQuestionAnswerRepository _questionAnswerRepository;
        private readonly INoticeRepository _noticeBoardRepository;
        public ChatBotController(IQuestionAnswerRepository questionsRepository, INoticeRepository noticeBoardRepository)
        {
            _questionAnswerRepository = questionsRepository;
            _noticeBoardRepository = noticeBoardRepository;
        }
        public async Task<ActionResult> Index(long? id = 0)
        {
            var model = id > 0 ? await _questionAnswerRepository.GetAnswer(id ?? 0) : new QuestionAnswerModel();
            model.NoticeBoards = await _noticeBoardRepository.GetNoticeSelectList();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Index(QuestionAnswerModel model)
        {
            await _questionAnswerRepository.InsertStudentQuestions(model, LoggedInUserId);

            if (model.Id > 0)
                return RedirectToAction("Index", "PendingQueries", new { area = "Student"});

            model = new QuestionAnswerModel();
            model.NoticeBoards = await _noticeBoardRepository.GetNoticeSelectList();
            ViewBag.Message = "Query submitted successfully! Very soon you will receive answer of your query.";
            return View(model);
        }

        public async Task<ActionResult> GetQuestions(long noticeId,string prefix)
        {
            var questions = await _questionAnswerRepository.GetQuestionsSelectList(noticeId);

            if(prefix == "%") 
            {
                var questionList = (from q in questions select new { q.Value, q.Text });
                return Json(questionList, JsonRequestBehavior.AllowGet);
            }

            var list =  (from q in questions
                        where q.Text != null && q.Text.ToLower().StartsWith(prefix.ToLower())
                        select new { q.Value, q.Text });

            return Json(list, JsonRequestBehavior.AllowGet);
        }
      
        public async Task<ActionResult> GetAnswer(long questionId)
        {
            var answer = await _questionAnswerRepository.GetAnswer(questionId);
            return Json(new { answer = answer?.Answer }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            await _questionAnswerRepository.Delete(id);
            return Json(new { isSuccess = true });
        }
    }
}
using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PendingQueriesController : BaseController
    {
        private readonly IQuestionAnswerRepository _questionsRepository;
        private readonly INoticeRepository _noticeRepository;
        public PendingQueriesController(IQuestionAnswerRepository questionsRepository, INoticeRepository noticeRepository)
        {
            _questionsRepository = questionsRepository;
            _noticeRepository = noticeRepository;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _questionsRepository.GetPendingQueries();
            return View(model);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var model = await _questionsRepository.GetAnswer(id);
            model.NoticeBoards = await _noticeRepository.GetNoticeSelectList();
            return View("Add", model);
        }
        [HttpPost]
        public async Task<ActionResult> Add(QuestionAnswerModel model)
        {
            await _questionsRepository.Update(model);
            return RedirectToAction("Index");
        }
    }
}
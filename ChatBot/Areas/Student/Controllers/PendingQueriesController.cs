using ChatBot.Business.Services.Interfaces;
using ChatBot.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]
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
            var model = await _questionsRepository.GetStudentsPendingQueries(LoggedInUserId);
            return View(model);
        }
        [HttpPost]
        public async Task<FileResult> DownloadFile(long? fileId)
        {
            var file = await _questionsRepository.GetFile(fileId ?? 0);
            return File(file.InputStream, file.ContentType, file.FileName);
        }
    }
}
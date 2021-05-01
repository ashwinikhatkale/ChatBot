using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Areas.Admin.Controllers
{

    [Authorize(Roles = "Admin")]
    public class QuestionsController : BaseController
    {
        private readonly IQuestionAnswerRepository _questionsRepository;
        private readonly INoticeRepository _noticeRepository;
        public QuestionsController(IQuestionAnswerRepository questionsRepository, INoticeRepository noticeRepository)
        {
            _questionsRepository = questionsRepository;
            _noticeRepository = noticeRepository;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _questionsRepository.GetQuestionAnswers();
            return View(model);
        }
        public async Task<ActionResult> Add()
        {
            var model = new QuestionAnswerModel();
            model.NoticeBoards = await _noticeRepository.GetNoticeSelectList();
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
           var uploadedFile = new byte[model.File.InputStream.Length];
            model.File.InputStream.Read(uploadedFile, 0, uploadedFile.Length);
            model.FileInputStream = uploadedFile;

            if (model.Id > 0)
                await _questionsRepository.Update(model);
            else
                await _questionsRepository.Insert(model);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            await _questionsRepository.Delete(id);
            return Json(new { isSuccess=true });
        }

        [HttpPost]
        public async Task<FileResult> DownloadFile(long? fileId)
        {
            var file = await _questionsRepository.GetFile(fileId ?? 0);
            return File(file.InputStream, file.ContentType, file.FileName);
        }
    }
}
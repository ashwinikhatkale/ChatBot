using ChatBot.Controllers;
using ChatBot.Business.Services.Interfaces;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Areas.Student.Controllers
{
    [Authorize(Roles = "Student")]

    public class NoticeBoardController : BaseController
    {
        private readonly IQuestionAnswerRepository _questionsRepository;
        private readonly INoticeRepository _noticeRepository;
        public NoticeBoardController(IQuestionAnswerRepository questionsRepository, INoticeRepository noticeBoardRepository)
        {
            _questionsRepository = questionsRepository;
            _noticeRepository = noticeBoardRepository;
        }
        public async Task<ActionResult> Index(DateTime? fromDate, DateTime? toDate)
        {
            if (fromDate == null)
                fromDate = DateTime.Now.AddDays(-30);

            if (toDate == null)
                toDate = DateTime.Now;

            var model = await _noticeRepository.GetNoticesWithinDate((DateTime)fromDate, (DateTime)toDate);

            if (Request.IsAjaxRequest())
                return PartialView("~/Areas/Student/Views/NoticeBoard/_Notices.cshtml", model);

            return View(model);
        }
        public async Task<ActionResult> Download(long noticeId)
        {
            var notice = await _noticeRepository.GetNotice(noticeId);
            var model = await _questionsRepository.GetQuestionAnswersByNoticeId(noticeId);
            StringBuilder csv = new StringBuilder();

            if(model != null && model.Any())
            {
                csv.Append(Environment.NewLine);
                csv.Append("NoticeBoard: ," + model.FirstOrDefault().NoticeBoard);
                csv.Append(Environment.NewLine);
                csv.Append(Environment.NewLine);
                csv.Append("Question Hint 1,Question Hint 2,Question Hint 3, Answer");
                csv.Append(Environment.NewLine);

                foreach (var element in model)
                {
                    csv.Append(element.QuestionHint1 + "," + element.QuestionHint2 + "," + element.QuestionHint3 + "," + element.Answer);
                    csv.Append(Environment.NewLine);
                }
            }
            else
            {
                csv.Append("Questions not found.");
            }

            return File(new System.Text.UTF8Encoding().GetBytes(csv.ToString()), "text/csv", $"NoticeBoard {notice.Name}.csv");


        }
    }
}
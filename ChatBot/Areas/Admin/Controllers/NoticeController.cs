using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Controllers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class NoticeController : BaseController
    {
        private readonly INoticeRepository _noticeRepository;
        public NoticeController(INoticeRepository noticeRepository)
        {
            _noticeRepository = noticeRepository;
        }
        public async Task<ActionResult> Index()
        {
            var model = await _noticeRepository.GetNotices();
            return View(model);
        }
        
        public ActionResult Add()
        {
            var model = new NoticeModel { NoticeDateTime = System.DateTime.Now };
            return View(model);
        }

        public async Task<ActionResult> Edit(long id)
        {
            var model = await _noticeRepository.GetNotice(id);
            return View("~/Areas/Admin/Views/Notice/Add.cshtml",model);
        }

        [HttpPost]
        public async Task<ActionResult> Add(NoticeModel model)
        {
            if(model.Id > 0)
                await _noticeRepository.Update(model);
            else
                await _noticeRepository.Insert(model);

            return RedirectToAction("Index","Notice",new { area="Admin" });
        }

        [HttpPost]
        public async Task<ActionResult> Delete(long id)
        {
            await _noticeRepository.Delete(id);
            return Json(new { isSuccess = true });
        }
    }
}














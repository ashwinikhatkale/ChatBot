using System.Web.Mvc;

namespace ChatBot.Controllers
{
    [Authorize(Roles = "Admin, Student")]
    public class DashBoardController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}
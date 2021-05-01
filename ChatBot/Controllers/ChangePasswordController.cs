using ChatBot.Business.Services.Interfaces;
using ChatBot.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ChatBot.Controllers
{
    [Authorize(Roles = "Admin, Student")]
    public class ChangePasswordController : BaseController
    {
        private readonly IUserRepository _userRepository;
        public ChangePasswordController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public ActionResult Index()
        {
            var model = new ChangePasswordModel{ Message = "" };
            model.UserId = LoggedInUserId; 
            return View(model);
        }
        [HttpPost]
        public async Task<ActionResult> Index(ChangePasswordModel model)
        {
            await _userRepository.ChangePassword(model.UserId, model.NewPassword);
            model.Message = "Change Password Successfully!";
            return View(model);
        }
    }
}
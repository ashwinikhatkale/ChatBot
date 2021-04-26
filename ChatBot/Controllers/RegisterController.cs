using ChatBot.Models;
using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ChatBot.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;
        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
       
        [AllowAnonymous]
        public ActionResult Index()
        {
            FormsAuthentication.SignOut();
            var model = new UserModel { RoleId = (long)UserRole.Student };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(UserModel model)
        {
            await _userRepository.Insert(model);
            ViewBag.Success = true;
            return View(model);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
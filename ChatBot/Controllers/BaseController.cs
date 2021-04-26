using ChatBot.Business.Services.Models;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ChatBot.Controllers
{
    public class BaseController : Controller
    {
        public UserModel User => HttpContext.User.Identity.IsAuthenticated ? (UserModel)new JavaScriptSerializer().Deserialize(((FormsIdentity)(HttpContext.User.Identity)).Ticket.UserData, typeof(UserModel)) : null;
        public long LoggedInUserId => (User?.Id ?? 0);
    }
}
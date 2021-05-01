using ChatBot.Business.Services.Interfaces;
using ChatBot.Models;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ChatBot.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        [AllowAnonymous]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                {
                    return RedirectToAction(Request.Form["ReturnUrl"]); //.Split('/')[2]
                }
                else
                {
                    return RedirectToAction("Index", "DashBoard");
                }
            }

            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginModel model)
        {
            var user = await _userRepository.CheckUserExists(model.UserName, model.Password);

            if (user != null)
            {
                var serializer = new JavaScriptSerializer();
                string userData = serializer.Serialize(user);
                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, user.FirstName+ " " + user.LastName, DateTime.Now, DateTime.Now.AddMinutes(2880), true, userData, FormsAuthentication.FormsCookiePath);
                string hash = FormsAuthentication.Encrypt(ticket);
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                if (!string.IsNullOrEmpty(Request.Form["ReturnUrl"]))
                {
                    return RedirectToAction(Request.Form["ReturnUrl"].Split('/')[2]);
                }
                else
                {
                    return RedirectToAction("Index", "DashBoard");
                }
            }

            ModelState.AddModelError("", "Invalid Username or Password.");
            return View(); 
        }
        
        public ActionResult ForgotPassword()
        {
            var model = new ForgotPasswordModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordModel model)
        {
            try
            {
                var isUserExists = await _userRepository.IsUserWithEmailIdExists(model.EmailAddress);

                if (!isUserExists)
                {
                    ModelState.AddModelError("EmailAddress", "User with entered email address does not exists.");
                    return View(model);
                }
                string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
                Random random = new Random();

                // Select one random character at a time from the string  
                // and create an array of chars  
                char[] chars = new char[6];
                for (int i = 0; i < 6; i++)
                {
                    chars[i] = validChars[random.Next(0, validChars.Length)];
                }
             
                var password = new string(chars);
                await _userRepository.ChangePasswordByEmail(model.EmailAddress, password);

                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("ashwinikhatkale@gmail.com");
                message.To.Add(new MailAddress(model.EmailAddress));
                message.Subject = "Password for Chatbot Application";
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = "Dear Student, <br/><br/>Password for <b>Chatbot Application</b> is <b>" + password + "</b>.<br/><br/>Regards,<br/><b>Chatbot Team</b>";
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com"; //for gmail host  
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("ashwinikhatkale@gmail.com", "Ashwini@16");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
                model.Message = "Temparary password has been sent on your email address.";
            }
            catch (Exception ex) 
            {
                model.Message = "Error occurred while sending email. Please try again.";
            }
           
            return View(model);
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}
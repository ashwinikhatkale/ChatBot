using SimpleInjector;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
using ChatBot.Business.Services.Core;
using ChatBot.Business.Services.Interfaces;
using ChatBot.Business.Services.Models;
using ChatBot.Business.Services.Services;
using ChatBot.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ChatBot
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            // Register your stuff here
            container.Register<ChatBotContext, ChatBotContext>(Lifestyle.Scoped);

            var assemblyRegistry = AssemblyRegistry.Create(new string[] { "ChatBot.Data", "ChatBot.Business.Services", "ChatBot" });
            var assemblies = assemblyRegistry.Assemblies.ToArray();

            container.RegisterInstance<IAssemblyRegistry>(assemblyRegistry);
            container.Register<INoticeRepository, NoticesRepository>(Lifestyle.Scoped);
            container.Register<IQuestionAnswerRepository, QuestionAnswerRepository>(Lifestyle.Scoped);
            container.Register<IRoleRepository, RoleRepository>(Lifestyle.Scoped);
            container.Register<IUserRepository, UserRepository>(Lifestyle.Scoped);
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());
            container.RegisterMvcIntegratedFilterProvider();
            container.RegisterPackages(assemblies);

            container.Verify();
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity is FormsIdentity)
                    {
                        string userData = ((FormsIdentity)(Context.User.Identity)).Ticket.UserData;
                        // Deserialize the json data and set it on the custom principal.
                        var serializer = new JavaScriptSerializer();
                        var user = (UserModel)serializer.Deserialize(userData, typeof(UserModel));
                        FormsIdentity id = (FormsIdentity)HttpContext.Current.User.Identity;
                        
                        string[] roles = new string[] { user.RoleName };
                        HttpContext.Current.User = new GenericPrincipal(id, roles);
                    }
                }
            }
        }
    }
}

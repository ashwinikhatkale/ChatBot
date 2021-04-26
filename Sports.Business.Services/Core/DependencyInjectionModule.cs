using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace ChatBot.Business.Services.Core
{
    public abstract class DependencyInjectionModule : IPackage
    {
        protected IAssemblyRegistry AssemblyRegistry { get; private set; }

        protected List<Assembly> Assemblies => AssemblyRegistry.Assemblies.ToList(); 

        protected Container Container { get; private set; }

        protected abstract void OnLoad();

	    public void RegisterServices(Container container)
	    {
		    Container = container;
            AssemblyRegistry = Core.AssemblyRegistry.Create();

            OnLoad();
	    } 
    }
}
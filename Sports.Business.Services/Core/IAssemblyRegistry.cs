using System;
using System.Collections.Generic;
using System.Reflection;

namespace ChatBot.Business.Services.Core
{
    public interface IAssemblyRegistry
    {
        IEnumerable<Assembly> Assemblies { get; }
        IEnumerable<ManifestResourceInfo> EmbeddedResources { get; }
        IEnumerable<Type> ExportedTypes { get; }
        IEnumerable<Type> ConcreteTypes { get; }
        IEnumerable<Type> GetConcreteTypesDerivingFrom(Type baseType);
    }
}
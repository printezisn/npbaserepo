using DryIoc;
using DryIoc.Mvc;
using NPBaseRepo.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NPBaseRepo.App_Start
{
    public static class DependencyConfig
    {
        public static void Register()
        {
            var container = new Container();
            container.Register<IDataRepository, DataRepository>(setup: Setup.With(allowDisposableTransient: true));

            container.RegisterMany(DryIocMvc.GetReferencedAssemblies(), type => type.IsAssignableTo(typeof(IController)),
                Reuse.Transient, FactoryMethod.ConstructorWithResolvableArguments,
                setup: Setup.With(trackDisposableTransient: true));

            DependencyResolver.SetResolver(new DryIocDependencyResolver(container));
        }
    }
}
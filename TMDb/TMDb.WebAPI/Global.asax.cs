using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Routing;
using System.Configuration;
using AutoMapper;
using AutoMapper.Configuration;
using TMDb.Repository;
using TMDb.Service;
using TMDb.Common;
using TMDb.Model;
using TMDb.WebAPI.Controllers;
using TMDb.Service.Common;
using TMDb.Repository.Common;
using TMDb.Model.Common;

namespace TMDb.WebAPI
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();
            var assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
            var config = GlobalConfiguration.Configuration;

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            foreach (var assemblie in assemblies)
            {
                builder.RegisterAssemblyModules(assemblie);
            }

            var container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}

using System;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Messageboard.Data.Database;
using Messageboard.Data.Repositories;
using Messageboard.Domain.Repositories;

namespace Messageboard.Web
{
    public static class AutofacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            var messageboardWeb = typeof (MvcApplication).Assembly;
            var messageboardData = Assembly.GetAssembly(typeof (Repository<>));
            var messageboardDomain = Assembly.GetAssembly(typeof(IRepository<>));

            var assemblies = new[] {messageboardDomain, messageboardData, messageboardWeb};

            /* Register MVC controllers */
            builder.RegisterControllers(assemblies);

            /* Register API controllers */
            builder.RegisterApiControllers(assemblies);

            /* Register DbContext */
            builder.RegisterType<MessageboardContext>()
                .AsSelf()
                .As<DbContext>()
                .InstancePerHttpRequest();

            /* Register repositories */
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces();

            /* Register services */
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();

            var container = builder.Build();

            /* Hook into MVC dependency resolver */
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            /* Hook into Web API dependency resolver */
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}
using Autofac;
using Autofac.Integration.Mvc;
using AutoMapper;
using Library.DataAccess;
using Library.Models;
using Microsoft.Data.Sqlite;
using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace Library.App_Start
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            #region IRepository for Sqlite

            var repositoryTypes = typeof(MvcApplication).Assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                        .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IRepository<>))
                        && t.Name.StartsWith("Sqlite"));

            foreach (var repositoryType in repositoryTypes)
            {
                builder.RegisterType(repositoryType)
                    .AsImplementedInterfaces();
            }

            #endregion

            //AutoMapper

            builder.Register(c =>
            {
                //This resolves a new context that can be used later.
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
            .As<IMapper>()
            .InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(typeof(MvcApplication).Assembly)
                      .Where(t => t.Name.EndsWith("Service"))
                      .AsSelf();


            builder.Register(c => new SqliteConnection(ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString))
                .As<IDbConnection>()
                .InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
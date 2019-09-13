using Autofac;
using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.Application.Queries;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate;
using JobLogger.LoggerApi.Domain.Aggregates.LogAggregate.Factory;
using JobLogger.LoggerApi.Infrastructure.Repository;
using JobLogger.LoggerApi.ViewModels;
using MediatR;
using System.Reflection;

namespace JobLogger.LoggerApi.Infrastructure.AutofacModules
{
    public class ApplicationModule : Autofac.Module
    {

        protected override void Load(ContainerBuilder builder)
        {
            #region Command

            builder.RegisterAssemblyTypes(typeof(LogToCreateRequest).GetTypeInfo().Assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>));

            #endregion



            #region Query
            builder.RegisterType<LogQueries>()
             .As<ILogQueries>()
             .InstancePerLifetimeScope();
            

            #endregion



            #region Factory

            builder.RegisterType<LogFactory>()
             .As<ILogFactory>()
             .InstancePerLifetimeScope();

            #endregion



            #region Repository
            builder.RegisterType<LogRepository>()
             .As<ILogRepository>()
             .InstancePerLifetimeScope();

            

            #endregion
        }
    }
}

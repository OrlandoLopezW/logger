using Autofac;
using FluentValidation;
using JobLogger.LoggerApi.Application.Commands.LogCommands;
using JobLogger.LoggerApi.ViewModels;

namespace JobLogger.LoggerApi.Infrastructure.AutofacModules
{
    public class ValidatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<LogToCreateRequestValidator>()
                .As<AbstractValidator<LogToCreateRequest>>()
                .AsImplementedInterfaces();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using JobLogger.LoggerApi.Infrastructure.AutofacModules;
using JobLogger.LoggerApi.Infrastructure.Filters;
using JobLogger.LoggerApi.ViewModels;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace JobLogger.LoggerApi
{
    public class Startup
    {
        public IConfiguration _Configuration { get; }

        public Startup(
            IConfiguration configuration)
        {
            _Configuration = configuration;
        }

        public IServiceProvider ConfigureServices(
            IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddControllersAsServices();

            services.AddDefaultAWSOptions(_Configuration.GetAWSOptions());
            Environment.SetEnvironmentVariable("AWS_ACCESS_KEY_ID", _Configuration["AWS:AccessKey"]);
            Environment.SetEnvironmentVariable("AWS_SECRET_ACCESS_KEY", _Configuration["AWS:SecretKey"]);
            Environment.SetEnvironmentVariable("AWS_REGION", _Configuration["AWS:Region"]);
            services.AddAWSService<IAmazonDynamoDB>();

            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API - Logger API", Version = "v1" });
            });

            var container = new ContainerBuilder();
            container.Populate(services);
            container.RegisterModule(new ApplicationModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ValidatorModule());
            return new AutofacServiceProvider(container.Build());

        }

        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowOrigin");
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API - Logger API - V1");
            });

            app.UsePathBase(_Configuration["pathBase"]);
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}

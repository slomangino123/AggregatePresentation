using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AggregatePresentation.CommandHandlers;
using AggregatePresentation.Infrastructure;
using AggregatePresentation.PipelineBehavior;
using AggregatePresentation.Processors;
using AggregatePresentation.Services.FullNameService;
using AggregatePresentation.Services.IdGenerator;
using CQRSlite.Domain;
using CQRSlite.Events;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AggregatePresentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();

            // Mediatr
            services.AddMediatR(typeof(Startup).Assembly);
            services.AddScoped<ServiceFactory>(x => x.GetService);
            services.AddTransient(typeof(ICommandDispatcher), typeof(CommandDispatcher));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

            // CqrsLite
            services.AddTransient(typeof(ISession), typeof(Session));
            services.AddTransient(typeof(IRepository), typeof(Repository));
            services.AddSingleton(typeof(IEventStore), typeof(InMemoryEventStore));

            // services
            services.AddTransient(typeof(IFullNameService), typeof(FullNameService));
            services.AddTransient(typeof(IUniqueIdGenerator), typeof(UniqueIdGenerator));
            services.AddTransient(typeof(ICreateSomethingAggregateProcessor), typeof(CreateSomethingAggregateProcessor));

            services.AddTransient(typeof(DoSomethingCommandHandler));
            //services.AddTransient(typeof(DoSomethingBetterCommandHandler));
            //services.AddTransient(typeof(DoSomethingBestCommandHandler));
            //services.AddTransient(typeof(DoSomethingBonusCommandHandler));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            // This forces https
            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}

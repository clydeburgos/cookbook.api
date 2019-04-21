using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using CookBook.API.Automapper;
using CookBook.Data.Models;
using CookBook.Service;
using CookBook.Service.AzureServices;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace CookBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CookbookContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CookbookConnection")),
           (ServiceLifetime.Transient));

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                          p => p.AllowAnyOrigin()
                                .AllowAnyHeader()
                                .AllowAnyMethod());
            });

            services.AddOData();
            var autoMapperConfig = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutomapperProfile());
            });
            var autoMapper = autoMapperConfig.CreateMapper();
            services.AddSingleton(autoMapper);

            services.AddMvc()
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver())
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<BingSearchImageService>();
            builder.RegisterType<RecipeService>().As<IRecipeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("AllowAll");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseMvc(routerBuilder => {
                routerBuilder.EnableDependencyInjection();
                routerBuilder.Filter().Select().OrderBy().MaxTop(null).Count();
            });
        }
    }
}

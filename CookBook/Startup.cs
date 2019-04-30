using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using CookBook.API.Automapper;
using CookBook.Data.Models;
using CookBook.Model;
using CookBook.Service;
using CookBook.Service.AzureServices;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

            var authenticationServiceConfig = new AuthenticationServiceConfig();
            Configuration.GetSection("AuthenticationService").Bind(authenticationServiceConfig);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = authenticationServiceConfig.JwtIssuer,
                        ValidAudience = authenticationServiceConfig.JwtAudience,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationServiceConfig.JwtSigningKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken) &&
                                (context.HttpContext.WebSockets.IsWebSocketRequest ||
                                 context.Request.Headers["Accept"] == "text/event-stream"))
                            {
                                context.Token = accessToken;
                            }

                            return Task.CompletedTask;
                        }
                    };
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
            var authenticationServiceConfig = new AuthenticationServiceConfig();
            Configuration.GetSection("AuthenticationService").Bind(authenticationServiceConfig);

            builder.RegisterInstance(authenticationServiceConfig).As<AuthenticationServiceConfig>();

            builder.RegisterType<BingSearchImageService>();
            builder.RegisterType<UserService>().As<IUserService>();
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
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            app.UseHttpsRedirection();
            app.UseMvc(routerBuilder => {
                routerBuilder.EnableDependencyInjection();
                routerBuilder.Filter().Select().OrderBy().MaxTop(null).Count();
            });
        }
    }
}

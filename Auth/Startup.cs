using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holder.ERP.Auth.Identity.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace Auth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentityServer(option =>
            {
                //可以通过此设置来指定登录路径，默认的登陆路径是/account/login
                option.UserInteraction.LoginUrl = "/account/login";
                option.IssuerUri = "http://localhost:51376";
            })
           .AddDeveloperSigningCredential()
           .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
           .AddInMemoryApiResources(Config.GetApiResource())
           .AddInMemoryClients(Config.GetClients())
           .AddInMemoryIdentityResources(Config.GetIdentityResources());
            //.AddTestUsers(Config.GetTestUsers());

            services.AddLogging(logBuilder =>
            {
                logBuilder.AddNLog();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
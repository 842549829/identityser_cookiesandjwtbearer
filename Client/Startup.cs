using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog.Extensions.Logging;

namespace Client
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                //认证失败，会自动跳转到这个地址
                options.LoginPath = "/Home/Index";

                //var originRedirectToLogin = options.Events.OnRedirectToLogin;
                //options.Events.OnRedirectToLogin = context =>
                //{
                //    return originRedirectToLogin(RebuildRedirectUri(context));
                //};
            })
            .AddJwtBearer(option =>
            {
                option.Authority = "http://localhost:51376";
                option.RequireHttpsMetadata = false;
                option.Audience = "api";
            });

            services.AddHttpClient();
            services.AddControllers();

            services.AddLogging(logBuilder =>
            {
                logBuilder.AddNLog();
            });
        }

        private static RedirectContext<CookieAuthenticationOptions> RebuildRedirectUri(
RedirectContext<CookieAuthenticationOptions> context)
        {
            if (context.RedirectUri.StartsWith(""))
                return context;

            var originUri = new Uri(context.RedirectUri);
            var uriBuilder = new UriBuilder(context.RedirectUri);
            uriBuilder.Path = originUri.AbsolutePath;
            var queryStrings = QueryHelpers.ParseQuery(originUri.Query);
            var returnUrlName = context.Options.ReturnUrlParameter;
            var returnUrl = originUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped) + queryStrings[returnUrlName];
            uriBuilder.Query = QueryString.Create(returnUrlName, returnUrl).ToString();
            context.RedirectUri = "http://localhost:51408/home/token";
            return context;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

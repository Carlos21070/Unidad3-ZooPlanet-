using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ZooPlanet
{
    
        public class Startup
        {
            // This method gets called by the runtime. Use this method to add services to the container.
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            public void ConfigureServices(IServiceCollection services)
            {
                services.AddMvc();
                services.AddSingleton<ZooPlanet.Services.MenuServices>();
                services.AddDbContext<Models.animalesContext>(
                    options => options.UseMySql("server=localhost;database=animales;uid=root; password=root")
                    );
            }

            // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

            public IWebHostEnvironment Enviroment { get; set; }
            public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            {
                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                Enviroment = env;
                app.UseRouting();
                app.UseFileServer();
                app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllerRoute(name: "areas", pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
                });
            }
        }
    }

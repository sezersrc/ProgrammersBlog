using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ProgrammersBlog.MVC.Controllers;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;

namespace ProgrammersBlog.MVC
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enum deðerleri döndürür (JsonNamingPolicy:CamelCase olursa string girebilirsin)
                opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.Preserve;// iç içe olan (nested ) json'A çevirir.
            }); // MVC olduðunu belirteci && Razor Runtime Compalition . // optJson'A dönüþtürmek için.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile)); // *Profil sýnýfý olmak zorunda Derlenme esnasýnda AutoMapper'ý derliyor . Mapping sýnýflarýný çaðýrýyor.

            // Service katmanýndan servis yüklenmek . Data'ya direk ulaþmýyor Service >Data aktarýp oradan çekiyor.
            services.LoadMyServices();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/User/Login");
                options.LogoutPath = new PathString("/Admin/User/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, // Güvenlik açýðý always'a çekersin SSL'e yönlenir.

                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/User/AccessDenied");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePages();// Sitede bulunmayan bir view'A gidildiðinde 404 kodu döner.
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSession();
            app.UseHttpsRedirection();  // Projede yok ;
            app.UseStaticFiles();  // Statik dosya java , html gibi dosyalarý kullandýrýr.

            app.UseRouting();
            app.UseCors(); // Startup hatasý çözümü. // projede yok

            app.UseAuthentication(); // Identity => Authentication 
            app.UseAuthorization();  // Identity => Authorization  


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}

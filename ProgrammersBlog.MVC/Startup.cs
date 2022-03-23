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
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enum de�erleri d�nd�r�r (JsonNamingPolicy:CamelCase olursa string girebilirsin)
                opt.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.Preserve;// i� i�e olan (nested ) json'A �evirir.
            }); // MVC oldu�unu belirteci && Razor Runtime Compalition . // optJson'A d�n��t�rmek i�in.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile),typeof(ArticleProfile)); // *Profil s�n�f� olmak zorunda Derlenme esnas�nda AutoMapper'� derliyor . Mapping s�n�flar�n� �a��r�yor.

            // Service katman�ndan servis y�klenmek . Data'ya direk ula�m�yor Service >Data aktar�p oradan �ekiyor.
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
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, // G�venlik a���� always'a �ekersin SSL'e y�nlenir.

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
                app.UseStatusCodePages();// Sitede bulunmayan bir view'A gidildi�inde 404 kodu d�ner.
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }


            app.UseSession();
            app.UseHttpsRedirection();  // Projede yok ;
            app.UseStaticFiles();  // Statik dosya java , html gibi dosyalar� kulland�r�r.

            app.UseRouting();
            app.UseCors(); // Startup hatas� ��z�m�. // projede yok

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

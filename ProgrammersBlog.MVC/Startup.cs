using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            services.AddControllersWithViews().AddRazorRuntimeCompilation(); // MVC oldu�unu belirteci && Razor Runtime Compalition 
            services.AddAutoMapper(typeof(Startup)); // Derlenme esnas�nda AutoMapper'� derliyor . Mapping s�n�flar�n� �a��r�yor.

            // Service katman�ndan servis y�klenmek . Data'ya direk ula�m�yor Service >Data aktar�p oradan �ekiyor.
            services.LoadMyServices();
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

          

            app.UseHttpsRedirection();
            app.UseStaticFiles();  // Statik dosya java , html gibi dosyalar� kulland�r�r.

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                // Admin Raouting => Tek Areai�in 
                endpoints.MapAreaControllerRoute(
                    name: "Admin",
                    areaName: "Admin",
                    pattern: "Admin/{controller=home}/{action=Index}/{id?}"
                );
                endpoints.MapDefaultControllerRoute();  // Varsay�lan olarak Hom Controller'A g�t�r�r.
            });
        }
    }
}

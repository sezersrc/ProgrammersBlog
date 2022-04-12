using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProgrammersBlog.MVC.AutoMapper.Profiles;
using ProgrammersBlog.MVC.Helpers.Abstract;
using ProgrammersBlog.MVC.Helpers.Concrete;
using ProgrammersBlog.Services.AutoMapper.Profiles;
using ProgrammersBlog.Services.Extensions;
using System.Text.Json.Serialization;

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
            services.AddControllersWithViews(options =>
            {
                // The value can not be Null mesaj� t�rk�ele�tirme.
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value=>"Bu alan bo� ge�ilmemelidir");
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enum de�erleri d�nd�r�r (JsonNamingPolicy:CamelCase olursa string girebilirsin)
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;// i� i�e olan (nested ) json'A �evirir.
                
            }).AddNToastNotifyToastr(); // MVC oldu�unu belirteci && Razor Runtime Compalition . // optJson'A d�n��t�rmek i�in.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(ViewModelsProfile),typeof(CommentProfile)); // *Profil s�n�f� olmak zorunda Derlenme esnas�nda AutoMapper'� derliyor . Mapping s�n�flar�n� �a��r�yor.

            // Service katman�ndan servis y�klenmek . Data'ya direk ula�m�yor Service >Data aktar�p oradan �ekiyor.
            services.LoadMyServices(connectionString: Configuration.GetConnectionString("LocalDB")); // Local DB'de
            services.AddScoped<IImageHelper, ImageHelper>();
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Admin/Auth/Login");
                options.LogoutPath = new PathString("/Admin/Auth/Logout");
                options.Cookie = new CookieBuilder
                {
                    Name = "ProgrammersBlog",
                    HttpOnly = true,
                    SameSite = SameSiteMode.Strict,
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, // G�venlik a���� always'a �ekersin SSL'e y�nlenir.

                };
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = System.TimeSpan.FromDays(7);
                options.AccessDeniedPath = new PathString("/Admin/Auth/AccessDenied");
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
            app.UseNToastNotify();

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

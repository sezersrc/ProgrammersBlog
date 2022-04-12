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
                // The value can not be Null mesajý türkçeleþtirme.
                options.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(value=>"Bu alan boþ geçilmemelidir");
            }).AddRazorRuntimeCompilation().AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); // Enum deðerleri döndürür (JsonNamingPolicy:CamelCase olursa string girebilirsin)
                opt.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;// iç içe olan (nested ) json'A çevirir.
                
            }).AddNToastNotifyToastr(); // MVC olduðunu belirteci && Razor Runtime Compalition . // optJson'A dönüþtürmek için.
            services.AddSession();
            services.AddAutoMapper(typeof(CategoryProfile), typeof(ArticleProfile), typeof(UserProfile), typeof(ViewModelsProfile),typeof(CommentProfile)); // *Profil sýnýfý olmak zorunda Derlenme esnasýnda AutoMapper'ý derliyor . Mapping sýnýflarýný çaðýrýyor.

            // Service katmanýndan servis yüklenmek . Data'ya direk ulaþmýyor Service >Data aktarýp oradan çekiyor.
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
                    SecurePolicy = CookieSecurePolicy.SameAsRequest, // Güvenlik açýðý always'a çekersin SSL'e yönlenir.

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

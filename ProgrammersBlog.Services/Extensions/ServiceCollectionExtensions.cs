using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Services.Concrete;

namespace ProgrammersBlog.Services.Extensions
{
    public  static class ServiceCollectionExtensions
    {
        public static IServiceCollection LoadMyServices(this IServiceCollection serviceCollection)
        {
            // Data KAtmanındaki  yapıları MVC KAtmanına aktarmaya yarayan servis .Startup.CS'den çağırdık.
            serviceCollection.AddDbContext<ProgrammersBlogContext>();
            serviceCollection.AddIdentity<User, Role>(options =>
            {
                // User Password Options 
                options.Password.RequireDigit = false;  // Password testten sonra true yap ; 
                options.Password.RequiredLength = 5;    // Şifre uzunluk minimum değer.
                options.Password.RequiredUniqueChars = 0;   // Özel Karakter farklı sayısı
                options.Password.RequireNonAlphanumeric = false; // *@ gibi işaretleri zorunlu yapar true  . 
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                // User Username and Email Options 
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
             

            }).AddEntityFrameworkStores<ProgrammersBlogContext>();  // Identity 
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICategoryService, CategoryManager>();
            serviceCollection.AddScoped<IArticleService, ArticleManager>();

            return serviceCollection;
        }
    }
}

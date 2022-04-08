using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.MVC.Helpers.Abstract;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        public BaseController(UserManager<User> userManager, IMapper mapper, IImageHelper imageHelper)
        {
            UserManager = userManager;
            Mapper = mapper;
            ImageHelper = imageHelper;
        }

        // Ortak ihtiyaç duyabileceğimiz değerleri giriyoruz ç

        protected UserManager<User>   UserManager { get; }  // Türeyen sınıflarda kullanmak için protected.
        protected IMapper Mapper { get; }
        protected IImageHelper ImageHelper { get; }
        protected User LoggedInUser => UserManager.GetUserAsync(HttpContext.User).Result; // Giren user'ı alıyoruz .

    }
}

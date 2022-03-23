using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{  
     [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;

        public UserController(UserManager<User> userManager ,IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        public async  Task<IActionResult>  Index()
        {
            var users = await _userManager.Users.ToListAsync();
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Succes
            });
        }
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }

        public async Task<string> ImageUpload(UserAddDto userAddDto)
        {    
            // resmin adını kaydediceğiz .  ~/img/user.Picture gibi 
            string wwwroot = _env.WebRootPath;
            // sezersurucu => sezersurucu.png gibi . kullanıcının gönderdiği dosya adı
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.PictureFile.FileName);
            //.png uzantısını alıyoruz .
            string fileExtension = Path.GetExtension(userAddDto.PictureFile.FileName);
            DateTime dateTime=DateTime.Now;
            // Örnek ; SezerSurucu_587_5_38_12_3_10_2020.png 
            string fileName = $"{userAddDto.UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img",fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await userAddDto.PictureFile.CopyToAsync(stream);
            }

            return fileName;

        }
    }
}

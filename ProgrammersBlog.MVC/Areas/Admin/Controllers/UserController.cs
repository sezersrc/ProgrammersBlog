using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.MVC.Areas.Admin.Models;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{  
     [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;

        public UserController(UserManager<User> userManager ,IWebHostEnvironment env, IMapper mapper)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
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

        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto);
                var user  = _mapper.Map<User>(userAddDto);
                var result = await _userManager.CreateAsync(user, userAddDto.Password); //identityResult dönüyor // password'ü identity hashleyerek db'ye yazıyor.
                if (result.Succeeded)
                {
                    var userAddAjaxModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Succes,
                            Message = $"{user.UserName} adlı kullanıcı adına sahip kullanıcı başarıyla eklenmiştir",
                            User = user
                        },
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)

                    });
                    return Json(userAddAjaxModel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("",error.Description);

                    }

                    var userAddAjaxErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
                    {
                        UserAddDto = userAddDto,
                        UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
                    });
                    return Json(userAddAjaxErrorModel);
                }
            }
            var userAddAjaxModelStateErrorModel = JsonSerializer.Serialize(new UserAddAjaxViewModel
            {
                UserAddDto = userAddDto,
                UserAddPartial = await this.RenderViewToStringAsync("_UserAddPartial", userAddDto)
            });
            return Json(userAddAjaxModelStateErrorModel);

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

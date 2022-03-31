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
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{  
     [Area("Admin")]
    
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _env;
        private readonly IMapper _mapper;
        private readonly SignInManager<User> _signInManager;

        public UserController(UserManager<User> userManager ,IWebHostEnvironment env, IMapper mapper, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _env = env;
            _mapper = mapper;
            _signInManager = signInManager;
        }
        [Authorize]
        public async  Task<IActionResult>  Index()
        {
            var users = await _userManager.Users.ToListAsync();
            ImageDelete("test ");
            return View(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Succes
            });
        }
       
        [HttpGet]
        public IActionResult Login()
        {
            return View("UserLogin");
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDto userLoginDto )
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(userLoginDto.Email);
                if (user!=null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, userLoginDto.Password, userLoginDto.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                       
                    }
                    else
                    {
                        ModelState.AddModelError("","Eposta adresiniz veya şifreniz yalnıştır .");
                        return View("UserLogin");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Eposta adresiniz veya şifreniz yalnıştır .");
                    return View("UserLogin");
                }
            }
            else
            {
                return View("UserLogin");
            }
           
        }
        [Authorize]
        [HttpGet]
        public async Task<JsonResult> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            
            var userListDto =JsonSerializer.Serialize(new UserListDto
            {
                Users = users,
                ResultStatus = ResultStatus.Succes
            },new JsonSerializerOptions 
            {
                ReferenceHandler = ReferenceHandler.Preserve
            });

            return Json(userListDto);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return PartialView("_UserAddPartial");
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(UserAddDto userAddDto)
        {
            if (ModelState.IsValid)
            {
                userAddDto.Picture = await ImageUpload(userAddDto.UserName, userAddDto.PictureFile);
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
        [Authorize]
        public async Task<JsonResult> Delete (int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                var deletedUser = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Succes,
                    Message = $"{user.UserName} adlı kullanıcı başarıyla silinmiştir ",
                    User = user

                });
                return Json(deletedUser);

            }
            else
            {
                string errorMessages = String.Empty;
                foreach (var error in result.Errors)
                {
                    errorMessages = $"*{error.Description}\n";
                }
                var deletedUserErrorModel = JsonSerializer.Serialize(new UserDto
                {
                    ResultStatus = ResultStatus.Error,
                    Message = $"{user.UserName} adlı kullanıcı silinirken bazı hatalar oluştu\n{errorMessages} ",
                    User = user
                });
                return Json(deletedUserErrorModel);
            }
        }

        [Authorize]
        [HttpGet]
        public async Task<PartialViewResult> Update(int userId)
        {    // 2. Yöntem 
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            var userUpdateDto = _mapper.Map<UserUpdateDto>(user);
            return PartialView("_UserUpdatePartial", userUpdateDto);

        }

        [Authorize]
        [HttpPost]

        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            if (ModelState.IsValid)
            {
                bool isNewPictureUploaded = false;
                var oldUser = await _userManager.FindByIdAsync(userUpdateDto.Id.ToString());
                var oldUserPicture = oldUser.Picture;
                if (userUpdateDto.PictureFile!=null)
                {
                    userUpdateDto.Picture = await ImageUpload(userUpdateDto.UserName, userUpdateDto.PictureFile);
                    isNewPictureUploaded = true;

                }
                var updatedUser = _mapper.Map<UserUpdateDto, User>(userUpdateDto, oldUser);
                var result = await _userManager.UpdateAsync(updatedUser);
                if (result.Succeeded)
                {
                    if (isNewPictureUploaded)
                    {
                        ImageDelete(oldUserPicture);
                    }
                    var userUpdateViewMoel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserDto = new UserDto
                        {
                            ResultStatus = ResultStatus.Succes,
                            Message = $"{updatedUser.UserName} adlı kullanıcı başarıyla güncellenmiştir",
                            User = updatedUser
                        },
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    });
                    return Json(userUpdateViewMoel);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    var userUpdateErrorViewMoel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                    {
                        UserUpdateDto = userUpdateDto,
                        UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                    }); 
                    return Json(userUpdateErrorViewMoel);
                }

            }
            else
            {
                var userUpdateModelStateErrorViewMoel = JsonSerializer.Serialize(new UserUpdateAjaxViewModel
                {
                    UserUpdateDto = userUpdateDto,
                    UserUpdatePartial = await this.RenderViewToStringAsync("_UserUpdatePartial", userUpdateDto)
                });
                return Json(userUpdateModelStateErrorViewMoel);
            }

        }

        [Authorize]
        public async Task<string> ImageUpload(string UserName , IFormFile pictureFile)
        {    
            // resmin adını kaydediceğiz .  ~/img/user.Picture gibi 
            string wwwroot = _env.WebRootPath;
            // sezersurucu => sezersurucu.png gibi . kullanıcının gönderdiği dosya adı
            //string fileName = Path.GetFileNameWithoutExtension(userAddDto.PictureFile.FileName);
            //.png uzantısını alıyoruz .
            string fileExtension = Path.GetExtension(pictureFile.FileName);
            DateTime dateTime=DateTime.Now;
            // Örnek ; SezerSurucu_587_5_38_12_3_10_2020.png 
            string fileName = $"{UserName}_{dateTime.FullDateAndTimeStringWithUnderscore()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/img",fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await pictureFile.CopyToAsync(stream);
            }

            return fileName;

        }

        [Authorize]
        public bool ImageDelete(string pictureName)
        {
            pictureName = "sezersurucu_76_46_6_14_23_3_2022.png";
             // güncelleme için eski resmi silecek class 
            string wwwroot = _env.WebRootPath;
            var fileToDelete = Path.Combine($"{wwwroot}/img", pictureName);
            if (System.IO.File.Exists(fileToDelete))
            {
                System.IO.File.Delete(fileToDelete);
                return true;
            }
            else
            {
                return false;
            }
        }

     
    }
}

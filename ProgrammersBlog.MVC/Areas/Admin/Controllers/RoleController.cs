using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.MVC.Areas.Admin.Models;
using ProgrammersBlog.MVC.Helpers.Abstract;
using ProgrammersBlog.Shared.Utilities.Extensions;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RoleController : BaseController
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager,UserManager<User> userManager,IMapper mapper,IImageHelper imageHelper):base(userManager,mapper,imageHelper)
        {
            _roleManager = roleManager;
        }

        [Authorize(Roles = "SuperAdmin,Role.Read")]
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return View(new RoleListDto
            {
                Roles = roles
            });
        }

        [Authorize(Roles = "SuperAdmin,Role.Read")]

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            var rolesDto = JsonSerializer.Serialize(new RoleListDto { Roles = roles });
            return Json(rolesDto);
            
        }

        [Authorize(Roles = "SuperAdmin,User.Update")]

        [HttpGet]
        public async Task<IActionResult> Assign(int userId)
        {
            var user = await UserManager.Users.SingleOrDefaultAsync(u => u.Id == userId);
            var roles = await _roleManager.Roles.ToListAsync();
            var userRoles = await UserManager.GetRolesAsync(user);
            UserRoleAssingDto userRoleAssingDto = new()
            {
                UserId = user.Id,
                UserName = user.UserName
            };

            foreach (var role in roles)
            {
                RoleAssignDto roleAssignDto = new ()
                {
                    RoleId = role.Id,
                    RoleName = role.Name,
                    HasRole = userRoles.Contains(role.Name)
                };
                userRoleAssingDto.RoleAssignDtos.Add(roleAssignDto);
            }

            return PartialView("_RoleAssignPartial", userRoleAssingDto);

        }

        [Authorize(Roles = "SuperAdmin,User.Update")]

        [HttpPost]

        public async Task<IActionResult> Assign(UserRoleAssingDto userRoleAssingDto)        
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.Users.SingleOrDefaultAsync(u => u.Id == userRoleAssingDto.UserId);
                foreach (var roleAssignDto in userRoleAssingDto.RoleAssignDtos)
                {
                    if (roleAssignDto.HasRole)
                        await UserManager.AddToRoleAsync(user, roleAssignDto.RoleName);
                    else
                    {
                        await UserManager.RemoveFromRoleAsync(user, roleAssignDto.RoleName);
                    }
                }

                await UserManager.UpdateSecurityStampAsync(user);// Kullanıcı SecStamp değeri sıfırlanıcak . 30 dk bir kontrol edilir varsayılan olarak. 
                //ServiceCollectionExtensions'A Configure ekleyerek bu varsayılan değer değiştirilebilir.
                var userRoleAssignAjaxViewModel = JsonSerializer.Serialize(new UserRoleAssignAjaxViewModel
                {
                    UserDto = new UserDto
                    {
                        User = user,
                        Message = $"{user.UserName} kullanıcısına ait rol atama işlemi başarıyla tamamlanmıştır.",
                        ResultStatus = ResultStatus.Success
                    },
                    RoleAssignPartial = await this.RenderViewToStringAsync("_RoleAssignPartial",userRoleAssingDto)
                });
                return Json(userRoleAssignAjaxViewModel);
            }
            else
            {
                var userRoleAssignAjaxErrorModel = JsonSerializer.Serialize(new UserRoleAssignAjaxViewModel
                {
                 
                    RoleAssignPartial = await this.RenderViewToStringAsync("_RoleAssignPartial", userRoleAssingDto),
                    UserRoleAssingDto = userRoleAssingDto
                });
                return Json(userRoleAssignAjaxErrorModel);
            }
        }
    }
}

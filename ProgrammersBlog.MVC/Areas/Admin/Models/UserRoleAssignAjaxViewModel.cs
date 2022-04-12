using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.MVC.Areas.Admin.Models
{
    public class UserRoleAssignAjaxViewModel
    {
        public UserRoleAssingDto UserRoleAssingDto { get; set; }
        public string RoleAssignPartial { get; set; }
        public UserDto UserDto { get; set; }
    }
}

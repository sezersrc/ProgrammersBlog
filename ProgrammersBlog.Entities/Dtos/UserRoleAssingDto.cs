using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
     public class UserRoleAssingDto
    {
        public UserRoleAssingDto()
        {
            //interface olduğu için initilaze olmaz . Listeye tek tek eleman atacağımız için 
            RoleAssignDtos = new List<RoleAssignDto>();
        }
        public int UserId { get; set; }
        public string UserName { get; set; }

        public IList<RoleAssignDto> RoleAssignDtos { get; set; }
    }
}

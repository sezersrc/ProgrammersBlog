using System;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserListDto : DtoGetBase
    {
        public IList<User> Users { get; set; }
       
    }
}

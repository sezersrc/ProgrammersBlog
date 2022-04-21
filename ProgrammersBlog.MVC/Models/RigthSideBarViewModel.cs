using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.MVC.Models
{
    public class RigthSideBarViewModel
    {
        public IList<Category>  Categories { get; set; }
        public IList<Article> Articles { get; set; }
    }
}

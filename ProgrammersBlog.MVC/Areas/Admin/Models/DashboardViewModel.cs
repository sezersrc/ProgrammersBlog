using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;

namespace ProgrammersBlog.MVC.Areas.Admin.Models
{
    public class DashboardViewModel
    {
        public int CategoriesCount { get; set; }
        public int ArticlesCount { get; set; }  // Aktif makaleler
        public int CommentsCount { get; set; }
        public int UsersCount { get; set; }

        public ArticleListDto Articles { get; set; }  // Tüm makaleler.
    }
}

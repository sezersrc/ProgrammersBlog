using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.MVC.Areas.Admin.Models;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;

        public ArticleController(IArticleService articleService, ICategoryService categoryService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }


        [HttpGet]
        public  async  Task<IActionResult>  Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStatus==ResultStatus.Succes) return View(result.Data );
            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult>  Add()

        {
            var result = await _categoryService.GetAllByNonDeletedAsync();
            if (result.ResultStatus==ResultStatus.Succes)
            {
                return View(new ArticleAddViewModel
                {
                    Categories = result.Data.Categories
                });
            }

            return NotFound();

        }
    }
}

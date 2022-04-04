﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ArticleController : Controller
    {
        private readonly IArticleService _articleService;

        public ArticleController(IArticleService articleService)
        {
            _articleService = articleService;
        }


        [HttpGet]
        public  async  Task<IActionResult>  Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStatus==ResultStatus.Succes) return View(result.Data );
            return NotFound();

        }

        [HttpGet]
        public  IActionResult Add()
        {
            return View();
        }
    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProgrammersBlog.Entities.ComplexTypes;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.MVC.Areas.Admin.Models;
using ProgrammersBlog.MVC.Helpers.Abstract;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProgrammersBlog.Entities.Concrete;

namespace ProgrammersBlog.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class ArticleController : BaseController
    {
        private readonly IArticleService _articleService;
        private readonly ICategoryService _categoryService;
        

        public ArticleController(IArticleService articleService, ICategoryService categoryService,UserManager<User> userManager  ,IMapper mapper, IImageHelper imageHelper):base(userManager,mapper,imageHelper)
        {
            _articleService = articleService;
            _categoryService = categoryService;
            
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetAllByNonDeletedAsync();
            if (result.ResultStatus == ResultStatus.Succes) return View(result.Data);
            return NotFound();

        }

        [HttpGet]
        public async Task<IActionResult> Add()

        {
            var result = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            
            if (result.ResultStatus == ResultStatus.Succes)
            {
                return View(new ArticleAddViewModel
                {
                    Categories = result.Data.Categories
                    
                });
            }

            return NotFound();

        }
        [HttpPost]
        public async Task<IActionResult> Add(ArticleAddViewModel articleAddViewModel)

        {
            
            if (ModelState.IsValid)
            {
                var articleAddDto = Mapper.Map<ArticleAddDto>(articleAddViewModel); // Base'den gelen Mapper
                var imageResult = await ImageHelper.Upload(articleAddViewModel.Title,
                    articleAddViewModel.ThumbnailFile, PictureType.Post);
                articleAddDto.Thumbnail = imageResult.Data.FullName;
                
                var result = await _articleService.AddAsync(articleAddDto, LoggedInUser.UserName,LoggedInUser.Id);
                if (result.ResultStatus == ResultStatus.Succes)
                {
                    TempData.Add("SuccessMassage", result.Message);

                    return RedirectToAction("Index", "Article");
                }
                else
                {
                    ModelState.AddModelError("", result.Message);
                    
                }


            }

            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleAddViewModel.Categories = categories.Data.Categories;
            return View(articleAddViewModel);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int articleId)
        {
            var articleResult = await _articleService.GetArticleUpdateDtoAsync(articleId);
            var categoriesResult = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            if (articleResult.ResultStatus==ResultStatus.Succes&&categoriesResult.ResultStatus==ResultStatus.Succes)
            {
                var articleUpdateViewModel = Mapper.Map<ArticleUpdateViewModel>(articleResult.Data);
                articleUpdateViewModel.Categories = categoriesResult.Data.Categories;
                return View(articleUpdateViewModel);
            }
            else
            {
                return NotFound();
            }

        }

        [HttpPost]
        public async Task<IActionResult> Update(ArticleUpdateViewModel articleUpdateViewModel)
        {
            if (ModelState.IsValid)
            {
                bool isNewThumbnailUploaded = false;
                var oldThumbNail = articleUpdateViewModel.Thumbnail;
                if (articleUpdateViewModel.ThumbnailFile!=null)
                {
                    var uploadedImageResult = await ImageHelper.Upload(articleUpdateViewModel.Title,
                        articleUpdateViewModel.ThumbnailFile, PictureType.Post);
                    articleUpdateViewModel.Thumbnail = uploadedImageResult.ResultStatus == ResultStatus.Succes
                        ? uploadedImageResult.Data.FullName
                        : "postImages/defaultThumbnail.jpg";
                    if (oldThumbNail!= "postImages/defaultThumbnail.jpg")
                    {
                        isNewThumbnailUploaded = true;
                    }

                    var articleUpdateDto = Mapper.Map<ArticleUpdateDto>(articleUpdateViewModel);
                    var result = await _articleService.UpdateAsync(articleUpdateDto, LoggedInUser.UserName);
                    if (result.ResultStatus==ResultStatus.Succes)
                    {
                        if (isNewThumbnailUploaded)
                        {
                            ImageHelper.Delete(oldThumbNail);
                        }
                        TempData.Add("SuccessMessage",result.Message);
                        return RedirectToAction("Index", "Article");
                    }
                    else
                    {
                        ModelState.AddModelError("",result.Message);
                    }
                }

            }

            var categories = await _categoryService.GetAllByNonDeletedAndActiveAsync();
            articleUpdateViewModel.Categories = categories.Data.Categories;
            return View(articleUpdateViewModel);
        }
    }
}

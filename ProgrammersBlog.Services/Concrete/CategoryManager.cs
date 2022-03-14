﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Services.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using ProgrammersBlog.Shared.Utilities.Results.ComplexTypes;
using ProgrammersBlog.Shared.Utilities.Results.Concrete;

namespace ProgrammersBlog.Services.Concrete
{
     public  class CategoryManager:ICategoryService
    {
        // UnitOfWork Kullanımı .

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CategoryManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IDataResult<CategoryDto>> Get(int categoryId)
        {
          var category =  await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c=>c.Articles);
          if (category!=null)
          {
              return new DataResult<CategoryDto>(ResultStatus.Succes, new CategoryDto
              {
                  Category = category,
                  ResultStatus = ResultStatus.Succes
              });
          }

          return new DataResult<CategoryDto>(ResultStatus.Error, "Böyle bir kategori bulunamadı", new CategoryDto
          {
              Category = null,
              ResultStatus = ResultStatus.Error,
              Message = "Böyle bir kategori bulunamadı"
          });
        }

        public async Task<IDataResult<CategoryListDto>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes,new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                    Message = "Katagori Listesi Başarıyla yüklendi"
                });
            }

            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı",new CategoryListDto
            {
                //View'E aktarabilmek için 
                Categories = null,
                ResultStatus = ResultStatus.Error,
                Message = "Hiçbir kategori bulunamadı."
            });
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles); // isDeleted==falce ile aynı.
            if (categories.Count>-1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", null);
        }

        public async Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted&&c.IsActive, c => c.Articles); // isDeleted==falce ile aynı.
            if (categories.Count > -1)
            {
                return new DataResult<CategoryListDto>(ResultStatus.Succes, new CategoryListDto
                {
                    Categories = categories,
                    ResultStatus = ResultStatus.Succes
                });
            }
            return new DataResult<CategoryListDto>(ResultStatus.Error, "Hiçbir kategori bulunamadı", null);
        }


        public async Task<IDataResult<CategoryDto>> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            var category = _mapper.Map<Category>(categoryAddDto);
            category.CreatedByName = createdByName;
            category.ModifiedByName = createdByName;
            var addCategory = await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return new DataResult<CategoryDto>(ResultStatus.Succes,
                message: $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir",new CategoryDto
                {
                    Category = addCategory,
                    ResultStatus = ResultStatus.Succes,
                    Message = $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir"
                });
        }

        public async Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = _mapper.Map<Category>(categoryUpdateDto);
            category.ModifiedByName = modifiedByName;
            var updatedCategory = await _unitOfWork.Categories.UpdateAsync(category);
            await _unitOfWork.SaveAsync();

            return new DataResult<CategoryDto>(ResultStatus.Succes, $"{categoryUpdateDto.Name} adlı kategory başarıyla güncellenmiştir",new CategoryDto
            {
                Category = updatedCategory,
                ResultStatus = ResultStatus.Succes,
                Message = $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir"
            });
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category!=null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate=DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, $"{category.Name} adlı kategori başarıyla silinmiştir");
            }

            return new Result(ResultStatus.Error, "Kategori silinemedi", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {

                await _unitOfWork.Categories.DeleteAsync(category);
                await _unitOfWork.SaveAsync();

                return new Result(ResultStatus.Succes, $"{category.Name} adlı kategori başarıyla veri tabanından silinmiştir");
            }


            return new Result(ResultStatus.Error, "Kategori veri tabanından silinemedi", null);
        }
    }
}

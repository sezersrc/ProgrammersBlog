using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IDataResult<Category>> Get(int categoryId)
        {
          var category =  await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId,c=>c.Articles);
          if (category!=null)
          {
              return new DataResult<Category>(ResultStatus.Succes, category);
          }

          return new DataResult<Category>(ResultStatus.Error, "Böyle bir kategori bulunamadı", null);
        }

        public async Task<IDataResult<IList<Category>>> GetAll()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(null, c => c.Articles);
            if (categories.Count>-1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Succes,categories);
            }

            return new DataResult<IList<Category>>(ResultStatus.Error, "Hiçbir kategori bulunamadı",null);
        }

        public async Task<IDataResult<IList<Category>>> GetAllByNonDeleted()
        {
            var categories = await _unitOfWork.Categories.GetAllAsync(c => !c.IsDeleted, c => c.Articles); // isDeleted==falce ile aynı.
            if (categories.Count>-1)
            {
                return new DataResult<IList<Category>>(ResultStatus.Succes, categories);
            }
            return new DataResult<IList<Category>>(ResultStatus.Error, "Hiçbir kategori bulunamadı", null);
        }

        public async Task<IResult> Add(CategoryAddDto categoryAddDto, string createdByName)
        {
            await _unitOfWork.Categories.AddAsync(new Category
            {
                Name = categoryAddDto.Name,
                Description = categoryAddDto.Description,
                Note = categoryAddDto.Note,
                IsActive = categoryAddDto.IsActive,
                CreatedByName = createdByName,
                CreatedDate = DateTime.Now,
                ModifiedByName = createdByName,
                ModifiedDate = DateTime.Now
            }).ContinueWith(t=>_unitOfWork.SaveAsync());
            // 2. yöntem . hızlı olan
            //await _unitOfWork.SaveAsync(); 1. yöntem .
            return new Result(ResultStatus.Succes,
                message: $"{categoryAddDto.Name} adlı kategori başarıyla eklenmiştir");
        }

        public async Task<IResult> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryUpdateDto.Id);
            if (category!=null)
            {
                category.Name = categoryUpdateDto.Name;
                category.Description = categoryUpdateDto.Description;
                category.Note = categoryUpdateDto.Note;
                category.IsActive = categoryUpdateDto.IsActive;
                category.IsDeleted = categoryUpdateDto.IsDeleted;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate=DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());
                return new Result(ResultStatus.Succes,
                    message: $"{categoryUpdateDto.Name} adlı kategori başarıyla güncellenmiştir");
            }
            return new Result>(ResultStatus.Error, "Kategori güncellenemedi", null);
        }

        public async Task<IResult> Delete(int categoryId, string modifiedByName)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category!=null)
            {
                category.IsDeleted = true;
                category.ModifiedByName = modifiedByName;
                category.ModifiedDate=DateTime.Now;
                await _unitOfWork.Categories.UpdateAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Succes, $"{category.Name} adlı kategori başarıyla silinmiştir");
            }

            return new Result(ResultStatus.Error, "Kategori silinemedi", null);
        }

        public async Task<IResult> HardDelete(int categoryId)
        {
            var category = await _unitOfWork.Categories.GetAsync(c => c.Id == categoryId);
            if (category != null)
            {
                
                await _unitOfWork.Categories.DeleteAsync(category).ContinueWith(t => _unitOfWork.SaveAsync());

                return new Result(ResultStatus.Succes, $"{category.Name} adlı kategori başarıyla veri tabanından silinmiştir");
            }


            return new Result(ResultStatus.Error, "Kategori veri tabanından silinemedi", null);
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        // Asenkron yapı olduğu için Task kullanıyoruz .

        Task<IDataResult<CategoryDto>> Get(int categoryId);
        
        Task<IDataResult<CategoryListDto>> GetAll();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeleted();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActive();
        Task<IDataResult<CategoryDto>>
            Add(CategoryAddDto categoryAddDto, string createdByName); // Data Transfer Object . ViewModel . FrontEnt tarafında sadece ihtiyaç duyulacak  alanları barındırır .

        Task<IDataResult<CategoryDto>> Update(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);  // IsDeleted Değerini değiştireceğiz . Geri dönülebilir .
        Task<IResult> HardDelete(int categoryId);// veri tabanından silinecek . 
    }
}

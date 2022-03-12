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

        Task<IDataResult<Category>> Get(int categoryId);
        Task<IDataResult<IList<Category>>> GetAll();
        Task<IDataResult<IList<Category>>> GetAllByNonDeleted();
        Task<IResult>
            Add(CategoryAddDto categoryAddDto, string createdByName); // Data Transfer Object . ViewModel . FrontEnt tarafında sadece ihtiyaç duyulacak  alanları barındırır .

        Task<IResult> Update(CategoryUpdateDto cteCategoryUpdateDto, string modifiedByName);
        Task<IResult> Delete(int categoryId, string modifiedByName);  // IsDeleted Değerini değiştireceğiz . Geri dönülebilir .
        Task<IResult> HardDelete(int categoryId);// veri tabanından silinecek . 
    }
}

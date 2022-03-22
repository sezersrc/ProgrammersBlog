using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Abstract
{
     public interface IUnitOfWork:IAsyncDisposable
    {
        IArticleRepository Articles { get; } // unitofwork.Articles diye property'lere erşiebileceğiz. 
        ICategoryRepository Categories { get; }
        ICommentRepository Comments { get; }
      
        Task<int> SaveAsync();

        //  _unitOfWork.Categories.AddAsync(category);
        //  _unitOfWork.Users.AddAsync(user);
        //  _unitOfWorkSaveAsync();
    }
}

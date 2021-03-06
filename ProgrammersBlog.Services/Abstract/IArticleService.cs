using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface IArticleService
    {
        Task<IDataResult<ArticleDto>> GetAsync(int articleId);
        Task<IDataResult<ArticleUpdateDto>> GetArticleUpdateDtoAsync(int articleId);
        Task<IDataResult<ArticleListDto>> GetAllAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByNonDeletedAndActiveAsync();
        Task<IDataResult<ArticleListDto>> GetAllByDeletedAsync();
        Task<IDataResult<ArticleListDto>> GetAllByViewCountAsync(bool isAscending, int? takeSize); // isAscending sıralama parametresi , takeSize kaç makale alınacağı . ? nulable yapıyor.
        Task<IDataResult<ArticleListDto>> GetAllByCategoryAsync(int categoryId);

        Task<IDataResult<ArticleListDto>> GetAllByPagingAsync(int? categoryId, int currentPage = 1, int pageSize = 5,
            bool isAscending = false);

        Task<IResult>
            AddAsync(ArticleAddDto articleAddDto, string createdByName, int userId);

        Task<IResult> UpdateAsync(ArticleUpdateDto articleUpdateDto, string modifiedByName);
        Task<IResult> DeleteAsync(int articleId, string modifiedByName);
        Task<IResult> HardDeleteAsync(int articleId);
        Task<IResult> UndoDeleteAsync(int articleId, string modifiedByName);
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}

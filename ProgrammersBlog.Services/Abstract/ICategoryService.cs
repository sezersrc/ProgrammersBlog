using ProgrammersBlog.Entities.Dtos;
using ProgrammersBlog.Shared.Utilities.Results.Abstract;
using System.Threading.Tasks;

namespace ProgrammersBlog.Services.Abstract
{
    public interface ICategoryService
    {
        // Asenkron yapı olduğu için Task kullanıyoruz .

        Task<IDataResult<CategoryDto>> GetAsync(int categoryId);

        /// <summary>
        /// Verilen ID parametresine ait kategorinin CategoryUpdateDto temsilini geriye döner.
        /// </summary>
        /// <param name="categoryId">0'dan büyük integer bir ID değeri</param>
        /// <returns>Asenktron bir operasyon ile Task olarak işlem sonucunu DataResult tipinde geriye döner.</returns>
        Task<IDataResult<CategoryUpdateDto>> GetCategoryUpdateDtoAsync(int categoryId);

        Task<IDataResult<CategoryListDto>> GetAllAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAsync();
        Task<IDataResult<CategoryListDto>> GetAllByNonDeletedAndActiveAsync();
        /// <summary>
        /// Verilen CategoryAddDto ve CreatedByName parametrelerine ait bilgiler ile yeni bir Category ekler.
        /// </summary>
        /// <param name="categoryAddDto">categodyAddDto tipinde eklenecek kategori bilgileri.</param>
        /// <param name="createdByName">string tipinde kullanıcı adı .</param>
        /// <returns>Asenktron bir operasyon ile Task olarak bizlere eklme işleminin sonucunu DateResult tipinde döner.</returns>
        Task<IDataResult<CategoryDto>>
            AddAsync(CategoryAddDto categoryAddDto, string createdByName); // Data Transfer Object . ViewModel . FrontEnt tarafında sadece ihtiyaç duyulacak  alanları barındırır .

        Task<IDataResult<CategoryDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto, string modifiedByName);
        Task<IDataResult<CategoryDto>> DeleteAsync(int categoryId, string modifiedByName);  // IsDeleted Değerini değiştireceğiz . Geri dönülebilir .
        Task<IResult> HardDeleteAsync(int categoryId);// veri tabanından silinecek . 
        Task<IDataResult<int>> CountAsync();
        Task<IDataResult<int>> CountByNonDeletedAsync();
    }
}

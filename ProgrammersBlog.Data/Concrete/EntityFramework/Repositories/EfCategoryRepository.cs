using Microsoft.EntityFrameworkCore;
using ProgrammersBlog.Data.Abstract;
using ProgrammersBlog.Data.Concrete.EntityFramework.Contexts;
using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Data.Concrete.EntityFramework;
using System.Threading.Tasks;

namespace ProgrammersBlog.Data.Concrete.EntityFramework.Repositories
{
    public class EfCategoryRepository : EfEntityRepositoryBase<Category>, ICategoryRepository
    {
        public EfCategoryRepository(DbContext context) : base(context)
        {
        }

        public async Task<Category> GetById(int categoryId)
        {
            //Örnek Method 

            return await ProgrammersBlogContext.Categories.SingleOrDefaultAsync(c => c.Id == categoryId);

        }

        private ProgrammersBlogContext ProgrammersBlogContext
        {  // Protected yaptık ki implement edebilelim.
            get
            {
                return _context as ProgrammersBlogContext;
            }

        }

    }
}

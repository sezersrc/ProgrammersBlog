using ProgrammersBlog.Shared.Entities.Abstract;
using System.Collections.Generic;

namespace ProgrammersBlog.Entities.Concrete
{
    public class Category : EntityBase, IEntity
    {
        /// public override string CreatedByName { get; set; } = "Admin"; Override Örneği;
        public string Name { get; set; }

        public string Description { get; set; }
        public ICollection<Article> Articles { get; set; }
    }
}

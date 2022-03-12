using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammersBlog.Entities.Dtos
{
    public class CategoryUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kategori Adı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir !..")]
        [MaxLength(70, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olamaz. ")]
        public string Name { get; set; }

        [DisplayName("Kategori Açıklaması")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır. ")]
        public string Description { get; set; }

        [DisplayName("Kategori Özel Not alanı")]
        [MaxLength(500, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır. ")]
        public string Note { get; set; }
        [DisplayName("Aktif mi ?")]
        [Required(ErrorMessage = "{0}  seçilmelidir !..")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi  mi ?")]
        [Required(ErrorMessage = "{0}  seçilmelidir !..")]
        public bool IsDeleted { get; set; }
    }
}

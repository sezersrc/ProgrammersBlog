using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProgrammersBlog.Entities.Dtos
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şuanki Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir !..")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz. ")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir !..")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz. ")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} boş geçilmemelidir !..")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olmamalıdır. ")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olamaz. ")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Girmiş olduğunuz Yeni Şifreniz ile Yeni Şifrenizin Tekrarı biribiriyle uyuşmalıdır")]
        public string RepeatPassword { get; set; }
    }
}

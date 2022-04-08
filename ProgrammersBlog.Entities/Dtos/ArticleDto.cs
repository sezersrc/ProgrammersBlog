using ProgrammersBlog.Entities.Concrete;
using ProgrammersBlog.Shared.Entities.Abstract;

namespace ProgrammersBlog.Entities.Dtos
{
    public class ArticleDto : DtoGetBase
    {
        public Article Article { get; set; }
        // Kullanım Örneği ; DtoBase  public override ResultStatus ResultStatus { get; set; } = ResultStatus.Succes;
    }
}

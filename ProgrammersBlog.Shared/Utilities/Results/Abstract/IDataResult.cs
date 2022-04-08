namespace ProgrammersBlog.Shared.Utilities.Results.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        // tek bir property içinde hem liste hem de tek bir entiti taşımak için out kullanıyoruz .

        public T Data { get; } // New DataResult<Category>(ResultStatus.Success,category);  başarılı şekilde geldiyse gönderiyoruz . 
        // New DataResult<IList<Category>>(ResultStatus.Success,categorylist); Liste Örneği IList yerine IEnumerable da olabilir . . 



    }
}

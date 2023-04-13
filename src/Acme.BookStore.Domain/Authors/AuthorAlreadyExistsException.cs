using Volo.Abp;

namespace Acme.BookStore.Authors
{
    // BusinessException 是一個特殊的異常類型. 在需要时抛出與領域相關的異常是一個好的練習. ABP框架會自動去處理他, 並且他也容易被本地化
    public class AuthorAlreadyExistsException : BusinessException
    {
        public AuthorAlreadyExistsException(string name)
            : base(BookStoreDomainErrorCodes.AuthorAlreadyExists)
        {
            WithData("name", name); // WithData(...) Method提供額外的資料給異常對象, 這些資料將會在本地化中或出於其它一些目的被使用
        }
    }
}

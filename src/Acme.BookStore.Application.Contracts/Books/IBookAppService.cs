using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Books
{
    public interface IBookAppService :
        ICrudAppService< //Defines CRUD methods 定義了基礎CRUD的方法
            BookDto, //Used to show books 用於在Interface Layer 顯示Book資訊
            Guid, //Primary key of the book entity 實體Book的主鍵
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateBookDto> //Used to create/update a book
    {

    }
}

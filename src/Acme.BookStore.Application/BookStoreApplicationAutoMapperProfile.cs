using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        // 由於存在資料庫之Entity Book類別 要傳輸到Interface Layer(表示層) 所以藉由AutoMapper函示庫自動轉換
        // Ex: CreateMap<來源,目標>();
        CreateMap<Book, BookDto>();
        // 由於會經由前端傳送資訊來更新 Book實體 所以此處使用AutoMapper自動轉換為Book
        CreateMap<CreateUpdateBookDto, Book>();
        // AuthorAppService 使用 ObjectMapper 將 Author 轉換為 AuthorDto. 所以我們需要在 AutoMapper 配置中定義他
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();

    }
}

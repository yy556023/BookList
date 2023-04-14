using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Books
{
    public class BookDto : AuditedEntityDto<Guid> // 為何DTO要繼承AuditedEntityDto而不是跟Entity一樣繼承AuditedAggregateRoot(聚合根)
    {
        public string? Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }

        // 新增作者屬性
        public Guid AuthorId { get; set; }

        public string? AuthorName { get; set; }
    }
}

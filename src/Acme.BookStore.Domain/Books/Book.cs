using System;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Books
{
    public class Book : AuditedAggregateRoot<Guid> // Guid是Entity(實體)的主键 (Id)
                                                   // AggregateRoot是一種特殊的實體，用於創建聚合的根實體
                                                   // 相比AggregateRoot類，AuditedAggregateRoot添加了更多属性：CreationTime、CreatorId、LastModificationTime和LastModifierId。
                                                   // 當您將實體体寫入資料庫时​​，ABP 會自動給這些属性赋值，CreationTime會設置為當前時間，CreatorId會自動設置為當前用戶的Id属性。
    {
        // 為了新增 Book與Author的關係 所以新增AuthorId屬性
        public Guid AuthorId { get; set; }

        public string Name { get; set; }

        public BookType Type { get; set; }

        public DateTime PublishDate { get; set; }

        public float Price { get; set; }
    }
}

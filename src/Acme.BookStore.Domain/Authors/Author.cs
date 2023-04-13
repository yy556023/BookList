using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Authors
{
    // 由於繼承了FullAuditedAggregateRoot<Guid> 所以Author這個Entity具有軟刪除的功能，且具備了審計功能
    // #軟刪除：表示資料在資料庫中只是被標記為刪除實際還是存在於資料庫 
    public class Author : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; } // Name屬性 設置為private set 限制外部更改這個屬性
        public DateTime BirthDate { get; set; }
        public string ShortBio { get; set; }

        private Author()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        // 建構式 和 ChangeName 方法的訪問修飾詞是 internal, 強制這些方法只能在Domain Layer經由 AuthorManager 使用.


        internal Author(
            Guid id,
            [NotNull] string name,
            DateTime birthDate,
            [CanBeNull] string shortBio = null)
            : base(id)
        {
            SetName(name);
            BirthDate = birthDate;
            ShortBio = shortBio;
        }

        internal Author ChangeName([NotNull] string name)
        {
            SetName(name);
            return this;
        }

        private void SetName([NotNull] string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: AuthorConsts.MaxNameLength
            );
        }
    }
}

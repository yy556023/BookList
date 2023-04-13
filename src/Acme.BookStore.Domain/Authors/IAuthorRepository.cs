using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Authors
{
    public interface IAuthorRepository : IRepository<Author, Guid>
    {
        // FindByNameAsync 在 AuthorManager 中用来根據姓名查詢作者
        Task<Author?> FindByNameAsync(string name);

        // GetListAsync 用於Application Layer以獲得一個排序的, 經過過濾的作者列表, 顯示在UI上.
        Task<List<Author>> GetListAsync(
            int skipCount,
            int maxResultCount,
            string sorting,
            string filter = null
        );
    }
}

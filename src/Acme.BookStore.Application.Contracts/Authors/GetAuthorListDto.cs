using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Authors
{
    // PagedAndSortedResultRequestDto 具有標準的分頁和排序属性: int MaxResultCount, int SkipCount 和 string Sorting.
    public class GetAuthorListDto : PagedAndSortedResultRequestDto
    {
        // Filter 用於查詢作者(sorting Method). 它可以是 null 或 string.Empty 以獲得所有用戶.
        public string Filter { get; set; }
    }
}

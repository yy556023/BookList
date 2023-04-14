using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Authors
{
    // IApplicationService 是一個標準的介面 所有ApplicationService都繼承他 所以ABP框架可以辨識他們
    public interface IAuthorAppService : IApplicationService
    {
        Task<AuthorDto> GetAsync(Guid id);

        // PagedResultDto 是一个ABP框架裡預設有的 DTO類. 他擁有一個 Items 集合 和一個 TotalCount 属性, 用於回傳分頁結果.
        Task<PagedResultDto<AuthorDto>> GetListAsync(GetAuthorListDto input);

        Task<AuthorDto> CreateAsync(CreateAuthorDto input);

        Task UpdateAsync(Guid id, UpdateAuthorDto input);

        Task DeleteAsync(Guid id);
    }
}

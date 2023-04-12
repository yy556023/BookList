using System;
using System.Threading.Tasks;
using Acme.BookStore.Books;
using Microsoft.AspNetCore.Mvc;

namespace Acme.BookStore.Web.Pages.Books
{
    public class EditModalModel : BookStorePageModel
    {
        // [HiddenInput] 和 [BindProperty]
        // 是標準的 ASP.NET Core MVC 特性.這裡啟用 SupportsGet 從Http Request的查詢字串參數中獲取ID的Value
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateUpdateBookDto Book { get; set; }

        private readonly IBookAppService _bookAppService;

        public EditModalModel(IBookAppService bookAppService)
        {
            _bookAppService = bookAppService;
        }

        // 在 OnGetAsync 方法中, 我們從 BookAppService 獲得 BookDto ,並將他映射成DTO對向 CreateUpdateBookDto.
        public async Task OnGetAsync()
        {
            var bookDto = await _bookAppService.GetAsync(Id);
            Book = ObjectMapper.Map<BookDto, CreateUpdateBookDto>(bookDto);
        }

        // OnPostAsync 方法直接使用 BookAppService.UpdateAsync 来更新實體
        public async Task<IActionResult> OnPostAsync()
        {
            await _bookAppService.UpdateAsync(Id, Book);
            return NoContent();
        }
    }
}

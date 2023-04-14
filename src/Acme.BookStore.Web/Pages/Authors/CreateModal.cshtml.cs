using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Acme.BookStore.Authors;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace Acme.BookStore.Web.Pages.Authors
{
    public class CreateModalModel : BookStorePageModel
    {
        [BindProperty]
        public CreateAuthorViewModel Author { get; set; } = new CreateAuthorViewModel();

        private readonly IAuthorAppService _authorAppService;

        public CreateModalModel(IAuthorAppService authorAppService)
        {
            _authorAppService = authorAppService;
        }

        public void OnGet()
        {
            Author = new CreateAuthorViewModel();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateAuthorViewModel, CreateAuthorDto>(Author!);
            await _authorAppService.CreateAsync(dto);
            return NoContent();
        }


        // 此處宣告一個新類別 CreateAuthorViewModel 是為了展示說 在一個Page裡面使用兩個不同的model 
        // 不過還有一個有益的地方是 我們class的成員裡新增了兩個attribute
        // [DataType(DataType.Date)] 此標籤可以讓UI顯示出datepicker
        // [TextArea] 可以讓原本的標準input box轉換成multi-line輸入

        public class CreateAuthorViewModel
        {
            [Required]
            [StringLength(AuthorConsts.MaxNameLength)]
            public string? Name { get; set; }

            [Required]
            [DataType(DataType.Date)]
            public DateTime BirthDate { get; set; } = DateTime.UtcNow;

            [TextArea]
            public string? ShortBio { get; set; }
        }
    }
}

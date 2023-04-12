using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Books
{
    public class CreateUpdateBookDto
    {
        // Attribute 屬性
        // [Required] 為必填 ABP會驗證是否填入
        // [StringLength(128)] 為限制最大長度為128個字元
        // [DataType(DataType.Date)] => 這串宣告不知道有甚麼用

        [Required]
        [StringLength(128)]
        public string Name { get; set; }

        [Required]
        public BookType Type { get; set; } = BookType.Undefined;

        [Required]
        [DataType(DataType.Date)]
        public DateTime PublishDate { get; set; } = DateTime.Now;

        [Required]
        public float Price { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace Acme.BookStore.Authors
{
    public class CreateAuthorDto
    {
        [Required] // 這些標籤可以用來驗證DTO的正確性
        [StringLength(AuthorConsts.MaxNameLength)]
        public string? Name { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        public string? ShortBio { get; set; }

        public Guid AuthorId { get; set; }
    }
}

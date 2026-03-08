using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.BookMarkNote
{
    public class UpdateBookMarkNoteDto
    {
        [Required(ErrorMessage = "is required field")]
        [StringLength(500, ErrorMessage = "too long content")]
        public string Content { get; set; }
        [Required]
        public int BookMarkId { get; set; }
    }
}

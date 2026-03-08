using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.BookMark
{
    public class UpdateBookMarkDTO
    {
        [Required(ErrorMessage = "is requiered field")]
        [MaxLength(100, ErrorMessage = "reached max length of note tittle")]
        public string Title { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}

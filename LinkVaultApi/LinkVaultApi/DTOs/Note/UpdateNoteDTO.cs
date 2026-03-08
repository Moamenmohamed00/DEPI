using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.Note
{
    public class UpdateNoteDTO
    {
        [Required(ErrorMessage = "is requiered field")]
        [MaxLength(100, ErrorMessage = "reached max length of note tittle")]
        public string Title { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "reached max length of note body")]
        public string Content { get; set; }
        [Required]
        public int CategoryID { get; set; }
    }
}

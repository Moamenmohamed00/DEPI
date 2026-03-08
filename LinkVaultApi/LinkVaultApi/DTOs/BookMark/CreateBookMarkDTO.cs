using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.BookMark
{
    public class CreateBookMarkDTO
    {
        [Required(ErrorMessage ="url is required")]
        [Url(ErrorMessage ="invalid url")]
        public string URL {  get; set; }
        [Required]
        [StringLength(100,ErrorMessage ="you reached max leangth of title")]
        public string BookMarkTitle { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }
}

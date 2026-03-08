using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.Category
{
    public class UpdateCtegoryDTO
    {
        [Required(ErrorMessage = "name is required")]//must be req because when map DB don't allow null
        [MaxLength(100)]
        public string CategoryName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string CategoryDescription { get; set; }
    }
}

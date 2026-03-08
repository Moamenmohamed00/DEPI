using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.Category
{
    public class CreateCategoryDTO
    {
        [Required(ErrorMessage ="name is required")]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }
    }
}

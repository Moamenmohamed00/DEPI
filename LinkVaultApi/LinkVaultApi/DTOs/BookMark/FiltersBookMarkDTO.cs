namespace LinkVaultApi.DTOs.BookMark
{
    public class FiltersBookMarkDTO
    {
        public bool? IsFavirote { get; set; }
        public bool? IsArchived { get; set; }
        public string? SerchTerm { get; set; }
        public string? CategoryName { get; set; }
    }
}

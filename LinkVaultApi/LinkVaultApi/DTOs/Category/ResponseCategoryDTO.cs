namespace LinkVaultApi.DTOs.Category
{
    public class ResponseCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CraetedAt { get; set; }
        public int BookMarkCount { get; set; }
        public int NotesCount { get; set; }
    }
}

namespace LinkVaultApi.DTOs.BookMark
{
    public class ResponseBookMarkDTO
    {
        public string URL { get; set; }
        public string Title { get; set; }
        public bool IsFavirote { get; set; }
        public bool IsArchived { get; set; }
        public string CategoryName { get; set; }
        public int BookMarNotesCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

namespace LinkVaultApi.DTOs.BookMarkNote
{
    public class ResponseBookMarkNoteDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string BookmarkTitle { get; set; }
    }
}

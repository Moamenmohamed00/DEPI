namespace LinkVaultApi.Models
{
    public class BookMarkNotes:BaseEntity
    {
        public string Content { get; set; }
        public BookMark BookMark { get; set; }
        public int BookMarkId { get; set; }
    }
}

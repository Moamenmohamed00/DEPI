namespace LinkVaultApi.DTOs.Note
{
    public class FiltersNoteDto//to use in query string to filter
    {
        public int? CategoryId { get; set; }
        public bool? IsPinned { get; set; }
        public string? SearchWord {  get; set; }
    }
}

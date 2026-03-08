using System.ComponentModel.DataAnnotations;

namespace LinkVaultApi.DTOs.Note
{
    public class ResponseNoteDTO
    {

        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }
        public string CategoryName { get; set; }
        public bool IsPinned { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

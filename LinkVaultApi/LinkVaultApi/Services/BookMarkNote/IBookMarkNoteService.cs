using LinkVaultApi.DTOs.BookMarkNote;
using LinkVaultApi.Models;

namespace LinkVaultApi.Services.BookMarkNote
{
    public interface IBookMarkNoteService
    {
        Task<List<ResponseBookMarkNoteDto>> GetAll(int BookMarkId);
        Task<ResponseBookMarkNoteDto> GetById(int BookMarkNoteId);
        Task<ResponseBookMarkNoteDto> Create(CreateBookMarkNoteDto dTO);
        Task<ResponseBookMarkNoteDto> Update(int BookMarkNoteId, UpdateBookMarkNoteDto dTO);
        Task<bool> Delete (int BookMarkNoteId);
    }
}

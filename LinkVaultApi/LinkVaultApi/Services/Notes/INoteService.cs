using LinkVaultApi.DTOs.Category;
using LinkVaultApi.DTOs.Note;

namespace LinkVaultApi.Services.Notes
{
    public interface INoteService
    {
        Task<List<ResponseNoteDTO>> GetAll(FiltersNoteDto filter);
        Task<ResponseNoteDTO> GetByID(int id);
        Task<ResponseNoteDTO> Create(CreateNoteDTO NewDto);
        Task<ResponseNoteDTO> Update(int id, UpdateNoteDTO NewNoteDTO);
        Task<bool> Delete(int id);
        Task<ResponseNoteDTO> TogglePinned(int id);//patch
    }
}

using LinkVaultApi.DTOs.BookMark;

namespace LinkVaultApi.Services.BookMark
{
    public interface IBookMarkService
    {
        Task<List<ResponseBookMarkDTO>> GetAll(FiltersBookMarkDTO filters);
        Task<ResponseBookMarkDTO> GetById(int id);
        Task<ResponseBookMarkDTO> Create(CreateBookMarkDTO dto);
        Task<ResponseBookMarkDTO> Update(int id,UpdateBookMarkDTO dto);
        Task<bool>Delete (int id);
        Task<ResponseBookMarkDTO> FaviroteToggle(int id);//patch
        Task<ResponseBookMarkDTO> ArchivedToggle(int id);//patch
    }
}

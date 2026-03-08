using LinkVaultApi.DTOs.Category;

namespace LinkVaultApi.Services.Category
{
    public interface ICategoryService
    {
        Task<List<ResponseCategoryDTO>> GetAll();
        Task<ResponseCategoryDTO>GetByID(int id);
        Task<ResponseCategoryDTO> Create(CreateCategoryDTO NewDto);
        Task<ResponseCategoryDTO> Update(int id,UpdateCtegoryDTO NewCtegoryDTO);
        Task<bool> Delete(int id);
    }
}

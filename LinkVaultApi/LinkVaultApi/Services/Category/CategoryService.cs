using LinkVaultApi.Data;
using LinkVaultApi.DTOs.Category;
using LinkVaultApi.Exceptions;
using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVaultApi.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        public CategoryService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseCategoryDTO> Create(CreateCategoryDTO NewDto)
        {
            if(NewDto == null)
                throw new ArgumentNullException(nameof(NewDto));
            if (NewDto.Name == null)
                throw new BadRequestException("name can't be null");
            var exist = _context.categoties.Any(c=>c.Name.ToLower()==NewDto.Name.ToLower());//to avoid upper characters
            if (exist)
                throw new DulipcateException($"category name:{NewDto.Name} is actually created");
            var category = new Categoty
            {
                Name = NewDto.Name,
                Description = NewDto.Description
            };
           await _context.categoties.AddAsync(category);
           await _context.SaveChangesAsync();
            //var res = new ResponseCategoryDTO { Name = category.Name
            //, Description = category.Description,
            //    CraetedAt = category.CreatedAt,
            //    BookMarkCount = category.bookMarks.Count,
            //    NotesCount = category.notes.Count,
            //};
            return new ResponseCategoryDTO//return await getbyid(category.id)
            {
                Id=category.Id,
                Name = category.Name,
                Description = category.Description,
                CraetedAt = category.CreatedAt,
                BookMarkCount = category.bookMarks.Count,
                NotesCount = category.notes.Count,
            }; ;
        }

        public async Task<bool> Delete(int id)
        {
            var c = await _context.categoties
                .Include(c => c.bookMarks).
                Include(c => c.notes)
                .FirstOrDefaultAsync(c => c.Id == id);
            if (c is null)
                throw new NotFoundException(nameof(c), id);
            if (c.bookMarks.Any() || c.notes.Any())
                throw new BadRequestException("can't delete category has book mark or note");
            _context.categoties .Remove(c);
           await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ResponseCategoryDTO>> GetAll()
        {
            return await _context.categoties
                .Include(c=>c.bookMarks)
                .Include(c=>c.notes)//to get count
                .OrderBy(c=>c.CreatedAt)
                .Select(c => new ResponseCategoryDTO
            {
                Id=c.Id,
                Name=c.Name,
                Description=c.Description,
                CraetedAt=c.CreatedAt,
                BookMarkCount=c.bookMarks.Count,
                NotesCount=c.notes.Count
            }).ToListAsync();//to convert querable to list//lazy loading
        }

        public async Task<ResponseCategoryDTO> GetByID(int id)
        {
            var c=await _context.categoties
                .Include(c=>c.bookMarks).
                Include(c=>c.notes)
                .FirstOrDefaultAsync(c=>c.Id==id);
            if (c is null)
                throw new NotFoundException(nameof(c),id);
            //var re = new ResponseCategoryDTO();
            //re.Id = id;
            //re.Name = c.Name;
            //re.Description = c.Description;
           return new ResponseCategoryDTO { 
               Id=c.Id,
               Name=c.Name,
               Description=c.Description,
               CraetedAt=c.CreatedAt,
               BookMarkCount= c.bookMarks.Count,
               NotesCount=c.notes.Count
           };
        }

        public async Task<ResponseCategoryDTO> Update(int id, UpdateCtegoryDTO NewCtegoryDTO)
        {
            var c = await _context.categoties.FindAsync(id);
            if (c is null)
                throw new NotFoundException(nameof(c), id);
            var exist = _context.categoties.Any(c => c.Name.ToLower() == NewCtegoryDTO.CategoryName.ToLower());//to avoid upper characters
            if (exist)
                throw new DulipcateException($"category name:{NewCtegoryDTO.CategoryName} is actually created");
            c.Name = NewCtegoryDTO.CategoryName;
            c.Description = NewCtegoryDTO.CategoryDescription;
            await _context.SaveChangesAsync();
            return new ResponseCategoryDTO
            {
                Id = id,
                Name = c.Name,
                Description = c.Description,
                CraetedAt = c.CreatedAt,
                BookMarkCount = c.bookMarks.Count,
                NotesCount = c.notes.Count
            };

        }
    }
}

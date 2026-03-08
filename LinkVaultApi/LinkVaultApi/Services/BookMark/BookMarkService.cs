using LinkVaultApi.Data;
using LinkVaultApi.DTOs.BookMark;
using LinkVaultApi.Exceptions;
using LinkVaultApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LinkVaultApi.Services.BookMark
{
    public class BookMarkService : IBookMarkService
    {
        private readonly AppDbContext _context;
        public BookMarkService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseBookMarkDTO> ArchivedToggle(int id)
        {
            var book = await GetById(id);
            book.IsArchived = !book.IsArchived;
            await _context.SaveChangesAsync();
            return book;
            //return new ResponseBookMarkDTO
            //{
            //    URL = book.URL,
            //    Title = book.Title,
            //    CategoryName = book.CategoryName,
            //    IsArchived = book.IsArchived,
            //    IsFavirote = book.IsFavirote,
            //    BookMarNotesCount = book.BookMarNotesCount,
            //    CreatedAt = book.CreatedAt,
            //};
        }

        public async Task<ResponseBookMarkDTO> Create(CreateBookMarkDTO dto)
        {
            if(dto == null)
                throw new BadRequestException($"can't send null request of {nameof(dto)}");
            if(_context.bookMarks.Any(b=>b.URL == dto.URL))
                    throw new DulipcateException("url is already entered");
            var bookmark = await _context.bookMarks.Include(b => b.Notes).FirstOrDefaultAsync(b=>b.CategoryId==dto.CategoryId);
            if (bookmark == null)
                throw new NotFoundException(nameof(bookmark),dto.CategoryId);
            _context.bookMarks.Add(bookmark);
            await _context.SaveChangesAsync();
            return new ResponseBookMarkDTO
            {
                URL = bookmark.URL,
                Title = bookmark.Title,
                CategoryName = bookmark.categoty.Name,
                IsArchived = bookmark.IsArchived,
                IsFavirote = bookmark.IsFavorite,
                BookMarNotesCount = bookmark.Notes.Count,
                CreatedAt = bookmark.CreatedAt,
            };
        }

        public async Task<bool> Delete(int id)
        {
            var exist = await _context.bookMarks.FindAsync(id);
            if (exist == null)
                throw new NotFoundException(nameof(exist), id);
            _context.bookMarks.Remove(exist);
            _context.SaveChanges();
            return true;
        }

        public async Task<ResponseBookMarkDTO> FaviroteToggle(int id)
        {
            var book = await GetById(id);
            book.IsFavirote = !book.IsFavirote;
           await _context.SaveChangesAsync();
            return book;
            //return new ResponseBookMarkDTO
            //{
            //    URL = book.URL,
            //    Title = book.Title,
            //    CategoryName = book.CategoryName,
            //    IsArchived = book.IsArchived,
            //    IsFavirote = book.IsFavirote,
            //    BookMarNotesCount = book.BookMarNotesCount,
            //    CreatedAt = book.CreatedAt,
            //};
        }
        public async Task<List<ResponseBookMarkDTO>> GetAll( FiltersBookMarkDTO filters)
        {
            var bookmarks = await _context.bookMarks.Include(b => b.Notes).ToListAsync();
            //var bookmarks =_context.bookMarks
            //    .Include(b=>b.Notes)
            //    .AsQueryable();
            if(filters.IsFavirote.HasValue)
                bookmarks= bookmarks.Where(b=>b.IsFavorite==filters.IsFavirote.Value).ToList();
            if(filters.IsArchived.HasValue)
                bookmarks=bookmarks.Where(b=>b.IsArchived==filters.IsArchived.Value).ToList();
            if(!string.IsNullOrWhiteSpace(filters.CategoryName))
                bookmarks=bookmarks.Where(b=>b.categoty.Name.Contains(filters.CategoryName)).ToList();
            if(!string.IsNullOrWhiteSpace(filters.SerchTerm))
                bookmarks=bookmarks.Where(b=>b.Title.Contains(filters.SerchTerm)||b.URL.Contains(filters.SerchTerm)).ToList();
            return  bookmarks.Select(b => new ResponseBookMarkDTO
            {
                URL = b.URL,
                Title = b.Title,
                CategoryName = b.categoty.Name,
                IsArchived = b.IsArchived,
                IsFavirote = b.IsFavorite,
                BookMarNotesCount = b.Notes.Count,
                CreatedAt = b.CreatedAt,
            }).ToList();

        }

        public async Task<ResponseBookMarkDTO> GetById(int id)
        {
            var exist =await _context.bookMarks.Include(b => b.Notes).FirstOrDefaultAsync(b => b.Id == id);
            if(exist == null)
                throw new NotFoundException(nameof(exist),id);
            return new ResponseBookMarkDTO
            {
                URL = exist.URL,
                Title = exist.Title,
                CategoryName = exist.categoty.Name,
                IsArchived = exist.IsArchived,
                IsFavirote = exist.IsFavorite,
                BookMarNotesCount = exist.Notes.Count,
                CreatedAt = exist.CreatedAt,
            };
        }

        public async Task<ResponseBookMarkDTO> Update(int id, UpdateBookMarkDTO dto)
        {
            var exist = await _context.bookMarks.Include(b => b.Notes).FirstOrDefaultAsync(b => b.Id == id);
            if (exist == null)
                throw new NotFoundException(nameof(exist), id);
            if(!_context.bookMarks.Any(b=>b.CategoryId==dto.CategoryID))
                throw new NotFoundException(nameof(dto), id);
            exist.Title = dto.Title;
            exist.CategoryId = dto.CategoryID;
            await _context.SaveChangesAsync();
            return new ResponseBookMarkDTO {
                URL = exist.URL,
                Title = exist.Title,
                CategoryName = exist.categoty.Name,
                IsArchived = exist.IsArchived,
                IsFavirote = exist.IsFavorite,
                BookMarNotesCount = exist.Notes.Count,
                CreatedAt = exist.CreatedAt,
            };
        }
    }
}

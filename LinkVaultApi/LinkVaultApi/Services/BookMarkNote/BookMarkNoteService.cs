using LinkVaultApi.Data;
using LinkVaultApi.DTOs.BookMarkNote;
using LinkVaultApi.DTOs.Note;
using LinkVaultApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LinkVaultApi.Services.BookMarkNote
{
    public class BookMarkNoteService : IBookMarkNoteService
    {
        private readonly AppDbContext _context;
        public BookMarkNoteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseBookMarkNoteDto> Create(CreateBookMarkNoteDto dTO)
        {
            if (dTO == null) throw new BadRequestException("can't ne null");
            var booknote = await _context.bookMarksNotes.Include(bn => bn.BookMark).FirstOrDefaultAsync(bn => bn.BookMarkId == dTO.BookMarkId);
            if (booknote is null)
                throw new NotFoundException(nameof(booknote),dTO.BookMarkId);
            _context.bookMarksNotes.Add(booknote);
            await _context.SaveChangesAsync();
            return new ResponseBookMarkNoteDto
            {
                Content = booknote.Content,
                Id = booknote.Id,
                BookmarkTitle = booknote.BookMark.Title,
                CreatedAt = booknote.CreatedAt,
            };
        }

        public async Task<bool> Delete(int BookMarkNoteId)
        {
            var exist = await _context.bookMarksNotes.FindAsync(BookMarkNoteId);
            if (exist == null)
                throw new NotFoundException(nameof(exist), BookMarkNoteId);
            _context.bookMarksNotes.Remove(exist);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<ResponseBookMarkNoteDto>> GetAll(int BookMarkId)
        {
            var booknotes = await _context.bookMarksNotes.Include(bn => bn.BookMark)
                .Where(bn => bn.BookMarkId == BookMarkId).ToListAsync();
            return booknotes
                .OrderByDescending(bn => bn.CreatedAt)
                .Select(bn => new ResponseBookMarkNoteDto
            {
                Content = bn.Content,
                Id = bn.Id,
                BookmarkTitle=bn.BookMark.Title,
                CreatedAt = bn.CreatedAt,
            }).ToList();

        }

        public async Task<ResponseBookMarkNoteDto> GetById(int BookMarkNoteId)
        {
            var exist = await _context.bookMarksNotes.FindAsync(BookMarkNoteId);
            if(exist == null)
                throw new NotFoundException(nameof(exist),BookMarkNoteId);
            return new ResponseBookMarkNoteDto
            {
                Content = exist.Content,
                Id = exist.Id,
                CreatedAt = exist.CreatedAt,
                BookmarkTitle = exist.BookMark.Title,
            };
        }

        public async Task<ResponseBookMarkNoteDto> Update(int BookMarkNoteId, UpdateBookMarkNoteDto dTO)
        {
            var exist = await _context.bookMarksNotes.FindAsync(BookMarkNoteId);
            if (exist == null)
                throw new NotFoundException(nameof(exist), BookMarkNoteId);
            if (!_context.bookMarksNotes.Any(bn => bn.BookMarkId == dTO.BookMarkId))
                throw new BadRequestException("book mark is not exsit");
            exist.Content = dTO.Content;
            exist.BookMarkId = dTO.BookMarkId;
            await _context.SaveChangesAsync();
            return new ResponseBookMarkNoteDto
            {
                Content = exist.Content,
                Id = exist.Id,
                CreatedAt = exist.CreatedAt,
                BookmarkTitle = exist.BookMark.Title,
            };
        }
    }
}

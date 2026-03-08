using LinkVaultApi.Data;
using LinkVaultApi.DTOs.Note;
using LinkVaultApi.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace LinkVaultApi.Services.Notes
{
    public class NoteService : INoteService
    {
        private readonly AppDbContext _context;
        public NoteService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<ResponseNoteDTO> Create(CreateNoteDTO NewDto)
        {
            var n = await _context.notes.Include(n => n.categoty).FirstOrDefaultAsync(n => n.CategoryId == NewDto.CategoryID);
            if (n == null)
                throw new NotFoundException(nameof(n), NewDto.CategoryID);
            _context.notes.Add(n);
            await _context.SaveChangesAsync();
            return new ResponseNoteDTO { 
                NoteTitle = n.Title,
                CategoryName=n.categoty.Name,
                CreatedAt = DateTime.UtcNow,
                IsPinned=n.IsPinned,
                NoteContent=n.Content
            };
            
        }

        public async Task<bool> Delete(int id)
        {
            var exist=await _context.notes.FindAsync(id);
            if(exist == null)
                throw new NotFoundException(nameof(exist),id);
            _context.notes.Remove(exist);
            _context.SaveChanges();
            return true;
        }

        public async Task<List<ResponseNoteDTO>> GetAll(FiltersNoteDto filter)
        {
            var notes = _context.notes
               .Include(n => n.categoty)
               .AsQueryable();
            if (filter.CategoryId.HasValue)
                notes = notes.Where(n => n.CategoryId == filter.CategoryId.Value);
            if (filter.IsPinned.HasValue)
                notes = notes.Where(n => n.IsPinned == filter.IsPinned.Value);
            if (!string.IsNullOrWhiteSpace(filter.SearchWord))
                notes = notes.Where(n => n.Title.Contains(filter.SearchWord) || n.Content.Contains(filter.SearchWord));
            return await notes.Select(n => new ResponseNoteDTO
            {
                NoteTitle = n.Title,
                NoteContent = n.Content,
                IsPinned = n.IsPinned,
                CategoryName = n.categoty.Name,
                CreatedAt = n.CreatedAt,
            }).ToListAsync();

            //return await _context.notes
            //    .Include(n => n.categoty)
            //    .Select(n => new ResponseNoteDTO
            //    {
            //        NoteTitle = n.Title,
            //        NoteContent = n.Content,
            //        IsPinned = n.IsPinned,
            //        CategoryName=n.categoty.Name,
            //        CreatedAt = n.CreatedAt,
            //    }).Where(n=>n.IsPinned==filter.IsPinned||n.NoteContent.Contains(filter.SearchWord)).ToListAsync();
        }

        public Task<ResponseNoteDTO> GetByID(int id)
        {
            var n=_context.notes.Include(n=>n.categoty).FirstOrDefault(n => n.Id == id);
            if(n == null)
                throw new NotFoundException(nameof(n),id);
            var res = new ResponseNoteDTO
            {
                NoteTitle = n.Title,
                NoteContent = n.Content,
                IsPinned = n.IsPinned,
                CategoryName = n.categoty.Name,
                CreatedAt = n.CreatedAt,
            };
            return Task.FromResult(res); 
        }

        public async Task<ResponseNoteDTO> TogglePinned(int id)
        {
            var note=await GetByID(id);//await to see props of note
            note.IsPinned = !note.IsPinned;
            await _context.SaveChangesAsync();
            return note;
        }

        public async Task<ResponseNoteDTO> Update(int id, UpdateNoteDTO NewNoteDTO)
        {
            var n = await _context.notes.Include(n => n.categoty).FirstOrDefaultAsync(n => n.Id ==id);
            if (n == null)
                throw new NotFoundException(nameof(n), id);
            if(!_context.notes.Any(n=>n.CategoryId== NewNoteDTO.CategoryID))
                throw new NotFoundException(nameof(n), NewNoteDTO.CategoryID);
            n.Title = NewNoteDTO.Title;
            n.Content = NewNoteDTO.Content;
            n.CategoryId = NewNoteDTO.CategoryID;
            await _context.SaveChangesAsync();
            return new ResponseNoteDTO
            {
                NoteTitle = n.Title,
                CategoryName = n.categoty.Name,
                CreatedAt = DateTime.UtcNow,
                IsPinned = n.IsPinned,
                NoteContent = n.Content
            };
        }
    }
}

using LinkVaultApi.DTOs.BookMarkNote;
using LinkVaultApi.Services.BookMarkNote;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkVaultApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkNoteController : ControllerBase
    {
        private readonly IBookMarkNoteService _bookMarkNoteService;
        public BookMarkNoteController(IBookMarkNoteService bookMarkNoteService)
        {
            _bookMarkNoteService = bookMarkNoteService;
        }
        // GET: api/<BookMarkNoteController>
        [HttpGet]
        public async Task<IActionResult> Get([FromBody]int bookmarkId)
        {
            return Ok(await _bookMarkNoteService.GetAll(bookmarkId));
        }

        // GET api/<BookMarkNoteController>/5
        [HttpGet("/BookNote/{id}")]
        public async Task<IActionResult> GetByid([FromBody]int bookmarknoteid)
        {
            return Ok(await _bookMarkNoteService.GetById(bookmarknoteid));
        }

        // POST api/<BookMarkNoteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookMarkNoteDto dto)
        {
            return Ok(await _bookMarkNoteService.Create(dto));
        }

        // PUT api/<BookMarkNoteController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBookMarkNoteDto dto)
        {
            return Ok(await _bookMarkNoteService.Update(id, dto));
        }

        // DELETE api/<BookMarkNoteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookMarkNoteService.Delete(id);
            return NoContent();
        }
    }
}

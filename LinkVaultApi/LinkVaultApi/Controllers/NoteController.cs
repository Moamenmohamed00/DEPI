using LinkVaultApi.DTOs.Note;
using LinkVaultApi.Services.Notes;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkVaultApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly INoteService _noteService;
        public NoteController(INoteService noteService)
        {
            _noteService = noteService;
        }
        // GET: api/<NoteController>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]FiltersNoteDto filters)//optional
        {
           return Ok(await _noteService.GetAll(filters));
        }

        // GET api/<NoteController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _noteService.GetByID(id));
        }

        // POST api/<NoteController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateNoteDTO dTO)
        {
            return Ok(await _noteService.Create(dTO));
        }

        // PUT api/<NoteController>/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateNoteDTO dTO)
        {
            return Ok(await _noteService.Update(id, dTO));
        }

        // DELETE api/<NoteController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            //     return Ok(await _noteService.Delete(id));//true ,false
            await _noteService.Delete(id);
       return Content("note deleted");
        }
    }
}

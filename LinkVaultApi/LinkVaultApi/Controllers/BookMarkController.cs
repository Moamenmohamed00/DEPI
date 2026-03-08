using LinkVaultApi.DTOs.BookMark;
using LinkVaultApi.Services.BookMark;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LinkVaultApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookMarkController : ControllerBase
    {
        private readonly IBookMarkService _bookmarkservice;
        public BookMarkController(IBookMarkService bookMarkService)
        {
            _bookmarkservice = bookMarkService;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] FiltersBookMarkDTO filters)
        {
            return Ok(await _bookmarkservice.GetAll(filters));
        }

        // GET api/<BookMarkController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _bookmarkservice.GetById(id));
        }

        // POST api/<BookMarkController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateBookMarkDTO dto)
        {
            return Ok(await _bookmarkservice.Create(dto));
        }

        // PUT api/<BookMarkController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateBookMarkDTO dto)
        {
            return Ok(await _bookmarkservice.Update(id, dto));
        }

        // DELETE api/<BookMarkController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _bookmarkservice.Delete(id);
            return NoContent();
        }
        [HttpGet("/Favorite/{id:int}")]
        public async Task<IActionResult> ToggleFavorite(int id)
        {
         return Ok(await _bookmarkservice.FaviroteToggle(id));
        }
        [HttpGet("/Archived/{id:int}")]
        public async Task<IActionResult> ToggleArchived(int id)
        {
         return Ok(await _bookmarkservice.ArchivedToggle(id));
        }
    }
}

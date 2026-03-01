using ApiSessions.Data;
using ApiSessions.Model;
using ApiSessions.Service;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ApiSessions.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        #region old
        /* //private readonly AppDbContext _context;
         public TaskItemController(AppDbContext context)
         {
             //_context = context;
         }
         // GET: api/<TaskItemController>
         [HttpGet]
         public async Task<ActionResult<List<TaskItem>>> Get()
         {
             //return Ok( await _context.tasks.ToListAsync());
         }

         // GET api/<TaskItemController>/5
         [HttpGet("{id}")]
         public async Task<ActionResult<TaskItem>> Get(int id)
         {
        //     var task= _context.tasks.FirstOrDefault(x => x.Id == id);
        //     if (task == null)
        //     {
        //         return BadRequest();
        //     }
        //return Ok(task);
         }

         // POST api/<TaskItemController>
         [HttpPost]
         public async Task<IActionResult> Post([FromBody] TaskItem newtask)
         //{
         //    await _context.tasks.AddAsync(newtask);
         //    _context.SaveChanges();
         //    return Created();
         }

         // PUT api/<TaskItemController>/5
         [HttpPut("{id}")]
         public async Task<IActionResult> Put(int id, [FromBody] TaskItem updateitem)
         {
             //var item= _context.tasks.FirstOrDefault(x=>x.Id == id);
             // item = new TaskItem
             //{
             //    Id = updateitem.Id,
             //    Title = updateitem.Title,
             //    Description = updateitem.Description,
             //    Created = updateitem.Created,
             //    Pirority = updateitem.Pirority,
             //    Status = updateitem.Status
             //};
             //_context.SaveChanges();
             //return CreatedAtAction(nameof(Get),new { id = updateitem.Id }, updateitem);
         }
         [HttpPatch("{id}")]//Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
         public async Task<IActionResult> PatchTask(int id, JsonPatchDocument<TaskItem> patchDoc)
         {
             //if (patchDoc == null)
             //    return BadRequest();

             //var taskItem = await _context.tasks.FindAsync(id);
             //if (taskItem == null)
             //    return NotFound();

             //patchDoc.ApplyTo(taskItem, (error) =>
             //{
             //    ModelState.AddModelError(error.AffectedObject?.ToString() ?? "", error.ErrorMessage);
             //});

             //if (!ModelState.IsValid)
             //    return BadRequest(ModelState);

             //await _context.SaveChangesAsync();

             //return Ok(taskItem);
         }

         // DELETE api/<TaskItemController>/5
         [HttpDelete("{id}")]
         public async Task<IActionResult> Delete(int id)
         {
             //var del= await _context.tasks.FindAsync(id);
             //if (del == null)
             //    return NotFound();
             //_context.tasks.Remove(del);
             //_context.SaveChanges();
             //return NoContent();
         }
        */
        #endregion
        #region after dependancy injection
        private readonly ItaskIService _taskService;
        private readonly ILogger<TaskItemController> _logger;//to make server logs listen to this controller and handle log level in the appsettings.json
        public TaskItemController(ItaskIService itaskIService,ILogger<TaskItemController> logger)
        {
            _taskService = itaskIService;   
            _logger = logger;//to inject by ctor
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> Get()
        {
            var tasks = await _taskService.GetAllAsync();
            return Ok(tasks);
        }
        // GET: api/taskitem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetById(int id)
        {
            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            return Ok(task);
        }

        // POST: api/taskitem
        [HttpPost]
        public async Task<ActionResult<TaskItem>> Create(TaskItem taskItem)
        {
            var task = await _taskService.CreateAsync(taskItem);
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        // PUT: api/taskitem/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TaskItem>> Update(int id, TaskItem newTask)
        {
            try
            {
                _logger.LogInformation($"Starting update operation for task with id {id}.");
                var updated = await _taskService.UpdateAsync(id, newTask);
                _logger.LogTrace($"Update operation for task with id {id} completed successfully.");
                return Ok(updated);
            }
            //catch(Exception ex)//first i catch all exception to handle any unexpected error that may occur during the update process, such as database connection issues, validation errors, or other unforeseen problems. This allows me to provide a more informative error message to the client and helps in debugging and troubleshooting the issue effectively.
            //{//then i handle specific exceptions like ArgumentException and NullReferenceException to provide more precise error messages related to invalid data or not found task, which can help the client understand the exact reason for the failure and take appropriate actions.
            //    throw new Exception($"An error occurred while updating task with id {id}.", ex);
            //}
            catch (ArgumentException ex)
            {
                //throw new Exception($"Invalid data for task with id {id}.", ex);
                return BadRequest($"Invalid data for task with id {id}. {ex.Message}");
            }
            catch (NullReferenceException ex)
            {
                _logger.LogWarning($"Warning Task with id {id} not found. {ex.Message}");
                _logger.LogError("An error occurred while updating task with id {id}.", ex);
                _logger.LogCritical($"Critical error occurred while updating task with id {id}.", ex);
                //   throw new Exception($"Task with id {id} not found.", ex);//throw thise message + excep in resonse and server
                return NotFound($"Task with id {id} not found. {ex.Message}");//return this meesage in response nothing in server
               // return StatusCode(404, $"Task with id {id} not found. {ex.Message}");
            }
            catch (Exception ex)//then make last specific if there is any other exception that i didn't handle before i catch it here to make sure that i handle all possible exceptions and provide a generic error message
            {
                throw new Exception($"An unexpected error occurred while updating task with id {id}.", ex);
                //return StatusCode(500, $"An unexpected error occurred while updating task with id {id}. {ex.Message}");
            }
            finally//always execute the code in the finally block regardless of whether an exception was thrown or not, ensuring that any necessary cleanup or finalization logic is executed, such as logging the completion of the update operation for the task with the specified id.
            {
                Console.WriteLine($"Update operation for task with id {id} has completed.");
            }
            //            if (updated == null)
            //                return NotFound();//this responce of not found if need specfic responce i use try-catch to threw excep
            //            /*{
            //    "type": "https://tools.ietf.org/html/rfc9110#section-15.5.5",
            //    "title": "Not Found",
            //    "status": 404,
            //    "traceId": "00-25ced850927bc2a7691c7d407e5e4126-5103a6212124361a-00"
            //}*/


         //   return Ok(updated);
        }
        /*
         * we need use logger and try-cath in many controller. we repeat ourself  
         update method call logger and service and handle exception so that not single responsablity to avoid this use global exception handling by middleware to handle all exception in one place and return custom error response to client and log the error in server logs to trace bugs and monitor the application performance and health.
         */
        // DELETE: api/taskitem/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _taskService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        // PATCH: api/taskitem/5
        [HttpPatch("{id}")]
        public async Task<ActionResult<TaskItem>> Patch(
            int id,
            [FromBody] JsonPatchDocument<TaskItem> patchDoc)
        {
            if (patchDoc == null)
                return BadRequest();

            var task = await _taskService.GetByIdAsync(id);

            if (task == null)
                return NotFound();

            patchDoc.ApplyTo(task, (error) =>
            {
                ModelState.AddModelError(error.AffectedObject?.ToString() ?? "", error.ErrorMessage);
            });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskService.UpdateAsync(id, task);

            return Ok(task);
        }
        #endregion
    }

}

using ApiSessions.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiSessions.Service
{
    public interface ItaskIService
    {
        //Task<ActionResult<List<TaskItem>>> getall();
        //Task<ActionResult<Task>>getById(int id);
        //Task<ActionResult<bool>> Delete(int id);
        //Task<ActionResult<bool>> Update(int id,TaskItem newtask);
        //Task<ActionResult<TaskItem>> Create(TaskItem taskItem);
        //controller who return action result
        Task<List<TaskItem>> GetAllAsync();
        Task<TaskItem?> GetByIdAsync(int id);
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<bool> DeleteAsync(int id);
        Task<TaskItem?> UpdateAsync(int id, TaskItem newTask);
    }
}

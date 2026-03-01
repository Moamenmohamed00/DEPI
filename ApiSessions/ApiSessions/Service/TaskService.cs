using ApiSessions.Data;
using ApiSessions.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ApiSessions.Service
{
    public class TaskService : ItaskIService
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            await _context.tasks.AddAsync(taskItem);
            _context.SaveChanges();
            return taskItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var t=await _context.tasks.FindAsync(id);
            if (t == null) 
                return false;
            return true;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return await _context.tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(int id)
        {
            return await _context.tasks.FindAsync(id);
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem newTask)
        {
            var oldTask = await _context.tasks.FirstOrDefaultAsync(x => x.Id == id);
            //if (oldTask == null)//to make try catch null exception
            //    return null;

            oldTask.Title = newTask.Title;
            oldTask.Description = newTask.Description;
            oldTask.Pirority = newTask.Pirority;
            oldTask.Status = newTask.Status;
            oldTask.Created = newTask.Created;

            await _context.SaveChangesAsync();

            return oldTask;
        }
    }
}

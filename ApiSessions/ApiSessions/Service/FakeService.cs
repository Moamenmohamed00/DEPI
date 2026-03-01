using ApiSessions.Model;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ApiSessions.Service
{
    public class FakeService : ItaskIService
    {
        private List<TaskItem> tasks =new List<TaskItem>
        {
            new TaskItem(){
                Id = 1,
                Title = "Test1",
                Description = "Test",
                Pirority="done",
                Status="good",
                Created= DateTime.Now
                },
            new TaskItem(){
                Id = 2,
                Title = "Test2",
                Description = "Test2",
                Pirority="in progress",
                Status="bad",
                Created= DateTime.Now
                }
        };
        //task mean aseprate thread if i remove async Cannot implicitly convert type 'ApiSessions.Model.TaskItem' to 'System.Threading.Tasks.Task<ApiSessions.Model.TaskItem>'
        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            if (taskItem == null)
            {
                throw new ArgumentNullException(nameof(taskItem));
            }
            tasks.Add(taskItem);
            return taskItem;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var task=tasks.FirstOrDefault(x => x.Id == id);
            if (task != null)
            {
                tasks.Remove(task);
                return true;
            }
            return false;
        }

        public async Task<List<TaskItem>> GetAllAsync()
        {
            return tasks.ToList();
        }

        public Task<TaskItem?> GetByIdAsync(int id)
        {
            return Task.FromResult(tasks.FirstOrDefault(x=>x.Id == id));
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem newTask)
        {
            var old=tasks.FirstOrDefault(x=>x.Id==id);
            if (old != null)
            {
                old.Title=newTask.Title;
                old.Description=newTask.Description;
                old.Created=newTask.Created;
                old.Status=newTask.Status;
                old.Pirority=newTask.Pirority;
                return old;
            }
            return null;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WorkBackend1.Data;
using WorkBackend1.Models;

namespace WorkBackend1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TaskController(AppDbContext context)
        {
            _context = context;
        }

        // GET all tasks for a user
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTasks(int userId)
        {
            var tasks = await _context.Tasks
                .Where(t => t.UserId == userId)
                .OrderBy(t => t.DueDate)
                .ToListAsync();
            return Ok(tasks);
        }

        // POST create a new task
        [HttpPost]
        public async Task<IActionResult> CreateTask(TaskItem task)
        {
            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // PUT mark task as complete
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            task.IsCompleted = !task.IsCompleted;
            await _context.SaveChangesAsync();
            return Ok(task);
        }

        // DELETE a task
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return NotFound();

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return Ok("Task deleted");
        }
    }
}
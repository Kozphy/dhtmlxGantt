using Microsoft.AspNetCore.Mvc;
using DHX.Gantt.Models;

namespace DHX.Gantt.Controllers
{

    // Produces Attr specify action returning type
    [Produces("application/json")]
    [Route("api/task")]
    public class TaskController : Controller
    {
        private readonly GanttContext _context;

        public TaskController(GanttContext context)
        {
            _context = context;
        }

        // Get api/task
        [HttpGet]
        public IEnumerable<WebApiLink> Get()
        {
            return (IEnumerable<WebApiLink>)_context.Tasks
                .Select(t => (WebApiTask)t)
                .ToList();
        }

        // Get api/task/5
        [HttpGet("{id}")]
        public Models.Task? Get(int id)
        {
            return _context.Tasks.Find(id);
        }

        // Post api/task
        [HttpPost]
        public ObjectResult Post(WebApiTask apiTask)
        {
            var newTask = (Models.Task)apiTask;

            _context.Tasks.Add(newTask);
            _context.SaveChanges();

            return Ok(new
            {
                tid = newTask.Id,
                action = "Inserted"
            });
        }

        // PUT api/task/5
        [HttpPut("{id}")]
        public ObjectResult? Put(int id, WebApiTask apiTask)
        {
            var updatedTask = (Models.Task)apiTask;
            var dbTask = _context.Tasks.Find(id);

            if (dbTask == null)
            {
                return null;
            }

            dbTask.Text = updatedTask.Text;
            dbTask.StartDate = updatedTask.StartDate;
            dbTask.Duration = updatedTask.Duration;
            dbTask.ParentId = updatedTask.ParentId;
            dbTask.Progress = updatedTask.Progress;
            dbTask.Type = updatedTask.Type;
            _context.SaveChanges();

            return Ok(new
            {
                action="updated"
            });
        }

        // DELETE api/task/5
        [HttpDelete("{id}")]
        public ObjectResult DeleteTask(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
                _context.SaveChanges();
            }

            return Ok(new
            {
                action = "deleted"
            });
        }
    }

}

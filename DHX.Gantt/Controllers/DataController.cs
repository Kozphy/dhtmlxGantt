using DHX.Gantt.Models;
using Microsoft.AspNetCore.Mvc;

namespace DHX.Gantt.Controllers
{

    [Produces("application/json")]
    [Route("api/data")]
    public class DataController : Controller
    {
        private readonly GanttContext _context;

        public DataController(GanttContext context)
        {
            _context = context;
        }

        // GET api/data
        [HttpGet]
        public object Get()
        {
            return new
            {
                data = _context.Tasks.Select(t => (WebApiTask)t),
                links = _context.Links.Select(l => (WebApiLink)l)
            };
        }
    }
}

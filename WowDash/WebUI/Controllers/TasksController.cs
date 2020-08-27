using Microsoft.AspNetCore.Mvc;
using System;
using WowDash.ApplicationCore.DTO;
using WowDash.ApplicationCore.Entities;
using WowDash.Infrastructure;

namespace WowDash.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public Guid InitializeTask(InitializeTaskRequest request)
        {
            var task = new Task(request.PlayerId, request.TaskType);

            _context.Tasks.Add(task);

            return task.Id;
        }

    }
}

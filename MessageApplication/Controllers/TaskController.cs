namespace MessageApplication.Controllers
{
    using MA.Data.Model;
    using MA.Service.TaskService;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }
        [HttpPost]
        public async Task<IActionResult> AddTask(TaskMessage taskMessage)
        {
            _taskService.AddTask(taskMessage);
            return Ok();
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _taskService.DeleteTask(id);
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTask(int id, TaskMessage taskMessage)
        {
            _taskService.UpdateTask(id, taskMessage);
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<TaskMessage>> GetTaskMessages()
        {
            
            return await _taskService.GetAllTaskMessages();
        }
    }
}

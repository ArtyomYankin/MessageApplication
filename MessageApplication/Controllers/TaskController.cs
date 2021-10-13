﻿namespace MessageApplication.Controllers
{
    using MA.Data.Model;
    using MA.Service.ApiService;
    using MA.Service.InitService;
    using MA.Service.TaskService;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
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
        private readonly IApiService _apiService;
        private readonly IInitOnStart _initOnStart;
        public TaskController(ITaskService taskService, IApiService apiService, IInitOnStart initOnStart)
        {
            _taskService = taskService;
            _apiService = apiService;
            _initOnStart = initOnStart;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> AddTask(TaskMessage taskMessage)
        {
            _taskService.AddTask(taskMessage);
            return Ok();
        }
        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeleteTask(int id)
        {
            _taskService.DeleteTask(id);
            return Ok();
        }
        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdateTask( TaskMessage taskMessage)
        {
            _taskService.UpdateTask(taskMessage);
            return Ok();
        }
        [HttpGet]
        public async Task<IEnumerable<TaskMessage>> GetTaskMessages(int userId)
        {
            
            return await _taskService.GetAllTaskMessagesById(userId);
        }
        [HttpGet]
        [Route("Weather")]
        public async Task<Main> GetWeatherForecast()
        {
            await _initOnStart.GetAllTaskMessagesWithReceiver();
            return null; // await _apiService.GetWeatherAsync();
        }
    }
}

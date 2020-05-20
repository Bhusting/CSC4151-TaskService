using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CSC4151_TaskService.Controllers
{
    [ApiController]
    [Route("Task")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskRepository _taskRepository;

        public TaskController(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        [HttpGet("House/{houseId}")]
        public async Task<List<Domain.Task>> GetTaskByHouse(Guid houseId)
        {
            var tasks = await _taskRepository.GetTaskByHouseId(houseId);

            return tasks;
        }
    }
}

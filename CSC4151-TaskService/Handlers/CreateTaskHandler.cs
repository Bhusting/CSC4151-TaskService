using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repositories;
using Domain;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Task = System.Threading.Tasks.Task;

namespace CSC4151_TaskService.Handlers
{
    public class CreateTaskHandler : IMessageHandler
    {
        private readonly ITaskRepository _taskRepository;

        public CreateTaskHandler(ILogger<CreateTaskHandler> logger, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(string messageBody)
        {
            var task = JsonConvert.DeserializeObject<TaskDTO>(messageBody);

            await _taskRepository.CreateTask(task);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Repositories;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CSC4151_TaskService.Handlers
{
    public class DeleteTaskHandler : IMessageHandler
    {
        private readonly ITaskRepository _taskRepository;

        public DeleteTaskHandler(ILogger<DeleteTaskHandler> logger, ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task Handle(string messageBody)
        {
            var taskId = JsonConvert.DeserializeObject<Guid>(messageBody);

            await _taskRepository.DeleteTask(taskId);
        }
    }
}

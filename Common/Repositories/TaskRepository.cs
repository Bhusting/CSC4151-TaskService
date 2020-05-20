using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common.Builders;
using Common.Clients;
using Common.Messaging;
using Domain;
using Microsoft.Azure.ServiceBus;
using Task = System.Threading.Tasks.Task;

namespace Common.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly SqlClient _sqlClient;
        private readonly IQueueClient _queueClient;

        public TaskRepository(SqlClient sqlClient, IQueueClient queueClient)
        {
            _sqlClient = sqlClient;
            _queueClient = queueClient;
        }

        public async Task<Domain.Task> GetTask(Guid taskId)
        {
            var cmd = SqlCommandBuilder.GetIndividualRecordBuilder(typeof(Domain.Task), taskId);

            var task = await _sqlClient.Get<Domain.Task>(cmd);

            return task[0];
        }

        public async Task<List<Domain.Task>> GetTaskByHouseId(Guid houseId)
        {
            var cmd = $"SELECT * FROM Task WHERE HouseId = \'{houseId}\'";

            var tasks = await _sqlClient.Get<Domain.Task>(cmd);

            return tasks;
        }

        public async Task CreateTask(TaskDTO taskDto)
        {
            var time = DateTime.Now.TimeOfDay.Add(TimeSpan.Parse(taskDto.Duration));

            var task = new Domain.Task();
            task.TaskId = taskDto.TaskId;
            task.HouseId = taskDto.HouseId;
            task.TaskName = taskDto.TaskName;
            task.EndTime = $"{time.Hours}:{time.Minutes}";

            var cmd = SqlCommandBuilder.InsertRecord(task);

            var workerTask = new WorkerTask();

            workerTask.TaskId = task.TaskId;
            workerTask.TaskName = task.TaskName;
            workerTask.EndTime = task.EndTime;
            workerTask.Channel = taskDto.Channel;

            await _sqlClient.Insert(cmd);

            var message = new Message().CreateMessage("AddEvent", workerTask);
            await _queueClient.SendAsync(message);
        }

        public async Task DeleteTask(Guid taskId)
        {
            var cmd = SqlCommandBuilder.DeleteRecord(typeof(Domain.Task), taskId);

            await _sqlClient.Delete(cmd);

            var message = new Message().CreateMessage("ClearEvents", "");
            await _queueClient.SendAsync(message);
        }
    }
}

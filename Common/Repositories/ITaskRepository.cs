using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain;
using Task = System.Threading.Tasks.Task;

namespace Common.Repositories
{
    public interface ITaskRepository
    {
        Task<Domain.Task> GetTask(Guid taskId);

        Task<List<Domain.Task>> GetTaskByHouseId(Guid houseId);

        Task CreateTask(TaskDTO task);

        Task DeleteTask(Guid taskId);
    }
}

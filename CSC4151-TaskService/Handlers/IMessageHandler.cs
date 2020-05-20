using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSC4151_TaskService.Handlers
{
    public interface IMessageHandler
    { Task Handle(string messageBody);
    }
}

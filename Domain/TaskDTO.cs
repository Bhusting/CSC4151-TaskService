using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class TaskDTO
    {
        public Guid TaskId { get; set; }

        public string TaskName { get; set; }

        public Guid HouseId { get; set; }

        public string Duration { get; set; }

        public Guid Channel { get; set; }
    }
}

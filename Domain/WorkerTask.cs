using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class WorkerTask
    {
        public Guid TaskId { get; set; }

        public string TaskName { get; set; }

        public string EndTime { get; set; }

        public Guid Channel { get; set; }
    }
}

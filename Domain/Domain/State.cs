using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{

    public partial class WorkflowStep
    {
        public enum State
        {
            Reject,
            InProgress,
            Approved
        }
    }
}

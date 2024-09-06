using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public partial class WorkflowStep
    {
        public string Description { get; private set; }
        public string Feedback { get; private set; }
        public byte Number { get; private set; }
        public State Status { get; private set; }
        public Guid RoleId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        public Workflow Workflow { get; private set; }
        public WorkflowStep(Workflow workflow,string description) 
        {
            Workflow = workflow;
            Status = State.InProgress;
            Description = description;
            RoleId = workflow.RoleId;
        }
        public void Aprove(Guid employeeId,string feedback)
        {
            EmployeeId = employeeId;
            Feedback = feedback;
            LastModifiedDate = DateTime.Now;
            Status = State.Approved;
            Workflow.Update();
        }
        public void Reject(Guid employeeId, string feedback)
        {
            EmployeeId = employeeId;
            Feedback = feedback;
            LastModifiedDate = DateTime.Now;
            Status = State.Reject;
            Workflow.Update();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Domain.WorkflowStep;

namespace Domain
{
    public class Workflow
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Feedback { get; private set; }
        public State Status { get; private set; }
        public Guid Id { get; private set; }
        public Guid RoleId { get; private set; }
        public Guid EmployeeId { get; private set; }
        public Guid CandidateId { get; private set; }
        public DateTime LastModifiedDate { get; private set; }
        private List<WorkflowStep> steps = new List<WorkflowStep>();
        public Workflow(WorkflowTemplate template,Guid candidateId) 
        {
            Name = template.Name;
            Description = template.Description;
            RoleId = template.RoleId;
            Id = Guid.NewGuid();
            CandidateId = candidateId;
        }

        public void Update()
        {
            LastModifiedDate = DateTime.Now;
            bool hasRejectedSteps = steps.Where(x => x.Status == State.Reject).Any();
            Status = hasRejectedSteps ? State.Reject : State.InProgress;
        }
        public void Aprove(Guid employeeId, string feedback)
        {
            EmployeeId = employeeId;
            Feedback = feedback;
            LastModifiedDate = DateTime.Now;
            Status = State.Approved;
        }
        public void Reject(Guid employeeId, string feedback)
        {
            EmployeeId = employeeId;
            Feedback = feedback;
            LastModifiedDate = DateTime.Now;
            Status = State.Reject;
        }
    }
}

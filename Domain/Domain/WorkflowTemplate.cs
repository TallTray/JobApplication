namespace Domain
{
    public class WorkflowTemplate
    {
        public string Name { get; init; }
        public Guid Id { get; init; }
        public Guid RoleId { get; init; }
        public string Description { get; init; }
        private List<WorkflowStep> steps = new List<WorkflowStep>();

        public WorkflowTemplate(string name,string description, Guid roleId)
        {
            Name = name;
            Description = description;
            Id = Guid.NewGuid();
            RoleId = roleId;
        }
        public Workflow CreateWorkflow(Guid candidateId)
        {
            return new(this, candidateId);
        }
    }
}
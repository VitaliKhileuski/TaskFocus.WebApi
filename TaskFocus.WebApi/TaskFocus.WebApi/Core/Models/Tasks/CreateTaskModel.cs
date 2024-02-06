namespace TaskFocus.WebApi.Core.Models.Tasks;

public class CreateTaskModel
{
    public Guid? LabelId { get; set; }
    
    public Guid? PriorityId { get; set; }
    
    public string Description { get; set; }
}
namespace LabWork_Tracker;

public class LabWork
{
    public Guid Id { get; set; }
    public string Subject { get; set; } 
    public string Title { get; set; } 
    public string? Description { get; set; }
    public DateTime Deadline { get; set; }
    public WorkStatus Status;

    public LabWork()
    {
        Id = Guid.NewGuid();
    }
}
namespace LabWork_Tracker;

public class WorkProgress
{
    public Guid StudentId { get; set; }
    public int LabId { get; set; }
    public WorkStatus Status { get; set; }
    public int? Grade { get; set; }
}
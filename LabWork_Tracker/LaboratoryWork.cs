namespace LabWork_Tracker;

public class LaboratoryWork
{
    public int Id { get; set; }
    public string Subject {get; set;}
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }

    public LaboratoryWork()
    {
        Id = new Random().Next(100000, 999999);
    }
}
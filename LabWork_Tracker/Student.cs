namespace LabWork_Tracker;

public class Student
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string GroupName { get; set; }
    public string StudentCardNumber { get; set; }

    public Student()
    {
        Id = Guid.NewGuid();
    }

}
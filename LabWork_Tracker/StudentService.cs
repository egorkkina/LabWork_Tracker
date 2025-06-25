using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker;

public class StudentService : IStudentService
{
    private List<Student> Students { get; set; } = new List<Student>();
    
    public void AddStudent(Student student)
    {
        Students.Add(student);
    }

    public void EditStudent(Guid id, Action<Student> editAction)
    {
        var student = Students.FirstOrDefault(x => x.Id == id);
        if (student != null)
        {
            editAction(student);
        }
    }

    public void RemoveStudent(Guid id)
    {
        Students.Remove(Students.Find(x => x.Id == id));
    }

    public List<Student> ShowAllStudents()
    {
        return Students;
    }
}
using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker.Services;

public class StudentService : IStudentService
{
    private readonly List<Student> _students = new();
    
    public bool AddStudent(Student student)
    {
        if (_students.Any(s => s.StudentCardNumber == student.StudentCardNumber))
            return false; 

        _students.Add(student);
        return true;
    }

    public bool EditStudent(Guid id, Action<Student> editAction)
    {
        var student = _students.FirstOrDefault(x => x.Id == id);
        if (student == null) 
            return false;
        
        editAction(student);
        return true;
    }

    public bool RemoveStudent(Guid id)
    {
        var student = _students.FirstOrDefault(x => x.Id == id);
        if (student == null) return false;
        _students.Remove(student);
        return true;
    }

    public List<Student> GetAllStudents()
    {
        return _students;
    }
}
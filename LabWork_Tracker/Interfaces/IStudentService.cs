namespace LabWork_Tracker.Interfaces;

public interface IStudentService
{
    bool AddStudent(Student student);
    bool EditStudent(Guid id, Action<Student> editAction);
    bool RemoveStudent(Guid id);
    List<Student> GetAllStudents();
}
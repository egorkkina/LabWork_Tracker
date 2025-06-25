namespace LabWork_Tracker.Interfaces;

public interface IStudentService
{
    void AddStudent(Student student);
    void EditStudent(Guid id, Action<Student> editAction);
    void RemoveStudent(Guid id);
    List<Student> ShowAllStudents();
}
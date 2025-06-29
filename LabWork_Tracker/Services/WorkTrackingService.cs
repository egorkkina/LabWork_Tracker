using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker.Services;

public class WorkTrackingService(StudentService studentService, LaboratoryWorkService labService)
    : IWorkTrackingService
{
    private readonly Dictionary<(Guid, int), WorkProgress> _workProgress = new();
    
    private readonly StudentService _studentService = studentService;
    private readonly LaboratoryWorkService _labService = labService;

    public bool Assign(Guid studentId, int labId)
    {
        var key = (studentId, labId);
        if (_workProgress.ContainsKey(key) || _studentService.GetAllStudents().All(s => s.Id != studentId) 
                                           || _labService.GetAllLaboratoryWorks().All(lw => lw.Id != labId))
        {
            return false;
        }
        _workProgress.Add(key, new WorkProgress {StudentId = studentId, LabId = labId, Status = WorkStatus.NotStarted});
        return true;
    }

    public bool UpdateStatus(Guid studentId, int labId, WorkStatus status)
    {
        if (!_workProgress.TryGetValue((studentId, labId), out var workProgress))
        {
            return false;
        }
        
        workProgress.Status = status;
        return true;
    }

    public bool SetGrade(Guid studentId, int labId, int grade)
    {
        if (!_workProgress.TryGetValue((studentId, labId), out var workProgress))
        {
            return false;
        }
        
        workProgress.Grade = grade;
        return true;
    }

    public List<WorkProgress> GetWorkProgress()
    {
        return _workProgress.Select(x => x.Value).ToList();
    }
}
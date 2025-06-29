namespace LabWork_Tracker.Interfaces;

public interface IWorkTrackingService
{
    bool Assign(Guid studentId, int labId);
    bool UpdateStatus(Guid studentId, int labId, WorkStatus status);
    bool SetGrade(Guid studentId, int labId, int grade);
    List<WorkProgress> GetWorkProgress();
}
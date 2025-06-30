using LabWork_Tracker.Interfaces;

namespace LabWork_Tracker.Services;

public class ReportService(
    IStudentService studentService,
    ILaboratoryWorkService labService,
    IWorkTrackingService workTrackingService)
    : IReportService
{

    private readonly IStudentService _studentService = studentService;
    private readonly ILaboratoryWorkService _labService = labService;
    private readonly IWorkTrackingService _workTrackingService = workTrackingService;

    public List<StudentStatistic> GetStudentStatistics()
    {
        var result = new List<StudentStatistic>();
        
        var students = _studentService.GetAllStudents();
        var allProgress = _workTrackingService.GetWorkProgress();

        foreach (var student in students)
        {
            var studentProgress = allProgress
                .Where(x => x.StudentId == student.Id).ToList();

            int totalLabs = studentProgress.Count;
            var submittedLabs = studentProgress
                .Where(x => x.Grade > 0 && (x.Status == WorkStatus.Submitted || x.Status == WorkStatus.Verified))
                .ToList();
            int submittedCount = submittedLabs.Count;
            
            double? averageGrade = submittedCount > 0 ? submittedLabs.Average(x => x.Grade) : 0;
            
            result.Add(new StudentStatistic
            {
                FullName = student.FullName,
                TotalWorks = totalLabs,
                SubmittedWorks = submittedCount,
                AverageGrade = averageGrade
            });
        }
        
        return result;
    }

    public List<LabStatistic> GetLabStatistics()
    {
        var result = new List<LabStatistic>();
        var labs = _labService.GetAllLaboratoryWorks();
        var allProgress = _workTrackingService.GetWorkProgress();

        foreach (var lab in labs)
        {
            var labProgress = allProgress
                .Where(x => x.LabId == lab.Id).ToList();
            int totalLabs = labProgress.Count;
            
            var submittedLabs = labProgress
                .Where(x => x.Grade > 0 && (x.Status == WorkStatus.Submitted || x.Status == WorkStatus.Verified))
                .ToList();
            int submittedCount = submittedLabs.Count;

            double percentageCompletion = (submittedCount / totalLabs) * 100;
            
            double? averageGrade = submittedLabs.Average(x => x.Grade);
            
            result.Add(new LabStatistic
            {
                SubjectName = lab.Name,
                TotalStudent = totalLabs,
                SubmittedWorks = submittedCount,
                PercentageCompletion = percentageCompletion,
                AverageGrade = averageGrade
            });
        }
        return result;
    }
}
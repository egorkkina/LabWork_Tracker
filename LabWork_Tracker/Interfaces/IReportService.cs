namespace LabWork_Tracker.Interfaces;

public interface IReportService
{
    List<StudentStatistic> GetStudentStatistics();
    List<LabStatistic> GetLabStatistics();
}
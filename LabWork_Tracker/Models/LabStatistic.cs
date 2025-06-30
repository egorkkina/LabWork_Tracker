namespace LabWork_Tracker;

public class LabStatistic
{
    public string SubjectName { get; set; } = "";
    public int TotalStudent { get; set; }
    public int SubmittedWorks { get; set; }
    public double PercentageCompletion { get; set; }
    public double? AverageGrade { get; set; }
}
using LabWork_Tracker.Interfaces;
using LabWork_Tracker.Services;

namespace LabWork_Tracker.Menus;

public static class ReportMenu
{
    public static void ShowReportMenu(IReportService reportService)
    {
        while (true)
        {
            Console.WriteLine("- - - Отчёты и аналитика - - -");
            Console.WriteLine("1. Статистика по студентам");
            Console.WriteLine("2. Статистика по лабораторным работам");
            Console.WriteLine("0. Назад");
            Console.Write("Выберите пункт: ");

            switch (Console.ReadLine())
            {
                case "1":
                    PrintStudentStatistics(reportService);
                    break;
                case "2":
                    PrintLabStatistics(reportService);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private static void PrintStudentStatistics(IReportService reportService)
    {
        var studentStatistics = reportService.GetStudentStatistics();
        Console.WriteLine("Имя студента              | Сдано работ | Средний балл");
        Console.WriteLine("--------------------------|-------------|---------------");
        foreach (var student in studentStatistics)
        {
            string submitted = $"{student.SubmittedWorks}/{student.TotalWorks}";
            Console.WriteLine($"{student.FullName,-26}| {submitted,-11} | {student.AverageGrade,13:F1}");
        }
    }

    private static void PrintLabStatistics(IReportService reportService)
    {
        var labStatistics = reportService.GetLabStatistics();
        Console.WriteLine("Предмет              | Сдано работ  | Процент выполнения | Средний балл");
        Console.WriteLine("---------------------|--------------|---------------------|--------------");
        foreach (var lab in labStatistics)
        {
            string submitted = $"{lab.SubmittedWorks}/{lab.TotalStudent}";
            Console.WriteLine($"{lab.SubjectName,-21} | {submitted,-12}| {lab.PercentageCompletion,19:F1} % | {lab.AverageGrade,12:F1}");
        }
    }
    
}
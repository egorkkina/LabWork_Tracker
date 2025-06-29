using LabWork_Tracker.Services;

namespace LabWork_Tracker;

public static class WorkProgressMenu
{
    public static void ShowWorkProgressMenu(WorkTrackingService service)
    {
        while (true)
        {
            Console.WriteLine("--- Учёт выполнения лабораторных работ ---");
            Console.WriteLine("1. Привязка студента к лабораторной работе\n" +
                              "2. Отметка статуса выполнения\n" +
                              "3. Добавление оценки за работу\n" +
                              "4. Все статусы и оценки\n" +
                              "0. Назад в главное меню");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AssignStudentLab(service);
                    break;
                case "2":
                    UpdateStatusLab(service);
                    break;
                case "3":
                    AddGrade(service);
                    break;
                case "4":
                    ShowProgress(service);
                    break;
                case "0":
                    return;
            }
        }
    }

    private static void AssignStudentLab(WorkTrackingService service)
    {
        Console.WriteLine("--- Привязка студента к лабораторной работе ---");
        var studentId = InputHelper.PromptValidStudentId("Введите ID студента: ");
        var labId = InputHelper.PromptValidLabId("Введите ID лабораторной работы: ");
        if (!service.Assign(studentId, labId))
        {
            Console.WriteLine("Не удалось прязать студента к лабораторной работе.");
            return;
        }
        
        Console.WriteLine($"Студент [ {studentId} ] успешно привязан к лабораторной работе [ {labId} ]");
    }

    private static void UpdateStatusLab(WorkTrackingService service)
    {
        Console.WriteLine("--- Отметка статуса выполнения ---");
        var studentId = InputHelper.PromptValidStudentId("Введите ID студента: ");
        var labId = InputHelper.PromptValidLabId("Введите ID лабораторной работы: ");
        WorkStatus workStatus = PromptWorkStatus();
        if (!service.UpdateStatus(studentId, labId, workStatus))
        {
            Console.WriteLine("Не удалось обновить статус работы. Проверьте корректность вводимых данных.");
            return;
        }
        
        Console.WriteLine($"Статус работы у студента [ {studentId} ] по работе [ {labId} ] \n" +
                          $"обновлён на [ {workStatus} ]");
    }

    private static void AddGrade(WorkTrackingService service)
    {
        Console.WriteLine("--- Добавление оценки за работу ---");
        var studentId = InputHelper.PromptValidStudentId("Введите ID студента: ");
        var labId = InputHelper.PromptValidLabId("Введите ID лабораторной работы: ");
        var grade = InputHelper.PromptGrade("Введите оценку за лабораторную работу (0 - 20): ");
        
        if (!service.SetGrade(studentId, labId, grade))
        {
            Console.WriteLine("Не удалось добавить оценку за работу. Проверьте корректность вводимых данных.");
            return;
        }
        
        Console.WriteLine($"Студенту [ {studentId} ] за лабораторную работу [ {labId} ] \n" +
                          $"присвоена оценка [ {grade} ]");
    }

    private static WorkStatus PromptWorkStatus()
    {
        while (true)
        {
            Console.Write("Выберите статус выполнения:\n" +
                          "1. Не начато\n2. В процессе\n3. Сдано\n4. Проверено\n > ");

            switch (Console.ReadLine())
            {
                case "1":
                    return WorkStatus.NotStarted;
                case "2":
                    return WorkStatus.InProgress;
                case "3":
                    return WorkStatus.Submitted;
                case "4":
                    return WorkStatus.Verified;
                default:
                    Console.WriteLine("Неверный ввод. Повторите попытку.");
                    break;
            }
        }
    }

    private static void ShowProgress(WorkTrackingService service)
    {
        Console.WriteLine("--- Статус и оценки студентов за лабораторную работу ---");
        Console.WriteLine("{0,-50} | {1,-40} | {2,-30} | {3, -40}", "ID студента", "ID лабораторной работы", "Статус выполнения", "Оценка");
        Console.WriteLine(new string('-', 160));
        
        foreach (var student in service.GetWorkProgress())
        {
            Console.WriteLine("{0,-50} | {1,-40} | {2,-30} | {3, -30}", student.StudentId, student.LabId, student.Status, student.Grade);
        }
    }
    
}
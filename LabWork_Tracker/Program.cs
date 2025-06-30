using System;
using LabWork_Tracker;
using LabWork_Tracker.Interfaces;
using LabWork_Tracker.Menus;
using LabWork_Tracker.Services;

class Program
{
    static void Main(string[] args)
    {
        IStudentService serviceStudent = new StudentService();
        ILaboratoryWorkService serviceLaboratoryWork = new LaboratoryWorkService();
        IWorkTrackingService serviceWorkTracking = new WorkTrackingService(serviceStudent, serviceLaboratoryWork);
        IReportService reportService = new ReportService(serviceStudent, serviceLaboratoryWork, serviceWorkTracking);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("- - -  Главное меню - - - ");
            Console.WriteLine("1. Управление студентами\n" +
                              "2. Управление лабораторными работами\n" +
                              "3. Учёт выполнения лабораторных работ\n" +
                              "4. Отчёты и аналитика\n" +
                              "0. Выход");
            Console.Write("Выберите нужный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    StudentMenu.ShowStudentMenu(serviceStudent);
                    break;
                case "2":
                    LaboratoryWorkMenu.ShowMenuLaboratoryWork(serviceLaboratoryWork);
                    break;
                case "3":
                    WorkProgressMenu.ShowWorkProgressMenu(serviceWorkTracking);
                    break;
                case "4":
                    ReportMenu.ShowReportMenu(reportService);
                    break;
                case "0":
                    return;
            }
        }
    }
}
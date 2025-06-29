using System;
using LabWork_Tracker;
using LabWork_Tracker.Services;

class Program
{
    static void Main(string[] args)
    {
        StudentService serviceStudent = new StudentService();
        LaboratoryWorkService serviceLaboratoryWork = new LaboratoryWorkService();
        WorkTrackingService serviceWorkTracking = new WorkTrackingService(serviceStudent, serviceLaboratoryWork);

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
                case "0":
                    return;
            }
        }
    }
}
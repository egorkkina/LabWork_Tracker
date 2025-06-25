using System;
using LabWork_Tracker;

class Program
{
    static void Main(string[] args)
    {
        StudentService service = new StudentService();

        while (true)
        {
            Console.Clear();
            Console.WriteLine("- - -  Главное меню - - - ");
            Console.WriteLine("1. Управление студентами\n" +
                              "2. Управление лабораторными работами\n" +
                              "3. Учёт выполнения лабораторных работ\n" +
                              "0. Выход");
            Console.Write("Выберите нужный вариант: ");

            switch (Console.ReadLine())
            {
                case "1":
                    StudentMenu.ShowStudentMenu(service);
                    break;
            }
        }
    }
}
using LabWork_Tracker.Services;

namespace LabWork_Tracker;

public static class StudentMenu
{
    public static void ShowStudentMenu(StudentService service)
    {
        while (true)
        {
            Console.WriteLine("--- Управление студентами ---");
            Console.WriteLine("1. Добавить студента");
            Console.WriteLine("2. Показать всех студентов");
            Console.WriteLine("3. Удалить студента");
            Console.WriteLine("4. Редактировать студента");
            Console.WriteLine("0. Назад в главное меню");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AddNewStudent(service);
                    break;
                case "2":
                    ShowStudent(service);
                    break;
                case "3":
                    DeleteStudent(service);
                    break;
                case "4":
                    EditStudentMenu(service);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод.");
                    break;
            }
        }
    }

    private static void AddNewStudent(StudentService service)
    {
        Student newStudent = CreateStudent();
        if (!service.AddStudent(newStudent))
        {
            Console.WriteLine("Проверьте корректность вводимых данных. \n" +
                              "Студент с таким номером студенческого уже существует");
            return;
        }
        service.AddStudent(newStudent);
        Console.WriteLine($"Студент [ {newStudent.FullName} ] добавлен");
    }

    private static void ShowStudent(StudentService service)
    {
        if (service.GetAllStudents().Count == 0)
        {
            Console.WriteLine("Список студентов пуст.");
            return;
        }
        
        Console.Write("--- Список всех студентов ---\n");
        Console.WriteLine("{0,-40} | {1,-40} | {2,-10} | {3, 0}", "ID", "ФИО", "Группа", "Номер студенческого билета");
        Console.WriteLine(new string('-', 140));
        foreach (var student in service.GetAllStudents())
        {
            Console.WriteLine("{0,-40} | {1,-40} | {2,-10} | {3, 0}", student.Id, student.FullName, student.GroupName, student.StudentCardNumber);
        }
    }

    private static void DeleteStudent(StudentService service)
    {
        Guid id = InputHelper.PromptValidStudentId("Введите ID студента, которого хотите удалить: ");
        if (!service.RemoveStudent(id))
        {
            Console.WriteLine("Студент с данным ID не найден");
            return;
        }
        Console.WriteLine($"Студент с ID: {id} был удалён");
    }
    
    private static Student CreateStudent()
    {
        Console.Clear();
        Console.WriteLine("--- Добавление нового студента ---");
        var newStudentName = InputHelper.PromptNonEmpty("Введите ФИО студента: ");
        var newStudentGroupName = InputHelper.PromptNonEmpty("Введите группу студента: ");
        var newStudentCard = InputHelper.PromptCardNumber("Введите номер студенческого билета (6 цифр): ");

        return new Student
        {
            FullName = newStudentName,
            GroupName = newStudentGroupName,
            StudentCardNumber = newStudentCard
        };
    }
    
    private static void EditStudentMenu(StudentService service)
    {
        Guid id = InputHelper.PromptValidStudentId("Введите ID студента, чьи данные хотите изменить: ");
        var student = service.GetAllStudents().FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            Console.WriteLine("Студент не найден.");
            return;
        }
        EntryStudent(student, service, id);
    }

    private static void EntryStudent(Student student, StudentService service, Guid id)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Редактирование студента: {student.FullName}");
            Console.WriteLine($"1. ФИО: {student.FullName}\n" +
                              $"2. Группа: {student.GroupName}\n" +
                              $"3. Студ. билет: {student.StudentCardNumber}\n" +
                              $"0. Завершить редактирование");
            Console.Write("Выберите поле для изменения: ");
            
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var newName = InputHelper.PromptNonEmpty("Введите новое ФИО: ");
                    service.EditStudent(id, s => s.FullName = newName);
                    break;
                case "2":
                    var newGroupName = InputHelper.PromptNonEmpty("Введите новую группу: ");
                    service.EditStudent(id, s => s.GroupName = newGroupName);
                    break;
                case "3":
                    var newCardNumber = InputHelper.PromptCardNumber(
                        "Введите новый номер студенческого билета: ",
                        number => service.GetAllStudents().Any(s => s.StudentCardNumber == number && s.Id != id)
                    );
                    service.EditStudent(id, s => s.StudentCardNumber = newCardNumber);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
    
    
}
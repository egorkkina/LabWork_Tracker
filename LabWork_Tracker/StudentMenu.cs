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
                    Student newStudent = CreateStudent();
                    service.AddStudent(newStudent);
                    Console.WriteLine("Студент успешно добавлен");
                    break;
                case "2":
                    Console.WriteLine("--- Список всех студентов ---\n");
                    Console.WriteLine("{0,-40} | {1,-40} | {2,-10} | {3, 0}", "ID", "ФИО", "Группа", "Номер студенческого билета");
                    Console.WriteLine(new string('-', 140));
                    foreach (var student in service.ShowAllStudents())
                    {
                        Console.WriteLine("{0,-40} | {1,-40} | {2,-10} | {3, 0}", student.Id, student.FullName, student.GroupName, student.StudentCardNumber);
                    }
                    break;
                case "3":
                    Console.Write("Введите ID студента, которого хотите удалить: ");
                    if (Guid.TryParse(Console.ReadLine(), out var id))
                    {
                        service.RemoveStudent(id);
                        Console.WriteLine($"Студент с ID: {id} был удалён");
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат GUID.");
                    }
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
    
    public static Student CreateStudent()
    {
        Console.Clear();
        Console.WriteLine("| Добавление нового студента |");
        Console.Write("Введите ФИО студента: ");
        var newStudentName = Console.ReadLine();
        Console.Write("Введите группу студента: ");
        var newStudentGroupName = Console.ReadLine();
        Console.Write("Введите номер студенческого билета: ");
        var newStudentCard = Console.ReadLine();

        return new Student
        {
            FullName = newStudentName,
            GroupName = newStudentGroupName,
            StudentCardNumber = newStudentCard
        };
    }
    
    public static void EditStudentMenu(StudentService service)
    {
        Console.WriteLine("Введите ID студента, чьи данные хотите изменить: ");
        if (!Guid.TryParse(Console.ReadLine(), out Guid id))
        {
            Console.WriteLine("Неверный формат ID");
            return;
        }
        
        var student = service.ShowAllStudents().FirstOrDefault(s => s.Id == id);
        if (student == null)
        {
            Console.WriteLine("Студент не найден.");
            return;
        }

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
                    Console.Write("Введите новое ФИО: ");
                    var newName = Console.ReadLine();
                    service.EditStudent(id, s => s.FullName = newName);
                    break;
                case "2":
                    Console.Write("Введите новую группу: ");
                    var newGroupName = Console.ReadLine();
                    service.EditStudent(id, s => s.GroupName = newGroupName);
                    break;
                case "3":
                    Console.Write("Введите новый номер студенческого билета: ");
                    var newStudentCardNumber = Console.ReadLine();
                    service.EditStudent(id, s => s.StudentCardNumber = newStudentCardNumber);
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
namespace LabWork_Tracker;

public class LaboratoryWorkMenu
{
    public static void ShowMenuLaboratoryWork(LaboratoryWorkService service)
    {
        while (true)
        {
            Console.WriteLine("--- Управление лабораторными работами ---");
            Console.WriteLine("1. Добавить лабораторную работу\n" +
                              "2. Просмотр всех лабораторных работ\n" +
                              "3. Удалить лабораторную работу\n" +
                              "4. Редактирование информации о лабораторной работе\n" +
                              "0. Назад в главное меню");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    service.AddLaboratoryWork(CreateLaboratoryWork());
                    break;
                case "2":
                    ShowLaboratoryWork(service);
                    break;
                case "3":
                    DeleteLaboratoryWork(service);
                    break;
                case "4":
                    ModifyLaboratoryWork(service);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный ввод");
                    break;
            }
        }
    }

    public static LaboratoryWork CreateLaboratoryWork()
    {
        Console.Clear();
        Console.WriteLine("| Добавление лабораторной работы |");
        Console.Write("Введите предмет, по которому будет выдана ЛР: ");
        var newSubject = Console.ReadLine();
        Console.Write("Введите название ЛР: ");
        var newName = Console.ReadLine();
        Console.WriteLine("Заполните описание данной ЛР: ");
        var newDescription = Console.ReadLine();
        Console.WriteLine("Введите дедлайн по данной работе (гггг-мм-дд): ");
        DateTime newDeadline = DateTime.Parse(Console.ReadLine());
        
        Console.WriteLine($"Лабораторная работа [ {newName} ] была добавлена");

        return new LaboratoryWork()
        {
            Subject = newSubject,
            Name = newName,
            Description = newDescription,
            Deadline = newDeadline
        };
        
    }

    public static void ShowLaboratoryWork(LaboratoryWorkService service)
    {
        Console.WriteLine("--- Информация по лабораторным работам ---\n");
        Console.WriteLine("{0,-10} | {1,-50} | {2,-50} | {3, -60} | {4, 0}", "ID", "Предмет", "Название", "Описание", "Дедлайн");
        Console.WriteLine(new string('-', 200));
        foreach (var lr in service.ShowAllLaboratoryWorks())
        {
            Console.WriteLine("{0,-10} | {1,-50} | {2,-50} | {3, -60} | {4, 0}", lr.Id, lr.Subject, lr.Name, lr.Description, lr.Deadline);
        }
    }

    public static void DeleteLaboratoryWork(LaboratoryWorkService service)
    {
        Console.Write("Введите ID лабораторной работы, которую хотите удалить: ");
        if (Int32.TryParse(Console.ReadLine(), out var id))
        {
            service.RemoveLaboratoryWork(id);
            Console.WriteLine($"Лабораторная работа с ID: {id} была удалена");
        }
        else
        {
            Console.WriteLine("Неверный формат ID.");
        }
    }

    public static void ModifyLaboratoryWork(LaboratoryWorkService service)
    {
        Console.Write("Введите ID лабораторной работы, которую хотите изменить: ");
        if (!Int32.TryParse(Console.ReadLine(), out var id))
        {
            Console.WriteLine("Неверный формат ID");
            return;
        }
        
        var laboratoryWork = service.ShowAllLaboratoryWorks().FirstOrDefault(x => x.Id == id);
        if (laboratoryWork == null)
        {
            Console.WriteLine($"Лабораторной работы с ID [ {id} ] не существует");
            return;
        }
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Редактирование лабораторной работы {id}");
            Console.WriteLine($"1. Предмет: {laboratoryWork.Subject}\n" +
                              $"2. Название: {laboratoryWork.Name}\n" +
                              $"3. Описание: {laboratoryWork.Description}\n" +
                              $"4. Дедлайн: {laboratoryWork.Deadline}"+
                              $"0. Завершить редактирование");
            Console.Write("Выберите поле для изменения: ");
            
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    Console.Write("Введите название предмета: ");
                    var modifySubject = Console.ReadLine();
                    service.EditLaboratoryWork(id, s => s.Subject = modifySubject);
                    break;
                case "2":
                    Console.Write("Введите название лабораторной работы: ");
                    var modifyName = Console.ReadLine();
                    service.EditLaboratoryWork(id, s => s.Name = modifyName);
                    break;
                case "3":
                    Console.Write("Введите новое описание: ");
                    var modifyDescription = Console.ReadLine();
                    service.EditLaboratoryWork(id, s => s.Description = modifyDescription);
                    break;
                case "4":
                    Console.Write("Введите дедлайн сдачи лабораторной работы: ");
                    var modifyDeadline = DateTime.Parse(Console.ReadLine());
                    service.EditLaboratoryWork(id, s => s.Deadline = modifyDeadline);
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
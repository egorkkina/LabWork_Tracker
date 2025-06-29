using LabWork_Tracker.Services;

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

    private static LaboratoryWork CreateLaboratoryWork()
    {
        Console.Clear();
        Console.WriteLine("--- Добавление лабораторной работы ---");
        var newSubject = InputHelper.PromptNonEmpty("Введите предмет, по которому будет выдана ЛР: ");
        var newName = InputHelper.PromptNonEmpty("Введите название ЛР: ");
        var newDescription = InputHelper.PromptNonEmpty("Заполните описание данной ЛР: ");
        DateTime newDeadline = InputHelper.PromptValidDate("Введите дедлайн по данной работе (гггг-мм-дд): ");
        
        Console.WriteLine($"Лабораторная работа [ {newName} ] была добавлена");

        return new LaboratoryWork()
        {
            Subject = newSubject,
            Name = newName,
            Description = newDescription,
            Deadline = newDeadline
        };
        
    }

    private static void ShowLaboratoryWork(LaboratoryWorkService service)
    {
        if (service.GetAllLaboratoryWorks().Count == 0)
        {
            Console.WriteLine("Список лабораторных работ пуст.");
            return;
        }
        Console.WriteLine("--- Информация по лабораторным работам ---\n");
        Console.WriteLine("{0,-10} | {1,-50} | {2,-50} | {3, -60} | {4, 0}", "ID", "Предмет", "Название", "Описание", "Дедлайн");
        Console.WriteLine(new string('-', 200));
        foreach (var lr in service.GetAllLaboratoryWorks())
        {
            Console.WriteLine("{0,-10} | {1,-50} | {2,-50} | {3, -60} | {4, 0}", lr.Id, lr.Subject, lr.Name, lr.Description, lr.Deadline);
        }
    }

    private static void DeleteLaboratoryWork(LaboratoryWorkService service)
    {
        var id = InputHelper.PromptValidLabId("Введите ID лабораторной работы, которую хотите удалить: ");
        if (!service.RemoveLaboratoryWork(id))
        {
            Console.WriteLine("Лабораторная работа с таким ID не найдена");
            return;
        }
        Console.WriteLine($"Лабораторная работа с ID: {id} была удалена");
        
    }

    private static void ModifyLaboratoryWork(LaboratoryWorkService service)
    {
        var id = InputHelper.PromptValidLabId("Введите ID лабораторной работы, которую хотите изменить: ");
        
        var laboratoryWork = service.GetAllLaboratoryWorks().FirstOrDefault(x => x.Id == id);
        if (laboratoryWork == null)
        {
            Console.WriteLine($"Лабораторной работы с ID [ {id} ] не существует");
            return;
        }
        
        EntryLaboratoryWork(laboratoryWork, service, id);
    }

    private static void EntryLaboratoryWork(LaboratoryWork laboratoryWork, LaboratoryWorkService service, int id)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"Редактирование лабораторной работы {id}");
            Console.WriteLine($"1. Предмет: {laboratoryWork.Subject}\n" +
                              $"2. Название: {laboratoryWork.Name}\n" +
                              $"3. Описание: {laboratoryWork.Description}\n" +
                              $"4. Дедлайн: {laboratoryWork.Deadline}\n"+
                              $"0. Завершить редактирование");
            Console.Write("Выберите поле для изменения: ");
            
            var input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    var modifySubject = InputHelper.PromptNonEmpty("Введите название предмета: ");
                    service.EditLaboratoryWork(id, s => s.Subject = modifySubject);
                    break;
                case "2":
                    var modifyName = InputHelper.PromptNonEmpty("Введите название лабораторной работы: ");
                    service.EditLaboratoryWork(id, s => s.Name = modifyName);
                    break;
                case "3":
                    var modifyDescription = InputHelper.PromptNonEmpty("Введите новое описание: ");
                    service.EditLaboratoryWork(id, s => s.Description = modifyDescription);
                    break;
                case "4":
                    Console.Write("Введите дедлайн сдачи лабораторной работы: ");
                    string str = Console.ReadLine();
                    if (DateTime.TryParse(str, out DateTime modifyDeadline))
                    {
                        service.EditLaboratoryWork(id, s => s.Deadline = modifyDeadline);
                        Console.WriteLine("Дедлайн обновлён.");
                    }
                    else
                    {
                        Console.WriteLine("Неверный формат даты. Попробуйте ещё раз.");
                    }
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
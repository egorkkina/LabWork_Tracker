namespace LabWork_Tracker;

public static class InputHelper
{
    public static string PromptNonEmpty(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (!string.IsNullOrWhiteSpace(input))
                return input;

            Console.WriteLine("Поле не может быть пустым. Повторите ввод.");
        }
    }
    
    public static int PromptCardNumber(string prompt, Func<int, bool>? isDuplicate = null)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int number) && input.Length == 6)
            {
                if (isDuplicate != null && isDuplicate(number))
                {
                    Console.WriteLine("Такой номер студенческого билета уже существует.");
                    continue;
                }

                return number;
            }

            Console.WriteLine("Введите корректный 6-значный номер студенческого билета.");
        }
    }
    
    public static Guid PromptValidStudentId(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (Guid.TryParse(input, out Guid id))
                return id;

            Console.WriteLine("Неверный ввод. Введите корректный ID студента (GUID).");
        }
    }

    public static int PromptGrade(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int grade) && grade >= 0 && grade <= 20)
                return grade;

            Console.WriteLine("Оценка должна быть целым числом от 0 до 20.");
        }
    }
    
    public static int PromptValidLabId(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (int.TryParse(input, out int labId))
                return labId;

            Console.WriteLine("Неверный ввод. Введите целое число (ID лабораторной работы).");
        }
    }

    public static DateTime PromptValidDate(string prompt)
    {
        while (true)
        {
            Console.Write(prompt);
            string? input = Console.ReadLine()?.Trim();

            if (DateTime.TryParse(input, out DateTime date) && date > DateTime.Today)
                return date;

            Console.WriteLine("Некорректное значение даты. Введите будущую дату (например: 2025-07-21).");
        }
    }
}
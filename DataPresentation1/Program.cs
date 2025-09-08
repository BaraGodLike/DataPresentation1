using DataPresentation1.DoubleLinked;

namespace DataPresentation1;

public class Program
{
    public static void Main()
    {
        MyList<Addressee> list = new MyList<Addressee>(); // создаем пустой лист
        FillList(list);
        list.PrintList(); // вывод
        Console.WriteLine();
        
        DeleteDuplicates(list);
        list.PrintList(); // вывод
    }
    
    private static void FillList(MyList<Addressee> list)
    {
        // Добавляем адресатов с повторениями
        list.Insert(list.End(), new Addressee("Влад Тужиков", "СПб, Альпийский 15к2"));
        list.Insert(list.End(), new Addressee("Иван Петров", "Москва, ул. Ленина 25"));
        list.Insert(list.End(), new Addressee("Мария Сидорова", "Екатеринбург, пр. Космонавтов 10"));
        list.Insert(list.End(), new Addressee("Алексей Иванов", "Новосибирск, ул. Гагарина 7"));
        list.Insert(list.End(), new Addressee("Елена Козлова", "Казань, ул. Баумана 15"));
        list.Insert(list.End(), new Addressee("Влад Тужиков", "СПб, Альпийский 15к2")); // Повторение
        list.Insert(list.End(), new Addressee("Дмитрий Смирнов", "Нижний Новгород, ул. Минина 3"));
        list.Insert(list.End(), new Addressee("Ольга Волкова", "Самара, ул. Куйбышева 12"));
        list.Insert(list.End(), new Addressee("Сергей Попов", "Ростов-на-Дону, ул. Большая Садовая 5"));
        list.Insert(list.End(), new Addressee("Мария Сидорова", "Екатеринбург, пр. Космонавтов 10")); // Повторение
        list.Insert(list.End(), new Addressee("Анна Морозова", "Краснодар, ул. Красная 20"));
        list.Insert(list.End(), new Addressee("Павел Новиков", "Воронеж, пр. Революции 8"));
        list.Insert(list.End(), new Addressee("Иван Петров", "Москва, ул. Ленина 25")); // Повторение
        list.Insert(list.End(), new Addressee("Наталья Орлова", "Уфа, ул. Октябрьской революции 14"));
        list.Insert(list.End(), new Addressee("Алексей Иванов", "Новосибирск, ул. Гагарина 7")); // Повторение
        list.Insert(list.End(), new Addressee("Кристина Лебедева", "Пермь, ул. Ленина 30"));
        list.Insert(list.End(), new Addressee("Михаил Козлов", "Волгоград, пр. Ленина 45"));
        list.Insert(list.End(), new Addressee("Елена Козлова", "Казань, ул. Баумана 15")); // Повторение
        list.Insert(list.End(), new Addressee("Артем Зайцев", "Саратов, ул. Московская 22"));
        list.Insert(list.End(), new Addressee("Влад Тужиков", "СПб, Альпийский 15к2")); // Повторение
    }

    private static void DeleteDuplicates(MyList<Addressee> list)
    {
        MyList<Addressee>.Position p = list.First(); // получение первой позиции списка
        MyList<Addressee>.Position end = list.End(); // получение позиции после последнего

        while (p != end) // пока спсиок не кончится
        {
            MyList<Addressee>.Position q = list.Next(p); // получаем следующую позицию за проверяемой
            while (q != end)
            {
                if (list.Retrieve(p).Equals(list.Retrieve(q))) // если объекты в разных позициях совпали
                {
                    MyList<Addressee>.Position deleted = q; // запоминаем удаляемую позицию, чтобы могли перейти к следующей
                    q = list.Next(q); 
                    list.Delete(deleted); // удаляем дубликат
                }
                else
                {
                    q = list.Next(q); // если не удаляли дубликат все равно переходим к следующей
                }
            }

            p = list.Next(p); // ищем дубликаты для объекта на следующей позиции
        }
    }
}
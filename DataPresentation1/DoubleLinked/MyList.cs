namespace DataPresentation1.DoubleLinked;

// Класс MyList<T> — реализация двусвязного списка.
// Каждый элемент хранит ссылки на предыдущий и следующий узлы.
// Работает только с типами, реализующими IEquatable<T>.
public class MyList<T> where T : IEquatable<T>
{
    // Голова списка (первый элемент)
    private Node? _head;
    // Хвост списка (последний элемент)
    private Node? _tail;
    // Условный "конец списка" (как маркер отсутствия позиции)
    private readonly Position _end = new Position(null);
    
    // Возвращает фиктивную позицию конца списка
    public Position End()
    {
        return _end;
    }

    // Проверка, существует ли указанная позиция в списке
    private bool CheckPosition(Position p)
    {
        if (p == _end) return false; // конец списка не считается валидной позицией
        
        Node? cur = _head;
        // Перебираем список от начала до конца
        while (cur != null)
        {
            if (cur == p.Pos) // нашли совпадение
            {
                return true;
            }

            cur = cur.Next;
        }

        return false; // позиция не найдена
    }

    // Связывает два узла (обновляет ссылки Next и Previous)
    private static void LinkNodes(Node? first, Node? second)
    {
        if (first != null) first.Next = second;
        if (second != null) second.Previous = first;
    }
    
    // Вставка элемента obj перед позицией p
    public void Insert(Position p, T obj)
    {
        Node newNode;
        // Если вставляем в конец списка
        if (p == _end)
        {
            // Случай пустого списка
            if (IsEmpty())
            {
                _head = _tail = new Node(obj); // новый элемент — и голова, и хвост
                return;
            }
            
            // Случай непустого списка — добавляем новый узел в конец
            newNode = new Node(obj);
            LinkNodes(_tail!, newNode);
            _tail = newNode;
            return;
        }
        
        // Если позиция некорректная
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");
        
        // Текущий узел, перед которым вставляем
        Node cur = p.Pos!;
        
        // Новый узел, в который переносим данные текущего
        newNode = new Node(cur.Data);
        // Переставляем ссылки
        LinkNodes(newNode, cur.Next);
        LinkNodes(cur, newNode);
        // В текущий узел записываем новые данные
        cur.Data = obj;

        // Если вставляли в хвост, меняем его позицию
        if (p.Pos == _tail)
        {
            _tail = _tail!.Next;
        }
        // Если в списке только один элемент, то хвост смещается на второй
        if (_head == _tail) _tail = _head!.Next;
    }

    // Поиск первого вхождения элемента obj в списке
    public Position Locate(T obj)
    {
        Node? cur = _head;

        while (cur != null)
        {
            if (cur.Data.Equals(obj))
            {
                return new Position(cur);
            }
            cur = cur.Next;
        }
        return _end; // не найден
    }

    // Получение данных по позиции
    public T Retrieve(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");
        
        return p.Pos!.Data;
    }

    // Удаление элемента по позиции
    public void Delete(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");

        // Если удаляем первый элемент
        if (p.Pos == _head)
        {
            // Если список состоит из одного элемента
            if (_head == _tail)
            {
                _head = _tail = null; // очищаем список
                return;
            }
            
            // Перемещаем голову на следующий элемент
            _head = _head!.Next;
            if (_head != null)
            {
                _head.Previous = null;
            }
            
            return;
        }

        // Если удаляем последний элемент
        if (p.Pos == _tail)
        {
            _tail = _tail!.Previous;
            _tail!.Next = null;
            return;
        }
        
        // Если удаляем элемент из середины
        LinkNodes(p.Pos!.Previous!, p.Pos!.Next);
    }

    // Получение позиции следующего элемента
    public Position Next(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");

        return p.Pos == _tail ? _end : new Position(p.Pos!.Next);
    }

    // Получение позиции предыдущего элемента
    public Position Previous(Position p)
    {
        if (p.Pos == _head || !CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке или не имеет предыдущего");

        return new Position(p.Pos!.Previous);
    }

    // Получение позиции первого элемента списка
    public Position First()
    {
        return _head == null ? _end : new Position(_head);
    }

    // Полная очистка списка
    public void MakeNull()
    {
        _head = _tail = null;
    }

    // Вывод списка в консоль
    public void PrintList()
    {
        Console.Write("[");
        
        Node? cur = _head;

        if (cur != null)
        {
            Console.Write(cur.Data); // печатаем первый элемент
            cur = cur.Next;
            
            // Печатаем остальные элементы через запятую
            while (cur != null)
            {
                Console.Write($",\n {cur.Data}");
                cur = cur.Next;
            }
        }

        Console.WriteLine("]");
    }

    // Проверка на пустоту списка
    private bool IsEmpty()
    {
        return _head is null;
    }
    
    // Вспомогательный класс, представляющий позицию в списке (обертка над Node)
    public class Position
    {
        internal Node? Pos { get; init; }

        internal Position(Node? pos)
        {
            Pos = pos;
        }
    }
    
    // Вспомогательный класс, представляющий ноду связного списка
    internal class Node(T data, Node? prev = null, Node? next = null)
    {
        internal T Data { get; set; } = data; // Объект внутри ноды
        internal Node? Previous { get; set; } = prev; // ссылка на предыдущую ноду
        internal Node? Next { get; set; } = next; // ссылка на следующую ноду
    }
}

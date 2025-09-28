namespace DataPresentation1.Cursors;

// Класс MyList<T> — реализация списка с использованием массивов и имитации связного списка
// Работает с объектами, реализующими интерфейс IEquatable<T> для сравнения.
public class MyList<T> where T : IEquatable<T>
{
    // Массив объектов списка и указателей
    private static readonly Node[] Nodes;
    // Индекс первого элемента списка, -1 означает пустой список
    private int _start = -1;
    // Указатель на первый свободный индекс в массиве (голова "свободного списка")
    private static int _space = 0;
    // Константная "позиция конца" списка (условный конец)
    private readonly Position _end = new Position(-1);
    // Максимальный размер списка
    private const int MaxSize = 100;

    // Статический конструктор — инициализация массивов и списка свободных ячеек
    static MyList()
    {
        Nodes = new Node[MaxSize];
        const int size = MaxSize - 1;
        // Каждая ячейка указывает на следующую свободную
        for (int i = 0; i < size; i++)
        {
            Nodes[i] = new Node
            {
                Next = i + 1
            };
        }
        // Последняя свободная указывает на конец (-1)
        Nodes[size] = new Node
        {
            Next = -1
        };
    }

    // Возвращает позицию конца списка
    public Position End()
    {
        return _end;
    }

    // Возвращает индекс последнего элемента списка
    private int Last()
    {
        int cur = _start;
        int prev = -1;
        while (cur != -1)
        {
            prev = cur;
            cur = Nodes[cur].Next;
        }

        return prev;
    }

    // Возвращает индекс предыдущего элемента относительно данного
    private int GetPrevious(int index)
    {
        int cur = _start;
        int prev = -1;
        while (cur != -1)
        {
            if (cur == index) return prev;
            prev = cur;
            cur = Nodes[cur].Next;
        }
        return -1; // не найден
    }

    // Проверяет, существует ли указанная позиция в списке
    private bool CheckPosition(int index)
    {
        return index >= 0 && (index == _start || GetPrevious(index) != -1);
    }
    
    // Вставка элемента obj перед позицией p
    public void Insert(Position p, T obj)
    {
        int tmp;
        // Если вставляем в конец списка
        if (p == _end)
        {
            // Если список пустой
            if (IsEmpty())
            {
                _start = _space; // новый старт
                Nodes[_space].Data = obj; // записываем объект
                _space = Nodes[_space].Next; // передвигаем указатель на свободное место
                Nodes[_start].Next = -1; // у последнего элемента next = -1
                return;
            }
            
            // Если список не пустой — добавляем в конец
            Nodes[_space].Data = obj;
            
            tmp = _space;
            _space = Nodes[_space].Next;
            
            int prev = Last();
            Nodes[tmp].Next = Nodes[prev].Next;
            Nodes[prev].Next = tmp;
            
            return;
        }

        // Проверка валидности позиции
        if (!CheckPosition(p.Pos))
        {
            throw new IncorrectPositionException("Данная позиция не существует в списке");
        }

        // Перемещаем элемент из p в свободную ячейку, а на его место вставляем новый
        Nodes[_space].Data = Nodes[p.Pos].Data;
        Nodes[p.Pos].Data = obj;
        
        tmp = _space;
        _space = Nodes[_space].Next;
        
        Nodes[tmp].Next = Nodes[p.Pos].Next;
        Nodes[p.Pos].Next = tmp;
    }

    // Находит позицию элемента obj в списке
    public Position Locate(T obj)
    {
        int cur = _start;
        while (cur != -1)
        {
            if (Nodes[cur].Data.Equals(obj)) return new Position(cur);
            cur = Nodes[cur].Next;
        }
        return _end; // не найден
    }

    // Возвращает элемент списка по позиции
    public T Retrieve(Position p)
    {
        if (!CheckPosition(p.Pos)) 
            throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        return Nodes[p.Pos].Data;
    }

    // Удаляет элемент из списка по позиции
    public void Delete(Position p)
    {
        if (p.Pos < 0) throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        int tmp;
        // Удаляем первый элемент
        if (p.Pos == _start)
        {
            tmp = _space;
            _space = _start; // освободили ячейку
            _start = Nodes[_start].Next; // новый старт
            Nodes[_space].Next = tmp;

            return;
        }

        // Удаляем не первый элемент
        int prev = GetPrevious(p.Pos);
        if (prev == -1) throw new IncorrectPositionException("Данная позиция отсутствует в списке");

        int cur = Nodes[prev].Next;
        Nodes[prev].Next = Nodes[cur].Next;
        
        tmp = _space;
        _space = cur;

        Nodes[_space].Next = tmp;
    }

    // Возвращает позицию следующего элемента
    public Position Next(Position p)
    {
        if (!CheckPosition(p.Pos)) throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        return Nodes[p.Pos].Next == -1 ? _end : new Position(Nodes[p.Pos].Next);
    }

    // Возвращает позицию предыдущего элемента
    public Position Previous(Position p)
    {
        int prev;
        if (p.Pos < 0 || (prev = GetPrevious(p.Pos)) == -1) 
            throw new IncorrectPositionException("Данная позиция отсутствует в списке");

        return new Position(prev);
    }

    // Очищает список, все элементы возвращаются в свободное пространство
    public void MakeNull()
    {
        if (IsEmpty()) return;
        
        Nodes[Last()].Next = _space; // цепляем освободившиеся ячейки к списку свободных
        _space = _start;
        _start = -1;
    }
    
    // Проверяет, пуст ли список
    private bool IsEmpty()
    {
        return _start == -1;
    }
    
    // Возвращает позицию первого элемента списка
    public Position First()
    {
        return IsEmpty() ? _end : new Position(_start);
    }

    // Печатает список в консоль
    public void PrintList()
    {
        Console.Write("[");
        int cur = _start;
        while (cur != -1) {
            Console.Write($",\n{Nodes[cur].Data}");
            cur = Nodes[cur].Next;
        }
        Console.WriteLine("]");
    }
    
    // Вспомогательный класс для представления позиции в списке
    public class Position(int pos)
    {
        internal int Pos { get; init; } = pos;
    }

    internal class Node
    {
        internal T Data { get; set; }
        internal int Next { get; set; }
        
        internal Node() {}

        internal Node(T data, int next)
        {
            Data = data;
            Next = next;
        }
    }
}

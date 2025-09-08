namespace DataPresentation1.Cursors;

public class MyList<T> where T : IEquatable<T>
{
    private static readonly T[] Objects;
    private static readonly int[] Nexts;
    private int _start = -1;
    private static int _space = 0;
    private readonly Position _end = new Position(-1);
    private const int MaxSize = 100;

    static MyList()
    {
        Objects = new T[MaxSize];
        Nexts = new int[MaxSize];
        const int size = MaxSize - 1;
        for (int i = 0; i < size; i++)
        {
            Nexts[i] = i + 1;
        }

        Nexts[size] = -1;
    }

    public Position End()
    {
        return _end;
    }

    private int Last()
    {
        int cur = _start;
        int prev = -1;
        while (cur != -1)
        {
            prev = cur;
            cur = Nexts[cur];
        }

        return prev;
    }

    private int GetPrevious(int index)
    {
        int cur = _start;
        int prev = -1;
        while (cur != -1)
        {
            if (cur == index) return prev;
            prev = cur;
            cur = Nexts[cur];
        }

        return -1;
    }

    private bool CheckPosition(int index)
    {
        return index >= 0 && (index == _start || GetPrevious(index) != -1);
    }
    
    public void Insert(Position p, T obj)
    {
        int tmp;
        if (p == _end)
        {
            if (IsEmpty())
            {
                _start = _space;
                Objects[_space] = obj;
                
                _space = Nexts[_space];
                Nexts[_start] = -1;
                
                return;
            }
            
            Objects[_space] = obj;
            
            tmp = _space;
            _space = Nexts[_space];
            
            int prev = Last();
            Nexts[tmp] = Nexts[prev];
            Nexts[prev] = tmp;
            
            return;
        }

        if (!CheckPosition(p.Pos)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");
        
        Objects[_space] = Objects[p.Pos];
        Objects[p.Pos] = obj;
        
        tmp = _space;
        _space = Nexts[_space];
        
        Nexts[tmp] = Nexts[p.Pos];
        Nexts[p.Pos] = tmp;
    }


    public Position Locate(T obj)
    {
        int cur = _start;
        while (cur != -1)
        {
            if (Objects[cur].Equals(obj)) return new Position(cur);
            cur = Nexts[cur];
        }

        return _end;
    }

    public T Retrieve(Position p)
    {
        if (!CheckPosition(p.Pos)) throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        
        return Objects[p.Pos];
    }

    public void Delete(Position p)
    {
        if (p.Pos < 0) throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        int tmp;
        if (p.Pos == _start)
        {
            tmp = _space;
            _space = _start;
            
            _start = Nexts[_start];
            Nexts[_space] = tmp;

            return;
        }

        int prev = GetPrevious(p.Pos);
        if (prev == -1) throw new IncorrectPositionException("Данная позиция отсутствует в списке");

        int cur = Nexts[prev];
        Nexts[prev] = Nexts[cur];
        
        tmp = _space;
        _space = cur;

        Nexts[_space] = tmp;
    }

    public Position Next(Position p)
    {
        if (p.Pos == _start && !IsEmpty()) return new Position(Nexts[_start]);
        int prev = GetPrevious(p.Pos);
        if (prev == -1) throw new IncorrectPositionException("Данная позиция отсутствует в списке");
        
        return Nexts[Nexts[prev]] == -1 ? _end : new Position(Nexts[p.Pos]);
    }

    public Position Previous(Position p)
    {
        int prev;
        if (p.Pos < 0 || (prev = GetPrevious(p.Pos)) == -1) 
            throw new IncorrectPositionException("Данная позиция отсутствует в списке");

        return new Position(prev);
    }

    public void MakeNull()
    {
        if (IsEmpty()) return;
        
        Nexts[Last()] = _space;
        _space = _start;
        _start = -1;
    }
    
    private bool IsEmpty()
    {
        return _start == -1;
    }
    
    public Position First()
    {
        return IsEmpty() ? _end : new Position(_start);
    }

    public void PrintList()
    {
        Console.Write("[");
        
        int cur = _start;
        
        while (cur != -1) {
            Console.Write($",\n{Objects[cur]}");
            cur = Nexts[cur];
        }
        
        Console.WriteLine("]");
    }
    
    public class Position(int pos)
    {
        internal int Pos { get; init; } = pos;
    }
}
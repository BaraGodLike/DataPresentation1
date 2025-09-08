namespace DataPresentation1.DoubleLinked;

public class MyList<T> where T : IEquatable<T>
{
    private Node<T>? _head;
    private Node<T>? _tail;
    private readonly Position _end = new Position(null);
    
    
    
    public Position End()
    {
        return _end;
    }

    private bool CheckPosition(Position p)
    {
        if (p == _end) return false;
        
        Node<T>? cur = _head;
        
        while (cur != null)
        {
            if (cur == p.Pos)
            {
                return true;
            }

            cur = cur.Next;
        }

        return false;
    }

    private static void LinkNodes(Node<T>? first, Node<T>? second)
    {
        if (first != null) first.Next = second;
        if (second != null) second.Previous = first;
    }
    
    public void Insert(Position p, T obj)
    {
        Node<T> newNode;
        if (p == _end)
        {
            if (IsEmpty())
            {
                _head = _tail = new Node<T>(obj);
                return;
            }
            
            newNode = new Node<T>(obj);
            LinkNodes(_tail!, newNode);
            _tail = newNode;
            return;
        }
        
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");

        Node<T> cur = p.Pos!;
        
        newNode = new Node<T>(cur.Data);
        LinkNodes(newNode, cur.Next);
        LinkNodes(cur, newNode);
        cur.Data = obj;
        if (_head == _tail) _tail = _head!.Next;
    }

    public Position Locate(T obj)
    {
        Node<T>? cur = _head;

        while (cur != null)
        {
            if (cur.Data.Equals(obj))
            {
                return new Position(cur);
            }

            cur = cur.Next;
        }

        return _end;
    }

    public T Retrieve(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");
        
        return p.Pos!.Data;
    }

    public void Delete(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");

        if (p.Pos == _head)
        {

            if (_head == _tail)
            {
                _head = _tail = null;
                return;
            }
            
            _head = _head!.Next;
            if (_head != null)
            {
                _head.Previous = null;
            }
            
            return;
        }

        if (p.Pos == _tail)
        {
            _tail = _tail!.Previous;
            _tail!.Next = null;
            return;
        }
        
        LinkNodes(p.Pos!.Previous!, p.Pos!.Next);
    }

    public Position Next(Position p)
    {
        if (!CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке");

        return p.Pos == _tail ? _end : new Position(p.Pos!.Next);
    }

    public Position Previous(Position p)
    {
        if (p.Pos == _head || !CheckPosition(p)) 
            throw new IncorrectPositionException("Данная позиция не существует в списке или не имеет предыдущего");

        return new Position(p.Pos!.Previous);
    }

    public Position First()
    {
        return _head == null ? _end : new Position(_head);
    }

    public void MakeNull()
    {
        _head = _tail = null;
    }

    public void PrintList()
    {
        Console.Write("[");
        
        Node<T>? cur = _head;

        if (cur != null)
        {
            Console.Write(cur.Data);
            cur = cur.Next;
            
            while (cur != null)
            {
                Console.Write($",\n {cur.Data}");
                cur = cur.Next;
            }
        }

        Console.WriteLine("]");
    }

    private bool IsEmpty()
    {
        return _head is null;
    }
    
    public class Position
    {
        internal Node<T>? Pos { get; init; }

        internal Position(Node<T>? pos)
        {
            Pos = pos;
        }
    } 
}

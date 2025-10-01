namespace Lab2.Queue.Linked;

public class Queue<T>
{
    private Node? _tail;
    
    public void Enqueue(T x)
    {
        if (Empty())
        {
            _tail = new Node(x, null);
            _tail.Next = _tail;
            return;
        }

        Node cur = new Node(x, _tail!.Next);
        _tail.Next = cur;
        _tail = cur;
    }

    public T Dequeue()
    {
        T toReturn = _tail!.Next!.Data;
        if (_tail!.Next == _tail) _tail = null;
        else _tail.Next = _tail.Next.Next;
        return toReturn;
    }
    
    public T Front()
    {
        return _tail!.Next!.Data;
    }

    public bool Full()
    {
        return false;
    }

    public bool Empty()
    {
        return _tail is null;
    }

    public void MakeNull()
    {
        _tail = null;
    }
    
    private class Node(T data, Node? next)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}
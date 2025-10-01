namespace Lab2.Stack.Linked;

public class Stack<T>
{
    private Node? _head;

    public void Push(T x)
    {
        Node? prevHead = _head;
        _head = new Node(x, prevHead);
    }

    public T Pop()
    {
        T toReturn = _head!.Data;
        _head = _head.Next;
        return toReturn;
    }

    public T Top()
    {
        return _head!.Data; 
    }
    
    public bool Empty()
    {
        return _head is null;
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _head = null;
    }
    
    private class Node(T data, Node? next)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}
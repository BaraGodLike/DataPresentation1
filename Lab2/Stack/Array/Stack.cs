namespace Lab2.Stack.Array;

public class Stack<T>
{
    private const int Size = 256; 
    private readonly T[] _array = new T[Size];
    private int _last = -1;

    public void Push(T x)
    {
        _array[++_last] = x;
    }

    public T Pop()
    {
        return _array[_last--];
    }

    public T Top()
    {
        return _array[_last];
    }
    
    public bool Full()
    {
        return _last == Size - 1;
    }

    public bool Empty()
    {
        return _last < 0;
    }

    public void MakeNull()
    {
        _last = -1;
    }
    
}
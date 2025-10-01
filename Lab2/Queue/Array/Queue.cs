namespace Lab2.Queue.Array;

public class Queue<T>
{
    private const int Size = 16; 
    private readonly T[] _array = new T[Size];
    private int _front = 0;
    private int _rear = -1;
    
    public void Enqueue(T x)
    {
        _rear = (_rear + 1) % Size;
        _array[_rear] = x;
    }

    public T Dequeue()
    {
        T toReturn = _array[_front++];
        _front %= Size;
        return toReturn;
    }

    public T Front()
    {
        return _array[_front];
    }

    public bool Full()
    {
        return (_rear + 2) % Size == _front;
    }

    public bool Empty()
    {
        return (_rear + 1) % Size == _front;
    }

    public void MakeNull()
    {
        _rear = _front - 1;
    }
}
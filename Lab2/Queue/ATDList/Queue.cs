using DataPresentation1.DoubleLinked;

namespace Lab2.Queue.ATDList;

public class Queue<T> where T : IEquatable<T>
{
    private MyList<T> _list = new();
    
    public void Enqueue(T x)
    {
        _list.Insert(_list.End(), x);
    }

    public T Dequeue()
    {
        T toReturn = _list.Retrieve(_list.First());
        _list.Delete(_list.First());
        return toReturn;
    }

    public T Front()
    {
        return _list.Retrieve(_list.First());
    }

    public bool Full()
    {
        return false;
    }

    public bool Empty()
    {
        return _list.First() == _list.End();
    }

    public void MakeNull()
    {
        _list.MakeNull();
    }
}
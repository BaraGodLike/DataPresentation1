using DataPresentation1.DoubleLinked;

namespace Lab2.Stack.ATDList;

public class Stack<T> where T : IEquatable<T>
{
    private readonly MyList<T> _list = new();

    public void Push(T x)
    {
        _list.Insert(_list.First(), x);
    }

    public T Pop()
    {
        T toReturn = _list.Retrieve(_list.First());
        _list.Delete(_list.First());
        return toReturn;
    }

    public T Top()
    {
        return _list.Retrieve(_list.First());
    }

    public bool Empty()
    {
        return _list.First() == _list.End();
    }

    public bool Full()
    {
        return false;
    }

    public void MakeNull()
    {
        _list.MakeNull();
    }
}
using DataPresentation1.DoubleLinked;

namespace Lab2.Queue.ATDList;

// Очередь на основе АТД списка
public class Queue<T> where T : IEquatable<T>
{
    private MyList<T> _list = new();  // Основа очереди - АТД список
    
    // Добавление в конец очереди
    public void Enqueue(T x)
    {
        _list.Insert(_list.End(), x);
    }

    // Извлечение из начала очереди
    public T Dequeue()
    {
        T toReturn = _list.Retrieve(_list.First());
        _list.Delete(_list.First());
        return toReturn;
    }

    // Просмотр первого элемента
    public T Front()
    {
        return _list.Retrieve(_list.First());
    }

    // Очередь на списке никогда не заполняется
    public bool Full()
    {
        return false;
    }

    // Проверка пустоты
    public bool Empty()
    {
        return _list.First() == _list.End();
    }

    // Очистка очереди
    public void MakeNull()
    {
        _list.MakeNull();
    }
}
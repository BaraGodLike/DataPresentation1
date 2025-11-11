namespace Lab2.Queue.Linked;

// Очередь на кольцевом связном списке
public class Queue<T>
{
    private Node? _tail;  // Хвост очереди, который ссылается на голову
    
    // Добавление элемента в конец
    public void Enqueue(T x)
    {
        if (_tail is null)
        {
            // Создаем единственный элемент, который ссылается сам на себя
            _tail = new Node(x, null);
            _tail.Next = _tail;
            return;
        }

        // Вставляем новый элемент после хвоста
        Node cur = new Node(x, _tail!.Next);
        _tail.Next = cur;
        _tail = cur;  // Новый элемент становится хвостом
    }

    // Извлечение из начала
    public T Dequeue()
    {
        T toReturn = _tail!.Next!.Data;  // Голова - следующий после хвоста
        if (_tail!.Next == _tail) _tail = null;  // Был один элемент - теперь пусто
        else _tail.Next = _tail.Next.Next;  // Удаляем голову из кольца
        return toReturn;
    }
    
    // Просмотр первого элемента
    public T Front()
    {
        return _tail!.Next!.Data;  // Голова - следующий после хвоста
    }

    // Очередь на списке никогда не заполняется
    public bool Full()
    {
        return false;
    }

    // Проверка пустоты
    public bool Empty()
    {
        return _tail is null;
    }

    // Очистка очереди
    public void MakeNull()
    {
        _tail = null;
    }
    
    // Узел кольцевого списка
    private class Node(T data, Node? next)
    {
        public T Data { get; set; } = data;
        public Node? Next { get; set; } = next;
    }
}
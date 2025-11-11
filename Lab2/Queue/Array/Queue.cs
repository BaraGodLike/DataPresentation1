namespace Lab2.Queue.Array;

// Очередь на кольцевом массиве фиксированного размера
public class Queue<T>
{
    private const int Size = 256;        // Фиксированный размер очереди
    private readonly T[] _array = new T[Size];  // Кольцевой буфер
    private int _front = 0;             // Указатель на начало очереди
    private int _rear = Size - 1;             // Указатель на конец очереди
    
    // Добавление элемента в конец очереди
    public void Enqueue(T x)
    {
        _rear = Next(_rear);  // Циклическое перемещение
        _array[_rear] = x;
    }

    // Извлечение элемента из начала очереди
    public T Dequeue()
    {
        T toReturn = _array[_front];
        _front = Next(_front);  // Зацикливание указателя
        return toReturn;
    }

    // Просмотр первого элемента без извлечения
    public T Front()
    {
        return _array[_front];
    }

    // Проверка заполненности очереди
    public bool Full()
    {
        return Next(Next(_rear)) == _front;
    }

    // Проверка пустоты очереди
    public bool Empty()
    {
        return Next(_rear) == _front;
    }

    // Очистка очереди
    public void MakeNull()
    {
        _rear = _front - 1;  // Сбрасываем указатель конца
    }

    private int Next(int pos)
    {
        return (pos + 1) % Size;
    }
}
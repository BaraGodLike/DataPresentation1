using DataPresentation1.DoubleLinked;

namespace Lab2.Stack.ATDList;

// Стек на основе АТД список
public class Stack<T> where T : IEquatable<T>
{
    private readonly MyList<T> _list = new();  // Основа стека

    // Добавление элемента на вершину стека
    public void Push(T x)
    {
        _list.Insert(_list.First(), x);  // Вставляем в начало списка
    }

    // Извлечение элемента с вершины
    public T Pop()
    {
        T toReturn = _list.Retrieve(_list.First());
        _list.Delete(_list.First());  // Удаляем первый элемент
        return toReturn;
    }

    // Просмотр вершины без извлечения
    public T Top()
    {
        return _list.Retrieve(_list.First());
    }

    // Проверка пустоты стека
    public bool Empty()
    {
        return _list.First() == _list.End();
    }

    // Стек на списке никогда не заполняется
    public bool Full()
    {
        return false;
    }

    // Очистка стека
    public void MakeNull()
    {
        _list.MakeNull();
    }
}
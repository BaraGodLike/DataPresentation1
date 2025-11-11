namespace Lab2.Map;

// Реализация словаря на односвязном списке
public class Map<TKey, TValue> where TKey : IEquatable<TKey>
{
    private Node? _head;  // Первый элемент списка
    
    // Поиск узла по ключу. Если не находит - возвращает последний узел для быстрой вставки
    private bool FindNodeByKey(TKey key, ref Node? node)
    {
        Node? cur = _head;
        Node? prev = null;
        
        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                node = cur;  // Найден существующий узел
                return true;
            }

            prev = cur;
            cur = cur.Next;
        }

        node = prev;  // Последний узел (для добавления нового)
        return false;
    }
    

    // Проверка пустоты словаря
    private bool IsEmpty()
    {
        return _head is null;
    }
    
    // Добавление или обновление значения по ключу
    public void Assign(TKey d, TValue r)
    {
        if (IsEmpty())
        {
            _head = new Node(d, r, null);
            return;
        }

        Node? node = null;
        if (FindNodeByKey(d, ref node))
        {
            node!.Value = r;  // Обновление существующего значения
            return;
        }

        node!.Next = new Node(d, r, null);  // Добавление нового элемента в конец
    }

    // Получение значения по ключу
    public bool Compute(TKey d, ref TValue r)
    {
        Node? node = null;
        if (!FindNodeByKey(d, ref node)) return false;
        r = node!.Value;
        return true;
    }

    // Очистка словаря
    public void MakeNull()
    {
        _head = null;
    }

    // Вывод всех элементов в формате {key: value, ...}
    public void PrintList()
    {
        Console.Write("{");
        
        Node? cur = _head;

        if (cur != null)
        {
            Console.Write(cur); 
            cur = cur.Next;
            
            while (cur != null)
            {
                Console.Write($", {cur}");
                cur = cur.Next;
            }
        }

        Console.WriteLine("}");
    }

    // Узел списка, хранящий пару ключ-значение
    private class Node(TKey key, TValue value, Node? next)
    {
        public TKey Key { get; set; } = key;      // Ключ
        public TValue Value { get; set; } = value; // Значение
        public Node? Next { get; set; } = next;   // Следующий узел

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}

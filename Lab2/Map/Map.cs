namespace Lab2.Map;

public class Map<TKey, TValue> where TKey : IEquatable<TKey>
{
    private Node? _head;
    
    private bool FindNodeByKey(TKey key, ref Node? node)
    {
        Node? cur = _head;
        Node? prev = null;
        
        while (cur != null)
        {
            if (cur.Key.Equals(key))
            {
                node = cur;
                return true;
            }

            prev = cur;
            cur = cur.Next;
        }

        node = prev;
        return false;
    }
    

    private bool IsEmpty()
    {
        return _head is null;
    }
    
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
            node!.Value = r;
            return;
        }

        node!.Next = new Node(d, r, null);
    }

    public bool Compute(TKey d, ref TValue r)
    {
        Node? node = null;
        if (!FindNodeByKey(d, ref node)) return false;
        r = node!.Value;
        return true;
    }

    public void MakeNull()
    {
        _head = null;
    }

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

    private class Node(TKey key, TValue value, Node? next)
    {
        public TKey Key { get; set; } = key;
        public TValue Value { get; set; } = value;
        public Node? Next { get; set; } = next;

        public override string ToString()
        {
            return $"{Key}: {Value}";
        }
    }
}
namespace DataPresentation1.DoubleLinked;

internal class Node<T>(T data, Node<T>? prev = null, Node<T>? next = null)
{
    internal T Data { get; set; } = data;
    internal Node<T>? Previous { get; set; } = prev;
    internal Node<T>? Next { get; set; } = next;
}
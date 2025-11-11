using Lab2.Map;

Map<char, int> map = new();
Lab2.Queue.ATDList.Queue<char> queue = new();
Lab2.Stack.ATDList.Stack<char> stack = new();
char[] arr = "Hello World".ToCharArray(); // строка символы которой будут идти в стек, очередь и отображение

foreach (var i in arr) // заносим все символы в коллекции
{
    int cur = 0;
    map.Assign(i, map.Compute(i, ref cur) ? cur + 1 : 1);
    queue.Enqueue(i);
    stack.Push(i);
}
Console.WriteLine("Результат работы отображения:");
map.PrintList(); // отображение

Console.WriteLine("Результат работы очереди:");

while (!queue.Empty())
{
    Console.Write(queue.Dequeue()); // очередь
}

Console.WriteLine("\nРезультат работы стека:");

while (!stack.Empty())
{
    Console.Write(stack.Pop()); // стек
}

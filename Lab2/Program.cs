using Lab2.Map;

Map<char, int> map = new();

var arr = "Hello World".ToCharArray();

foreach (var i in arr)
{
    var cur = 0;
    map.Assign(i, map.Compute(i, ref cur) ? cur + 1 : 1);
}

map.PrintList();

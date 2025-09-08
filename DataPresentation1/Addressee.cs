namespace DataPresentation1;

public class Addressee(string name, string address) : IEquatable<Addressee>
{
    public string Name { get; init; } = name;
    public string Address { get; init; } = address;


    public bool Equals(Addressee? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name && Address == other.Address;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Addressee);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Address);
    }

    public override string ToString()
    {
        return $"{Name}: {Address}";
    }
}
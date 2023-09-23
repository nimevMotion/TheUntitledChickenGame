public class Item
{
    public string Name { get; set; }
    public int Size { get; set; }

    public Item(string name, int size)
    {
        Name = name;
        Size = size;
    }
}

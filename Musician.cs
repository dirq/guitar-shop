namespace GuitarShop;

public class Musician
{
    public Musician(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Guitar> Guitars { get; set; } = new List<Guitar>();
}
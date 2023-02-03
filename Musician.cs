namespace GuitarShop;

public class Musician
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public List<Guitar> Guitars { get; set; }
}
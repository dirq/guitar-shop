namespace GuitarShop;

public class Guitar
{
    public Guitar(Guid id, string make, string model, string color, float price)
    {
        Id = id;
        Make = make;
        Model = model;
        Color = color;
        Price = price;
    }

    public Guid Id { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }

    public string Color { get; set; }
    public float Price { get; set; }
}
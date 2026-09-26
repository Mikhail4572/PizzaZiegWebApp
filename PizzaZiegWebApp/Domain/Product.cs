namespace PizzaZiegWebApp.Domain;

public class Product
{
    public int Id { get; set; }
    public string PhotoPath { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int SizeSm { get; set; }
    public decimal Price { get; set; }
}

public class Computer : IProduct
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Processor { get; set; }
    public int RAM { get; set; }
    public int Storage { get; set; }

    public string GetProductDetails()
    {
        return $"Computer: {Name}, Processor: {Processor}, RAM: {RAM}GB, Storage: {Storage}GB, Price: ${Price}";
    }
}

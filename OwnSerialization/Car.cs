namespace OwnSerialization;

public class Car
{
    public string Color { get; set; }
    public string Name { get; set; }
    public int Price { get; set; }

    public List<Car> GetAll()
    {
        return
        [
            new Car { Name = "Lada", Color = "White", Price = 100 },
            new Car { Name = "Kia", Color = "Red", Price = 444 },
            new Car { Name = "Nissan", Color = "Black", Price = 777 },
            new Car { Name = "Aurus", Color = "Black", Price = 999 },
            new Car { Name = "Haval", Color = "Black", Price = 555 }
        ];        
    }  
}     
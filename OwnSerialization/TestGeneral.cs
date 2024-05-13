using System.Diagnostics;
using System.Text.Json;

namespace OwnSerialization
{
    public static class TestGeneral
    {
        public static void Activate()
        {
            var data = new CsvSerializer<Car>().SerializeToString(new Car().GetAll());

            var stopwatch = new Stopwatch();
            stopwatch.Start();
                      
            for (int i = 0; i < 1000; i++)
            {
                new CsvSerializer<Car>().SerializeToString(new Car().GetAll());
            }

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения сериализации: {stopwatch.ElapsedMilliseconds} мс");


            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                new CsvSerializer<Car>().DeserializeFromString(data);
            }           

            stopwatch.Stop();
            Console.WriteLine($"Время выполнения десериализации: {stopwatch.ElapsedMilliseconds} мс");

            var serializedJson = JsonSerializer.Serialize(new Car().GetAll());
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 1000; i++)
            {
                JsonSerializer.Serialize(new Car().GetAll());
            }
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения сериализации (System.Text.Json): {stopwatch.ElapsedMilliseconds} мс");

            stopwatch.Reset();
            stopwatch.Start();
            
            for (int i = 0; i < 1000; i++)
            {
                JsonSerializer.Deserialize<List<Car>>(serializedJson);
            }
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения десериализации (System.Text.Json): {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}

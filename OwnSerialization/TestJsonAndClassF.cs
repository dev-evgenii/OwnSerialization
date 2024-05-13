using System.Diagnostics;
using System.Text.Json;

namespace OwnSerialization
{
    public static class TestJsonAndClassF
    {
        public static void Activate()
        {           
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                JsonSerializer.Serialize(new F().Get());
            }

            stopwatch.Stop();
            var serializationJsonTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
               new CsvSerializer<F>().SerializeToString(new F().Get());
            }
            stopwatch.Stop();
            var serializationCsvTime = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Время выполнения сериализации в JSON: {serializationJsonTime} мс");
            Console.WriteLine($"Время выполнения сериализации в CSV: {serializationCsvTime} мс");
            Console.WriteLine($"Разница во времени: {serializationJsonTime - serializationCsvTime} мс");
        }
    }
}

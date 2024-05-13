using System.Diagnostics;

namespace OwnSerialization
{
    public static class TestClassF
    {
        public static void Activate()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
                        
            for (int i = 0; i < 10000; i++)
            {
                new F().Get();
            }

            stopwatch.Stop();
            var generalTime = stopwatch.ElapsedMilliseconds;

            stopwatch.Reset();
            stopwatch.Start();

            string csvString = string.Empty;
            var data = new F().Get();
            for (int i = 0; i < 10000; i++)
            {
                csvString += new CsvSerializer<F>().SerializeToString(data);
            }

            stopwatch.Stop();
            var serializationTime = stopwatch.ElapsedMilliseconds;
                        
            stopwatch.Reset();
            stopwatch.Start();

            for (int i = 0; i < 10000; i++)
            {
                Console.Write(new CsvSerializer<F>().SerializeToString(data));
            }
            stopwatch.Stop();
            Console.WriteLine($"Время выполнения без сериализации: {generalTime} мс");
            Console.WriteLine($"Время выполнения c сериализацией: {serializationTime} мс");
            Console.WriteLine($"Разница во времени: {serializationTime - generalTime} мс");
            Console.WriteLine($"Время выполнения c сериализацией и выводом на консоль: {stopwatch.ElapsedMilliseconds} мс");
        }
    }
}

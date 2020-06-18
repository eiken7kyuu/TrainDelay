using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using System.Text.Json;

namespace TrainDelay
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                var jsonString = File.ReadAllText("config.json");
                var numbers = JsonSerializer.Deserialize<Route>(jsonString).Numbers;
                var tasks = numbers.Select(number => new TrainDelay(number).Run());
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
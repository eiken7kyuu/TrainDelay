using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace TrainDelay
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tasks = File.ReadLines("routeNumbers.txt")
                .Select(number => new TrainDelay(number).Run());

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                new LineNotify().Send(e.ToString());
            }
        }
    }
}

using IOETExercise.Core;
using System;
using System.Threading.Tasks;

namespace IOETExercise
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                await ReadParseFile.ProcessFileAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing file", ex);
            }
        }
    }
}

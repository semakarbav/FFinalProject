using System;

namespace Bussiness.CCS
{
    public class DataLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("Veritabanına loglandı");
        }
    }
}

using System;

namespace Tema5
{
    class Program
    {
        public static String GetTimestamp(DateTime value)
{
    return value.ToString("yyyyMMddHHmmssffff");
}
            static void Main(string[] args)
        {
                Console.WriteLine("S-a generat la ");
                Console.WriteLine(GetTimestamp(DateTime.Now));
                new MetricRepo().Update15m().GetAwaiter().GetResult();
        }
    }
}

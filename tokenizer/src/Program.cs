using System;

namespace Pretokenizer
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                Config config = new Config(args);


            }
            catch (Exception _exc)
            {
                Console.WriteLine("--- ---\n Exception! " + _exc.Message);
                return 1;
            }

            return 0;
        }
    }
}

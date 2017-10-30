using System;

namespace ConsoleSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Console.WriteLine(typeof(string).IsPrimitive);

            Console.WriteLine(typeof(object).IsPrimitive);

            Console.WriteLine(typeof(int).IsPrimitive);

            Console.WriteLine(typeof(float).IsPrimitive);

        }
    }
}

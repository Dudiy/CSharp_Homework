using System;
using System.Text;

namespace tests
{
    class Class1
    {
        public static void Main()
        {
            Class2.test2();
            Console.ReadKey();
        }
        public static void test1()
        {
            System.Console.WriteLine("Hello I am in class 1");
        }
    }
}

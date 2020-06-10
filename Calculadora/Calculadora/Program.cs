using System;

namespace Calculadora
{
    class Program
    {
        static void Main(string[] args)
        {
            int a = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(a);
            sbyte v = Convert.ToSByte(a);
            byte b = v;


            if (a< 0)
            {
                Console.WriteLine("este numero e menor que 0");
            }
            else
            {
                Console.WriteLine("nao e menor");
            }


        }
    }
}

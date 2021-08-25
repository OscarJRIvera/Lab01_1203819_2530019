using System;
using ArbolB;


namespace Lab01_1203819_2530019
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("ingrese cantidad de valores");
            int x = Convert.ToInt32(Console.Read());
            ArbolB<int> Arbolprueba = new ArbolB<int>(5, Compararint);

            Random numerorandom = new Random();
            while (x != 0)
            {
                x--;
                int num = numerorandom.Next(1, 200);
                Arbolprueba.Add(num);

            }
        }
        public static int Compararint(int x, int y)
        {
            int r = x.CompareTo(y);
            return r;
        }
    }
}

using System;
using ArbolB;


namespace Lab01_1203819_2530019
{
    class Program
    {
        
        static void Main(string[] args)
        {
            
            ArbolB<int> Arbolprueba = new ArbolB<int>(7, Compararint);
            Random numerorandom = new Random();
            Arbolprueba.Add(70);
            Arbolprueba.Add(46);
            Arbolprueba.Add(61);
            Arbolprueba.Add(44);
            Arbolprueba.Add(3);
            Arbolprueba.Add(69);
            Arbolprueba.Add(35);
            Arbolprueba.Add(28);
            Arbolprueba.Add(78);
            Arbolprueba.Add(19);
            Arbolprueba.Add(58);
            Arbolprueba.Add(71);
            Arbolprueba.Add(41);
            Arbolprueba.Add(57);
            Arbolprueba.Add(31);
            Arbolprueba.Add(72);
            Arbolprueba.Add(73);
            Arbolprueba.Add(52);
            Arbolprueba.Add(22);
            Arbolprueba.Add(1);
            Arbolprueba.Add(89);
            Arbolprueba.Add(15);
            Arbolprueba.Add(62);
            Arbolprueba.Add(63);
            Arbolprueba.Add(64);
            Arbolprueba.Add(65);
            Arbolprueba.Add(90);
            Arbolprueba.Add(91);
            Arbolprueba.Add(45);
            Arbolprueba.Add(67);
            Arbolprueba.Add(9);
            Arbolprueba.Add(66);
            Arbolprueba.Add(68);



            //Arbolprueba.Delete(90);
            //Arbolprueba.Delete(96);
            //Arbolprueba.Delete(9);
            //Arbolprueba.Delete(81);
            //Arbolprueba.Delete(34);
            //Arbolprueba.Delete(12);

            int x = 100;
            while (x != 0)
            {
                Console.WriteLine("ingrese valor");
                int y = int.Parse(Console.ReadLine());
                if (y == 6969)
                {
                    int[] arreg = Arbolprueba.Recorridos2(1);
                    int cant = 0;
                    while (arreg[cant] != 0)
                    {
                        Console.Write(arreg[cant] + " ");
                        cant++;
                    }
                }
                else
                {
                    Arbolprueba.Delete(y);
                }
                x--;
                //int num = numerorandom.Next(1, 200);
                //Arbolprueba.Add(num);

            }
        }
        public static int Compararint (int x, int y)
        {
            int r = x.CompareTo(y);
            return r;
        }
    }
}

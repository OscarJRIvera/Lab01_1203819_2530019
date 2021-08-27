using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArbolB;

namespace Api.Models.Data
{
    public class Singleton
    {
        private readonly static Singleton _instance = new Singleton();

        public ArbolB<Movie> Arbolb;
        public int tamaño;
        private Singleton()
        {
            tamaño = 5;
         Arbolb = new ArbolB<Movie>(5,Movie.Compare_Title);
        }
        public static Singleton Instance
        {
            get
            {
                return _instance;
            }
        }
    }
}

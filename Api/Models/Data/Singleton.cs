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
        public int id;

        public ArbolB<Movie> Arbolb;
        public int tamaño;
        private Singleton()
        {
            id = 1;
            tamaño = 3;
            Arbolb = new ArbolB<Movie>(3, Movie.Compare_id);
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

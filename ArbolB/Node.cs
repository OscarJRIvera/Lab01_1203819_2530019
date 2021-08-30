using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolB
{
    class Node<T>
    {
        internal NodosInternos<T>[] nodosInternos;
        private ArbolB<T>.Comparador<T> comparador;



        public Node(int n, ArbolB<T>.Comparador<T> comparador)
        {
            nodosInternos = new NodosInternos<T>[n];
            this.comparador = comparador;
        }
        public void empty()
        {
            for (int x = 0; x < nodosInternos.Length; x++)
            {
                nodosInternos[x] = null;
            }
        }


        public bool IsEmpy()
        {
            if (nodosInternos[0] == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void AddValue(T value)
        {
            if (IsEmpy())
            {
                nodosInternos[0] = new NodosInternos<T>()
                {
                    Value = value
                };
            }
            else
            {
                int i = 0;
                while (nodosInternos[i] != null)
                {
                    i++;
                }
                nodosInternos[i] = new NodosInternos<T>()
                {
                    Value = value
                };
            }

            CorrectOrder();
        }
        public void eliminar1(int posicion)
        {
            nodosInternos[posicion] = null;

            while (posicion < nodosInternos.Length - 1)
            {

                nodosInternos[posicion] = nodosInternos[posicion + 1];
                posicion++;
            }
        }
        internal void eliminar2(int posicion, ref Node<T> root)
        {
            if (comparador.Invoke(nodosInternos[0].Value, root.nodosInternos[0].Value) == 0 && root.valueslength() == 1)
            {
                root = root.nodosInternos[0].Left;
            }
            else
            {
                nodosInternos[posicion] = null;

                while (posicion < nodosInternos.Length - 1)
                {

                    nodosInternos[posicion] = nodosInternos[posicion + 1];
                    posicion++;
                }
            }


        }
        public void AddValue2(NodosInternos<T> value)
        {
            if (IsEmpy())
            {
                nodosInternos[0] = new NodosInternos<T>();
                nodosInternos[0] = value;

            }
            else
            {
                int i = 0;
                while (nodosInternos[i] != null)
                {
                    i++;
                }
                nodosInternos[i] = new NodosInternos<T>();
                nodosInternos[i] = value;
            }

            CorrectOrder();
        }
        public void CorrectOrder()
        {
            int length = 0;
            for (int x = 0; x < nodosInternos.Length; x++)
            {
                if (nodosInternos[length] != null)
                {
                    length++;
                }


            }
            for (int i = 0; i < length - 1; i++)
            {
                for (int j = i + 1; j < length; j++)
                {
                    if (comparador.Invoke(nodosInternos[i].Value, nodosInternos[j].Value) > 0)
                    {
                        var t = nodosInternos[i];
                        nodosInternos[i] = nodosInternos[j];
                        nodosInternos[j] = t;
                    }
                }
            }
        }
        public bool IsFull()
        {
            foreach (var item in nodosInternos)
            {
                if (item == null)
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsFull2()
        {
            int x = 0;
            foreach (var item in nodosInternos)
            {
                if (item != null)
                {
                    x++;
                }
            }
            if (x == (nodosInternos.Length - 1))
            {
                return true;
            }
            return false;
        }
        public bool tienehijos()
        {
            return nodosInternos[0].Left != null;
        }
        public int valueslength()
        {
            int x = 0;
            for (int i = 0; i < nodosInternos.Length; i++)
            {
                if (nodosInternos[i] != null)
                {
                    x++;
                }
            }
            return x;
        }

    }


}

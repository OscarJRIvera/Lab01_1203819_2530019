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
            nodosInternos = new NodosInternos<T>[n - 1];
            this.comparador = comparador;
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
                        var t = nodosInternos[i].Value;
                        nodosInternos[i].Value = nodosInternos[j].Value;
                        nodosInternos[j].Value = t;
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
        public bool tienehijos()
        {
            return nodosInternos[0].Left != null;
        }

    }


}

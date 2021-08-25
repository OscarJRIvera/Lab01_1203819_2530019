using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolB
{
    class Node<T>
    {
        internal NodosInternos<T>[] nodosInternos;       
        private int n;
        private ArbolB<T>.Comparador<T> comparador;

        public Node(int n, ArbolB<T>.Comparador<T> comparador)
        {
            this.n = n;
            this.comparador = comparador;
        }

        public bool IsEmpy()
        {
            foreach (var item in nodosInternos)
            {
                if (item!=null)
                {
                    return false; //osea que no esta vacío
                }
            }
            return true;
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
        }
        public void CorrectOrder()
        {
            for (int i = 0; i < nodosInternos.Length -1 ; i++)
            {
                for (int j = i + 1; j < nodosInternos.Length; j++)
                {
                    if(comparador.Invoke(nodosInternos[i].Value,nodosInternos[j].Value) > 0)
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
      
    }


}

using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolB
{
    public class ArbolB<T>
    {
        internal Node<T> root;
        internal int n;
        public delegate int Comparador<T>(T a, T b);
        internal Comparador<T> comparador;//una de las propiedades del arbol es el comparador
        public ArbolB(int n, Comparador<T> Funcomparador) //Esta es la funcion
        {
            this.n = n;
            this.comparador = Funcomparador;// el apuntador de la linea 12 que apunte a la funcion. 
        }
        public void Empty()
        {
            root = null;
        }
        public bool IsEmpty()
        {
            if (root == null)
            {
                return true;
            }
            else if (root.IsEmpy())
            {
                return true;
            }
            return false;
        }

        public void Add(T dato)
        {
            if (root == null)
            {
                root = new Node<T>(n, comparador);
                root.AddValue(dato);
            }
            else
            {
                if (root.IsFull())
                {
                    NodosInternos<T>[] aux = new NodosInternos<T>[n];
                    int k = 0;
                    foreach (var item in root.nodosInternos)
                    {
                        aux[k] = item;
                        k++;
                    }
                    aux[k] = new NodosInternos<T>
                    {
                        Value = dato
                    };
                    for (int i = 0; i < aux.Length - 1; i++)
                    {
                        for (int j = i + 1; j < aux.Length; j++)
                        {
                            if (comparador.Invoke(aux[i].Value, aux[j].Value) > 0)
                            {
                                var t = aux[i].Value;
                                aux[i].Value = aux[j].Value;
                                aux[j].Value = t;
                            }
                        }
                    }
                    Node<T> left = new Node<T>(n, comparador);
                    Node<T> right = new Node<T>(n, comparador);
                }
                else
                {
                    root.AddValue(dato);
                }
            }
        }
        internal T Divide(NodosInternos<T>[] arr, ref Node<T> left, ref Node<T> right)
        {
            for (int i = 0; i < arr.Length/2; i++)
            {
                left.AddValue(arr[i].Value);
            }
            for (int i = arr.Length / 2 + 1; i < arr.Length ; i++)
            {
                right.AddValue(arr[i].Value);
            }
            return arr[arr.Length / 2].Value;
        }
        
    }
}

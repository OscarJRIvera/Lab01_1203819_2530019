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
                add2(dato, root, null);
            }

        }
        private void add2(T dato, Node<T> Actual, Node<T> AnteriorActual)
        {
            if (find(dato, Actual) == false)
            {
                if (Actual.IsFull())
                {
                    if (Actual.tienehijos())
                    {
                        int i = 0;
                        while (Actual.nodosInternos[i].Value != null)
                        {
                            if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == -1)
                            {
                                add2(dato, Actual.nodosInternos[i].Left, Actual);
                            }

                            if (Actual.nodosInternos[i + 1] == null)
                            {
                                add2(dato, Actual.nodosInternos[i].Right, Actual);
                            }
                            i++;
                        }
                    }
                    else
                    {
                        if (AnteriorActual == null)
                        {
                            Node<T> aux = new Node<T>(n + 1, comparador);
                            int k = 0;
                            foreach (var item in Actual.nodosInternos)
                            {
                                aux.nodosInternos[k] = item;
                                k++;
                            }
                            aux.AddValue(dato);
                            Node<T> Left = new Node<T>(n, comparador);
                            Node<T> Right = new Node<T>(n, comparador);
                            Node<T> Tempactual = new Node<T>(n, comparador);
                            Tempactual = Actual;
                            Actual = new Node<T>(n, comparador);
                            Actual.AddValue(Divide(aux, ref Left, ref Right));
                            Actual.nodosInternos[0].Left = Left;
                            Actual.nodosInternos[0].Right = Right;
                        }

                    }
                }
                else
                {
                    Actual.AddValue(dato);
                    if (Actual.tienehijos())
                    {

                    }
                }
            }

        }
        internal T Divide(Node<T> arr, ref Node<T> left, ref Node<T> right)
        {
            for (int i = 0; i < arr.nodosInternos.Length / 2; i++)
            {
                left.AddValue(arr.nodosInternos[i].Value);
            }
            for (int i = arr.nodosInternos.Length / 2 + 1; i < arr.nodosInternos.Length; i++)
            {
                right.AddValue(arr.nodosInternos[i].Value);
            }
            return arr.nodosInternos[arr.nodosInternos.Length / 2].Value;
        }
        internal T ValorEnMedio(Node<T> arr)
        {
            return arr.nodosInternos[arr.nodosInternos.Length / 2].Value;
        }
        private bool find(T dato, Node<T> Actual)
        {
            if (Actual.tienehijos())
            {
                int i = 0;
                while (Actual.nodosInternos[i].Value != null)
                {
                    if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == 0)
                    {
                        return true;
                    }
                    else if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == -1)
                    {
                        find(dato, Actual.nodosInternos[i].Left);
                    }
                    else if (Actual.nodosInternos[i + 1] == null)
                    {
                        find(dato, Actual.nodosInternos[i].Right);
                    }
                    i++;
                }
                return false;
            }
            else
            {
                for (int x = 0; x < n - 1; x++)
                {
                    if (Actual.nodosInternos[x] != null)
                    {
                        if (comparador.Invoke(dato, Actual.nodosInternos[x].Value) == 0)
                        {
                            return true;
                        }
                    }


                }
                return false;
            }
        }

    }
}

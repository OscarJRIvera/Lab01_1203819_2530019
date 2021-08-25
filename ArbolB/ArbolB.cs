using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolB
{
    public class ArbolB<T>
    {
        public bool ValorRepitodo;
        public bool valoringresado;
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
            valoringresado = false;
            if (root == null)
            {
                root = new Node<T>(n, comparador);
                root.AddValue(dato);
            }
            else
            {
                add2(dato, root,null);
            }
           
        }
        
        
        private void add2(T dato, Node<T> Actual, Node<T> AnteriorActual)
        {
            bool ciclo = false;
            find(dato, Actual);
            if (ValorRepitodo!=true)
            {
                if (Actual.tienehijos())
                {
                    int i = 0;
                    while (Actual.nodosInternos[i] != null && ciclo != true)
                    {
                        if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == -1)
                        {
                            add2(dato, Actual.nodosInternos[i].Left, Actual);
                            ciclo = true;
                        }
                        else if (Actual.nodosInternos[i + 1] == null)
                        {
                            add2(dato, Actual.nodosInternos[i].Right, Actual);
                            ciclo = true;
                        }
                        i++;
                    }
                }
                if (valoringresado == false)
                {
                    if (Actual.IsFull2())
                    {
                        Node<T> Left = new Node<T>(n, comparador);
                        Node<T> Right = new Node<T>(n, comparador);
                        valoringresado = true;
                        Node<T> aux = new Node<T>(n, comparador);
                        int k = 0;
                        foreach (var item in Actual.nodosInternos)
                        {
                            aux.nodosInternos[k] = item;
                            k++;
                        }
                        aux.AddValue(dato);
                        if (AnteriorActual == null)
                        {
                            partirraiz(ref Left, ref Right, ref Actual, ref aux);
                            int x = 0;
                        }
                        else
                        {
                            subir(ref Right, ref Actual, ref aux, ref AnteriorActual);
                        }
                    }
                    else
                    {
                        Actual.AddValue(dato);
                        valoringresado = true;
                    }
                }
                else
                {
                    if (Actual.IsFull())
                    {
                        Node<T> Left = new Node<T>(n, comparador);
                        Node<T> Right = new Node<T>(n, comparador);
                        Node<T> Temp = new Node<T>(n, comparador);
                        int k = 0;
                        foreach (var item in Actual.nodosInternos)
                        {
                            Temp.nodosInternos[k] = item;
                            k++;
                        }
                        if (AnteriorActual == null)
                        {
                            partirraiz(ref Left, ref Right, ref Actual, ref Temp);
                        }
                        else
                        {
                            subir(ref Right, ref Actual, ref Temp, ref AnteriorActual);
                        }
                    }
                }
            }
           
            
        }
        internal void partirraiz(ref Node<T> Left, ref Node<T> Right, ref Node<T> Actual, ref Node<T> Temp)
        {
            Actual.empty();
            Actual.AddValue(Divide(Temp, ref Left, ref Right));
            Actual.nodosInternos[0].Left = Left;
            Actual.nodosInternos[0].Right = Right;
        }
        internal void subir(ref Node<T> Right, ref Node<T> Actual, ref Node<T> Temp, ref Node<T> AnteriorActual)
        {

            Actual.empty();
            Divide(Temp, ref Actual, ref Right);
            AnteriorActual.AddValue(ValorEnMedio(Temp));
            AnteriorActual.nodosInternos[findpos(ValorEnMedio(Temp), AnteriorActual)].Left = Actual;
            AnteriorActual.nodosInternos[findpos(ValorEnMedio(Temp), AnteriorActual)].Right = Right;
            if (!(AnteriorActual.IsFull() && findpos(ValorEnMedio(Temp), AnteriorActual)==n-1))
            {
                if (AnteriorActual.nodosInternos[findpos(ValorEnMedio(Temp), AnteriorActual) + 1] != null)
                {
                    AnteriorActual.nodosInternos[findpos(ValorEnMedio(Temp), AnteriorActual) + 1].Left = AnteriorActual.nodosInternos[findpos(ValorEnMedio(Temp), AnteriorActual)].Right;
                }
            }
           
        }
        internal T Divide(Node<T> arr, ref Node<T> left, ref Node<T> right)
        {
            for (int i = 0; i < arr.nodosInternos.Length / 2; i++)
            {
                left.AddValue2(arr.nodosInternos[i]);
            }
            for (int i = arr.nodosInternos.Length / 2 + 1; i < arr.nodosInternos.Length; i++)
            {
                right.AddValue2(arr.nodosInternos[i]);
            }
            return arr.nodosInternos[arr.nodosInternos.Length / 2].Value;
        }
        internal T ValorEnMedio(Node<T> arr)
        {
            return arr.nodosInternos[arr.nodosInternos.Length / 2].Value;
        }
        private int findpos(T dato, Node<T> Actual)
        {
            for (int x = 0; x < n; x++)
            {
                if (comparador.Invoke(dato, Actual.nodosInternos[x].Value) == 0)
                {
                    return x;
                }
            }
            return -1;
        }
        private void find(T dato,Node<T> Actual)
        {
            ValorRepitodo = false;
            bool ciclo = false;
            if (Actual.tienehijos())
            {
                
                int i = 0;
                while (Actual.nodosInternos[i] != null && ciclo!=true)
                {
                    if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == 0)
                    {
                        ValorRepitodo = true;
                    }
                    else if (comparador.Invoke(dato, Actual.nodosInternos[i].Value) == -1)
                    {
                        find(dato, Actual.nodosInternos[i].Left);
                        ciclo = true;
                    }
                    else if (Actual.nodosInternos[i + 1] == null)
                    {
                        find(dato, Actual.nodosInternos[i].Right);
                        ciclo = true;
                    }
                    i++;
                }
                
            }
            else
            {
                for (int x=0;x< n-1;x++)
                {
                    if (Actual.nodosInternos[x] != null)
                    {
                        if (comparador.Invoke(dato, Actual.nodosInternos[x].Value) == 0)
                        {
                            ValorRepitodo = true;
                        }
                    }
                    

                }
            }
            
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace ArbolB
{
    public class ArbolB<T>
    {
        public bool sucesorentra;
        public T SucesorV;
        public int minimo;
        public List<T> pre = new List<T>();
        public bool ValorRepitodo;
        public bool valoringresado;
        internal Node<T> root;
        internal Node<T> Hijoiz;
        internal Node<T> hijode;
        internal int n;
        public delegate int Comparador<T>(T a, T b);
        internal Comparador<T> comparador;//una de las propiedades del arbol es el comparador
        public ArbolB(int n, Comparador<T> Funcomparador) //Esta es la funcion
        {
            Hijoiz = new Node<T>(n, comparador);
            hijode = new Node<T>(n, comparador);
            SucesorV = default;
            sucesorentra = false;
            minimo = (n / 2);
            this.n = n;
            this.comparador = Funcomparador;// el apuntador de la linea 12 que apunte a la funcion. 
        }
        public List<T> Recorridos(int x)
        {
            pre = new List<T>();
            if (x == 1)
            {
                Preorder(root);
            }
            else if (x == 2)
            {
                inorder(root);
            }
            else
            {
               postorder(root);
            }
            return pre;
        }
        public int[] Recorridos2(int x)
        {
            pre = new List<T>();
            if (x == 1)
            {
                Preorder(root);
            }
            else if (x == 2)
            {
                inorder(root);
            }
            else
            {
                postorder(root);
            }
            int i = 0;
            int[] xd = new int[1000];
            while (i < pre.Count)
            {
                xd[i] = Convert.ToInt32(pre[i]);
                i++;
            }
            return xd;
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
                        ciclo = true;
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
        internal void recorridohoja(Node<T> Actual)
        {
            for (int i = 0; i < Actual.valueslength(); i++)
            {
                pre.Add(Actual.nodosInternos[i].Value);
            }
        }
        internal void postorder(Node<T> Actual)
        {
            for (int i = 0; i < Actual.valueslength(); i++)
            {
                if (Actual.tienehijos())
                {
                    if (i == 0)
                    {
                        postorder(Actual.nodosInternos[i].Left);
                    }
                    postorder(Actual.nodosInternos[i].Right);
                    pre.Add(Actual.nodosInternos[i].Value);
                }
                else
                {
                    recorridohoja(Actual);
                    i = Actual.valueslength();
                }
            }
        }
        internal void inorder(Node<T> Actual)
        {
            for (int i = 0; i < Actual.valueslength(); i++)
            {
                if (Actual.tienehijos())
                {
                    if (i == 0)
                    {
                        inorder(Actual.nodosInternos[i].Left);
                    }
                    pre.Add(Actual.nodosInternos[i].Value);
                    inorder(Actual.nodosInternos[i].Right);

                }
                else
                {
                    recorridohoja(Actual);
                    i = Actual.valueslength();
                }
            }
        }
        internal void Preorder(Node<T> Actual)
        {
            for (int i = 0; i < Actual.valueslength(); i++)
            {
                if (!Actual.tienehijos())
                {
                    recorridohoja(Actual);
                    i = Actual.valueslength();
                }
                else
                {
                    pre.Add(Actual.nodosInternos[i].Value);
                    if (i == 0)
                    {

                        Preorder(Actual.nodosInternos[i].Left);
                        Preorder(Actual.nodosInternos[i].Right);
                    }
                    else
                    {
                        Preorder(Actual.nodosInternos[i].Right);
                    }
                }


            }
        }
        public bool Delete(T value)
        {
            find(value, root);
            if (ValorRepitodo == true)
            {
                Delete2(value, root, null);
                return true;
            }
            else
            {
                return false;
            }
            
        }
        private void Delete2(T value, Node<T> Actual, Node<T> Padreactual)
        {
            int posicionpadre=0;
            for (int i = 0; i < Actual.valueslength(); i++)
            {
                if (comparador.Invoke(value, Actual.nodosInternos[i].Value)==0)
                {
                    posicionpadre = Buscarposicionpadre(Actual, Padreactual, i);

                    if (Actual.tienehijos())
                    {
                        sucesorentra = false;
                        Sucesor(Actual, Padreactual, i);
                        Actual.nodosInternos[i].Value = SucesorV;
                        Delete2(SucesorV, Actual.nodosInternos[i].Right, Actual);
                        
                    }
                    else
                    {
                       hijode = Actual.nodosInternos[i].Right;
                        Hijoiz = Actual.nodosInternos[i].Left;
                        if (Padreactual != null)
                        {
                            Actual.eliminar1(i);
                        }
                        else
                        {
                            Actual.eliminar2(i, ref root);
                        }
                    }
                    i = n;
                }
                else if (comparador.Invoke(value, Actual.nodosInternos[i].Value) == -1)
                {
                    posicionpadre = Buscarposicionpadre(Actual, Padreactual, i);
                    Delete2(value, Actual.nodosInternos[i].Left,Actual);
                    i = n;
                }
                else if (Actual.nodosInternos[i + 1] == null)
                {
                    posicionpadre = Buscarposicionpadre(Actual, Padreactual, i);
                    Delete2(value, Actual.nodosInternos[i].Right, Actual);
                    i = n;

                }
                if (Actual.valueslength() < minimo)
                {
                    if (Padreactual != null)
                    {
                        
                        if (!redistribucion(ref Actual, ref Padreactual, posicionpadre))
                        {
                            union(ref Actual, ref Padreactual, posicionpadre);
                        }
                    }
                    
                }
            }
        }
        private void Sucesor(Node<T> Actual, Node<T> Padre, int posicion)
        {
            SucesorV = default;
            if (sucesorentra == false)
            {
                sucesorentra = true;
                 Sucesor(Actual.nodosInternos[posicion].Right, Actual, posicion);
            }
            else if (Actual.nodosInternos[0].Left != null)
            {
                 Sucesor(Actual.nodosInternos[0].Left, Actual, posicion);
            }
            else
            {
               SucesorV= Actual.nodosInternos[0].Value;
            }
            
        }
        private Node<T> Unirnodos(Node<T> Hijo1, Node<T> Padre, Node<T> Hijo2,int posicionpadre, Node<T> nieto) // :D
        {
            int x = 0;
            Node<T> Newnode = new Node<T>(n, comparador);
            for (int i = 0; i < Hijo1.valueslength(); i++)
            {
                Newnode.AddValue2(Hijo1.nodosInternos[i]);
            }
            Newnode.AddValue(Padre.nodosInternos[posicionpadre].Value);
            for (int i = 0; i < Hijo2.valueslength(); i++)
            {
                Newnode.AddValue2(Hijo2.nodosInternos[i]);
            }
            x = findpos(Padre.nodosInternos[posicionpadre].Value, Newnode);
            if (n == 3)
            {
                if (x == 0)
                {
                    Newnode.nodosInternos[x].Left = nieto; Newnode.nodosInternos[x].Right = Newnode.nodosInternos[x + 1].Left;
                }
                else
                {
                    Newnode.nodosInternos[x].Left = Newnode.nodosInternos[x - 1].Right; Newnode.nodosInternos[x].Right = nieto;
                }
                
            }
            else
            {
                Newnode.nodosInternos[x].Left = Newnode.nodosInternos[x - 1].Right; Newnode.nodosInternos[x].Right = Newnode.nodosInternos[x + 1].Left;
            }
           
            return Newnode;
        }
        private int Buscarposicionpadre(Node<T> Actual, Node<T> Padreactual, int posicion)
        {
            int posicionpadre = 0;
            if (!(Padreactual == null))
            {
                
                for (int i = 0; i < Padreactual.valueslength(); i++)
                {
                    posicionpadre = i;
                    if (comparador.Invoke(Actual.nodosInternos[posicion].Value, Padreactual.nodosInternos[i].Value) == -1)
                    {
                        i = Padreactual.valueslength();
                    }
                }
            }
            return posicionpadre;
        }
        internal void union(ref Node<T> Actual,ref Node<T> Padreactual, int posicionpadre)
        {
            bool hijoderecho=false;
            if (Actual.valueslength() == 0)
            {
                hijoderecho = (Padreactual.nodosInternos[posicionpadre].Right.valueslength() == 0);
            }
            else
            {
                hijoderecho = comparador.Invoke(Padreactual.nodosInternos[posicionpadre].Right.nodosInternos[0].Value, Actual.nodosInternos[0].Value) == 0;
            }
            Node<T> NewNode= new Node<T>(n,comparador);
            if (Padreactual.nodosInternos[posicionpadre].Right != null && !(hijoderecho))
            {
                NewNode = Unirnodos(Padreactual.nodosInternos[posicionpadre].Right, Padreactual,Actual, posicionpadre, hijode) ;
            }
            else
            {
                NewNode = Unirnodos(Padreactual.nodosInternos[posicionpadre].Left, Padreactual,Actual, posicionpadre, Hijoiz);
            }
            if (Padreactual.valueslength()>1)
            {
                if (posicionpadre == 0)
                {
                    Padreactual.nodosInternos[1].Left = NewNode;
                }
                else if (posicionpadre == Padreactual.valueslength() - 1)
                {
                    Padreactual.nodosInternos[Padreactual.valueslength() - 2].Right = NewNode;
                }
                else
                {
                    Padreactual.nodosInternos[posicionpadre - 1].Right = NewNode;
                    Padreactual.nodosInternos[posicionpadre + 1].Left = NewNode;
                }

            }
            else
            {
                
                Padreactual.nodosInternos[posicionpadre].Right = NewNode;
                Padreactual.nodosInternos[posicionpadre].Left = NewNode;
                hijode = Padreactual.nodosInternos[posicionpadre].Right;
                Hijoiz = Padreactual.nodosInternos[posicionpadre].Left;
            }
            Padreactual.eliminar2(posicionpadre,ref root);
        }
        internal bool redistribucion(ref Node<T> Actual, ref Node<T> Padreactual, int posicionpadre)
        {
            bool verificar = false;
            bool hijoderecho = false;
            bool hijoizquirdo = false;
           
            bool cambio=false;
            if (Padreactual.valueslength() > 1)
            {
                if (!(Padreactual.valueslength() - 1 == posicionpadre))
                {
                    if (hijoderecho)
                    {
                        posicionpadre = +1;
                        cambio = true;
                    }
                }
            }
            if (Actual.valueslength() == 0)
            {
                hijoderecho = Padreactual.nodosInternos[posicionpadre].Right.valueslength() == 0;
                hijoizquirdo = Padreactual.nodosInternos[posicionpadre].Left.valueslength() == 0;
            }
            else
            {
                hijoderecho = comparador.Invoke(Padreactual.nodosInternos[posicionpadre].Right.nodosInternos[0].Value, Actual.nodosInternos[0].Value) == 0;
                hijoizquirdo = comparador.Invoke(Padreactual.nodosInternos[posicionpadre].Left.nodosInternos[0].Value, Actual.nodosInternos[0].Value) == 0;
            }
            if (!(hijoderecho) && (Padreactual.nodosInternos[posicionpadre].Right.valueslength() > minimo))
            {
                NodosInternos<T> Temp = new NodosInternos<T>();
                Temp.Right = Padreactual.nodosInternos[posicionpadre].Right.nodosInternos[0].Left;
                if (Actual.valueslength() == 0){
                    Temp.Left = Hijoiz;
                }
                else{
                    Temp.Left = Actual.nodosInternos[Actual.valueslength() - 1].Right;
                }
                Temp.Value = Padreactual.nodosInternos[posicionpadre].Value;
                Actual.AddValue2(Temp);
                T temp = Padreactual.nodosInternos[posicionpadre].Right.nodosInternos[0].Value;
                Padreactual.nodosInternos[posicionpadre].Right.eliminar2(0, ref root);
                Padreactual.nodosInternos[posicionpadre].Value = temp;
                verificar = true;
            }
            else
            {
                if (cambio == true)
                {
                    if (hijoderecho)
                    {
                        posicionpadre = -1;
                    }
                }
                if (Padreactual.valueslength() > 1) 
                {
                    if (posicionpadre!=0)
                    {
                        if (!hijoderecho)
                        {
                            posicionpadre = posicionpadre - 1;
                        }
                    }
                }
                if (Actual.valueslength() == 0)
                {
                    hijoderecho = Padreactual.nodosInternos[posicionpadre].Right.valueslength() == 0;
                    hijoizquirdo = Padreactual.nodosInternos[posicionpadre].Left.valueslength() == 0;
                }
                else
                {
                    hijoderecho = comparador.Invoke(Padreactual.nodosInternos[posicionpadre].Right.nodosInternos[0].Value, Actual.nodosInternos[0].Value) == 0;
                    hijoizquirdo = comparador.Invoke(Padreactual.nodosInternos[posicionpadre].Left.nodosInternos[0].Value, Actual.nodosInternos[0].Value) == 0;
                }
                if (!(hijoizquirdo) && (Padreactual.nodosInternos[posicionpadre].Left.valueslength() > minimo))
                {
                    NodosInternos<T> Temp = new NodosInternos<T>();
                    Temp.Left = Padreactual.nodosInternos[posicionpadre].Left.nodosInternos[Padreactual.nodosInternos[posicionpadre].Left.valueslength() - 1].Right;
                    if (Actual.valueslength() == 0)
                    {
                        Temp.Right = hijode;
                    }
                    else
                    {
                        Temp.Right = Actual.nodosInternos[0].Left;
                    }
                    Temp.Value = Padreactual.nodosInternos[posicionpadre].Value;
                    Actual.AddValue2(Temp);
                    T temp = Padreactual.nodosInternos[posicionpadre].Left.nodosInternos[Padreactual.nodosInternos[posicionpadre].Left.valueslength() - 1].Value;
                    Padreactual.nodosInternos[posicionpadre].Left.eliminar2(Padreactual.nodosInternos[posicionpadre].Left.valueslength() - 1, ref root);
                    Padreactual.nodosInternos[posicionpadre].Value = temp;
                    verificar = true;
                }
            }
            return verificar;
        }
    }
}

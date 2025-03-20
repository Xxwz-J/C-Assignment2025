using System;
using System.Security.AccessControl;
using static homework4_1.Program;

namespace homework4_1
{
    internal class Program
    {
       
        public class Node<T>
        {
            public Node<T> Next { get; set; } 
            public T Data { get; set; }        

            
            public Node(T t)
            {
                Next = null;  
                Data = t;      
            }
        }
        public class GenericList<T>
        {
            private Node<T> head;
            private Node<T> tail;

            public GenericList()
            {
                tail = head = null;
            }

            public Node<T> Head
            {
                get => head;
            }

            public void Add(T t)
            {
                Node<T> n = new Node<T>(t);  
                if (tail == null)
                {        
                    head = tail = n;
                }
                else
                {                    
                    tail.Next = n;            
                    tail = n;                
                }
            }

            public void ForEach(Action<Node<T>> action)
            {
                Node < T > p = head;
                while(p != null) {
                    action(p);
                    p= p.Next;
                }
            }
        }
        static void Main(string[] args)
        {
           GenericList<int> list = new GenericList<int>();
            list.Add(2);
            list.Add(1);
            list.Add(4);
            list.Add(3);
            int min = int.MaxValue, max = int.MinValue;
            double sum=0, lenth=0;
            list.ForEach(item => { 
            if(item.Data >max) max= item.Data;
            if(item.Data <min) min= item.Data;
            lenth++;
            sum+= item.Data;
            });
            Console.WriteLine($"max:{max} min:{min} sum:{sum} ave:{(double)sum / (double)lenth}");

        }
    }
}

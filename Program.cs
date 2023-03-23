using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Stack
{
    public class Stack_Array<T>
    {
        private T[] arr;
        private int top;

        public Stack_Array (int size)
        {
            this.arr = new T[size];
            this.top = -1;
        }

        public void Push (T data)
        {
            this.arr[++top] = data;
        }

        public T Pop()
        {
            T PeekData;
            PeekData = this.arr[this.top];
            this.top--;
            return PeekData;
        }

    }

    class Node<T>
    {
        internal T data; 
        internal Node<T> next; 
        public Node(T data)
        {
            this.data = data;
            next = null;
        }
    }

    class LinkedList2<T>
    {
        Node<T> head;

        internal void AddLast(T data)
        {
            Node<T> node = new Node<T>(data);
            if (head == null)
            {
                head = node;
                return;
            }
            Node<T> lastNode = GetLastNode();
            lastNode.next = node;
        }

        internal Node<T> GetLastNode()
        {
            Node<T> temp = head;
            while (temp.next != null)
            {
                temp = temp.next; 
            }
            return temp; 
        }

        internal void RemoveLast()
        {
            Node<T> temp = head;
            Node<T> prev = null;

            if(temp.next == null)
            {
                temp = null;
                return;
            }

            while (temp.next != null)
            {
                prev = temp;
                temp = temp.next;
            }
             prev.next = null;
        }

    }

    public class Stack_LList<T> {
        LinkedList2<T> list;

        public Stack_LList ()
        {
            this.list = new LinkedList2<T>();
        }
        
        public void Push(T data)
        {
            this.list.AddLast(data);
        }

        public T Pop()
        {
            Node<T> lastnode = list.GetLastNode();
            T Ldata = lastnode.data;
            list.RemoveLast();
            return Ldata;
        }

    }
internal class Program
    {
        static void Main(string[] args)
        {
            Stack_Array<string> stack1 = new Stack_Array<string>(10);
            Stack_LList<int> stack2 = new Stack_LList<int>();

            stack1.Push("A");
            stack1.Push("B");
            stack1.Push("C");
            System.Console.WriteLine(stack1.Pop());
            System.Console.WriteLine(stack1.Pop());
            System.Console.WriteLine(stack1.Pop());

            stack2.Push(1);
            stack2.Push(2);
            stack2.Push(3);
            System.Console.WriteLine(stack2.Pop());
            System.Console.WriteLine(stack2.Pop());
            System.Console.WriteLine(stack2.Pop());

        }
    }
}

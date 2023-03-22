using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public class Stack_LList<T> {
        LinkedList<T> list;

        public Stack_LList ()
        {
            this.list = new LinkedList<T>();
        }
        
        public void Push(T data)
        {
            this.list.AddLast(data);
        }

        public T Pop()
        {
            LinkedListNode<T> lastnode = list.Last;
            list.RemoveLast();
            return lastnode.Value;
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

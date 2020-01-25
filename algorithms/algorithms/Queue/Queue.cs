using System;

namespace algorithms.Queue
{
    public class Queue<T>
    {
        public class Node<T>
        {
            public T data;
            public Node<T> next = null;

            public Node(T data)
            {
                this.data = data;
            }
        }    
       
        public Node<T> first;
        public Node<T> last;

        public void Add(T item)
        {
            var newNode = new Node<T>(item);

            if(last != null)
            {
                last.next = newNode;
            }

            last = newNode;

            if (first == null)
            {
                first = last;
            }
        }

        public T Remove()
        {
            if (first.data == null)
            {
                throw new Exception("No such element");
            }

            T data = first.data;

            first = first.next;

            if (first == null)
            {
                last = null;
            }

            return data;
        }
    }
}

using System;

namespace algorithms.Stack
{
    public class MyStack<T>
    {
        private T data;
        private MyStack<T> next;

        public MyStack(T data)
        {
            this.data = data;
        }

        private MyStack<T> top;

        public T Pop()
        {
            if(top == null)
            {
                throw new Exception("Stack is empty");
            }

            T item = top.data;
            top = top.next;
            return item;
        }

        public void Push(T item)
        {
            MyStack<T> newNode = new MyStack<T>(item);
            newNode.next = top;
            top = newNode;
        }

        public T GetTop()
        {
            if(top == null)
            {
                throw new Exception("Stack is empty");
            }

            return top.data;
        }

        public bool IsEmpty()
        {
            return top == null;
        }
    }
}

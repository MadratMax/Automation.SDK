using System;

namespace algorithms
{
    public class LinkedList
    {
        LinkedList next = null;
        int data = 0;

        public LinkedList(int node)
        {
            data = node;
        }

        public void Append(int node)
        {
            LinkedList end = new LinkedList(node);
            LinkedList n = this;

            while(n.next != null)
            {
                n = n.next;
            }

            n.next = end;
        }
    }
}

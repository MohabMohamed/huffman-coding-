using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace huffman_coding_1
{
    public class Node
    {

        public char Data { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }
        public string huffman_code {get; set;}

        public Node(char data, int freq)
        {
            Right = Left = null;
            this.Data = data;
            this.Frequency = freq;
        }

        public Node(Node left, Node right)
        {
            this.Left = left;
            this.Right = right;
            Frequency = left.Frequency + right.Frequency;
        }

        public int CompareTo(Node x)
        {
            if (this.Frequency > x.Frequency)
                return 1;
            else if (this.Frequency < x.Frequency)
                return -1;
            else
                return 0;

        }
        
    }

   

}

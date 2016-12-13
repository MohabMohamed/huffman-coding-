using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace huffman_coding_1
{


    public class HuffmanTree
    {
        public Dictionary<char, int> Dict { get; set;}
        public List<Node> nodelist { get; set; }
        public Dictionary<char, string> HuffmanDict { get; set; }
        public Node Root;
        public string decoded = null;


        public HuffmanTree()
        {
            HuffmanDict = new Dictionary<char, string>();
            Dict = new Dictionary<char, int>();
            nodelist = new List<Node>();
        }

        public void setDict(string input)
        {
            //count letters frequency
            foreach (char currentChar in input)
            {
                if (Dict.ContainsKey(currentChar))
                { Dict[currentChar]++; }
                else
                { Dict.Add(currentChar, 1); }
            }

            List<KeyValuePair<char, int>> sortedlist = Dict.ToList();
            var sorted = from entry in Dict orderby entry.Value 
                         descending select entry;

            StreamWriter wr2 = new StreamWriter("dictionary.txt");
            foreach (KeyValuePair<char, int> entry in sorted)
            {
                wr2.WriteLine(entry);
            }
            wr2.Close();
        }
    
        public void setnodelist(Dictionary<char,int> Dict)
        {
            foreach (KeyValuePair<char, int> entry in Dict)
            {
                Node node = new Node(entry.Key, entry.Value);
                nodelist.Add(node);
            }
            while (nodelist.Count > 1)
            {
                nodelist.Sort((x, y) => x.CompareTo(y)); //sort nodelist by freq
                Node parentnode = new Node(nodelist[0], nodelist[1]); //parent
                //pop first two elements with least freq
                nodelist.RemoveAt(0);
                nodelist.RemoveAt(0);
                nodelist.Add(parentnode);
                Root = parentnode;
            }
           
        }
        
        public void setHuffmanCode(Node node, string str)
        {
            if (node.Left == null || node.Right == null)
            {
                HuffmanDict[node.Data] = str;
                return;
            }
            else
            {
                //recursion
                setHuffmanCode(node.Left, str + "0");
                setHuffmanCode(node.Right, str + "1");
            }

        }

        public void Decode(string compressed)
        {
            bool flag = false;
            Node ptr = Root;
            foreach (char bit in compressed)
            {
                if(flag)
                {
                    ptr = Root;
                    flag = false;
                }
                if(bit=='1')
                {
                    ptr = ptr.Right;
                }
                else
                {
                    ptr = ptr.Left;
                }
                if (ptr.Right == null || ptr.Left == null)
                {
                    decoded += ptr.Data;
                    flag = true;
                }
            }
        }
        public Image Draw()
        {
            GC.Collect();// collects the unreffered locations on the memory
            int temp;
            return Root == null ? null : Root.Draw(out temp);
        }
        
    }

    
}

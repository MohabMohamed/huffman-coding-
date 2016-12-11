using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace huffman_coding_1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

      

        private void button1_Click(object sender, EventArgs e)
        {
            StreamWriter wr = new StreamWriter("message.txt");
            wr.WriteLine(textBox1.Text);
            wr.Close();
            
            string input = File.ReadAllText("message.txt").Replace("\r\n","");
            
            HuffmanTree tree = new HuffmanTree();

            tree.setDict(input);

            tree.setnodelist(tree.Dict);

            tree.setHuffmanCode(tree.nodelist[0], ""); //root node


            StreamWriter wr3 = new StreamWriter("code.txt");
            foreach (KeyValuePair<char, string> entry in tree.HuffmanDict)
            {
                wr3.WriteLine(entry);
            }
            
            wr3.Close();
            string text = File.ReadAllText("code.txt");
            textBox4.Text = (text);
            //generate compressed code

            string compressed = "";
            foreach (char entry in input)
            {
                compressed += tree.HuffmanDict[entry];
            }

            StreamWriter wr4 = new StreamWriter("compressed.txt");
            wr4.Write(compressed);
            wr4.Close();
            tree.Decode(compressed);
            textBox2.Text = (tree.decoded);
            textBox3.Text = (compressed);
            float inp = (input.Length * 8), compr=compressed.Length;
            string compressionrate = "Total number of character in original text " + input.Length.ToString() + "@Bits required in original text " + inp.ToString() + "@Total bits required after compression "+compr.ToString() + "@Rate of compression " + (100-(compr/inp)*100).ToString() + "%";
            compressionrate = compressionrate.Replace("@",System.Environment.NewLine);
            textBox5.Text=(compressionrate);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}

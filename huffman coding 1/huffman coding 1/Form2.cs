using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace huffman_coding_1
{
    public partial class Form2 : Form
    {

        public Form2()
        {
            InitializeComponent();
            pictureBox1.Image = null;
            pictureBox1.Image = Form1.tree.Draw();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace huffman_coding_1
{
    public class Node
    {

        public char Data { get; set; }
        public int Frequency { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }
        public string huffman_code {get; set;}
        private int lastImageLocationOfStarterNode;
        // the backgroung image of each nodes, the size of this bitmap affects the quality of output image
        private static Bitmap nodeBg = new Bitmap(60, 50);
        // the free space between nodes on the drawed image
        private static Size freeSpace = new Size(nodeBg.Width / 6, (int)(nodeBg.Height * 1.3f));
        public string ShowUpData = "";
        private static readonly float Coef = nodeBg.Width / 40f;
        Image lastImage;
        private static Font font = new Font("Tahoma", 14f * Coef);
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
        public Image Draw(out int center)
        {
            center = lastImageLocationOfStarterNode;
            
            var lCenter = 0;
            var rCenter = 0;

            Image lNodeImg = null, rNodeImg = null;
            if (Left != null)       // draw left node's image
            {
                Left.ShowUpData = "0";
                lNodeImg = Left.Draw(out lCenter); }
            if (Right != null)      // draw right node's image
            {
                Right.ShowUpData = "1";
                rNodeImg = Right.Draw(out rCenter); }

            // draw current node and it's childs (left node image and right node image)
            var lSize = new Size();
            var rSize = new Size();
            var under = (lNodeImg != null) || (rNodeImg != null);// if true the current node has childs
            if (lNodeImg != null)
                lSize = lNodeImg.Size;
            if (rNodeImg != null)
                rSize = rNodeImg.Size;

            var maxHeight = lSize.Height;
            if (maxHeight < rSize.Height)
                maxHeight = rSize.Height;

            if (lSize.Width <= 0)
                lSize.Width = (nodeBg.Width - freeSpace.Width) / 2;
            if (rSize.Width <= 0)
                rSize.Width = (nodeBg.Width - freeSpace.Width) / 2;

            var resSize = new Size
            {
                Width = lSize.Width + rSize.Width + freeSpace.Width,
                Height = nodeBg.Size.Height + (under ? maxHeight + freeSpace.Height : 0)
            };

            var result = new Bitmap(resSize.Width, resSize.Height);
            var g = Graphics.FromImage(result);
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.FillRectangle(Brushes.White, new Rectangle(new Point(0, 0), resSize));
            g.DrawImage(nodeBg, lSize.Width - nodeBg.Width * 2 + freeSpace.Width / 2, 0);
            if (Left == null && Right == null) ShowUpData += Data;
            var str = ShowUpData +" "+ Frequency.ToString();
           
            g.DrawString(str, font, Brushes.Black, lSize.Width - nodeBg.Width / 2 + freeSpace.Width / 2 + (2 + (str.Length == 1 ? 10 : str.Length == 2 ? 5 : 0)) * Coef, nodeBg.Height / 2f - 12 * Coef);

            
            center = lSize.Width + freeSpace.Width / 2;
            var pen = new Pen(Brushes.Black, 2f * Coef)
            {
                EndCap = LineCap.ArrowAnchor,
                StartCap = LineCap.Round
            };


            float x1 = center;
            float y1 = nodeBg.Height;
            float y2 = nodeBg.Height + freeSpace.Height;
            float x2 = lCenter;
            var h = Math.Abs(y2 - y1);
            var w = Math.Abs(x2 - x1);
            if (lNodeImg != null)
            {
                g.DrawImage(lNodeImg, 0, nodeBg.Size.Height + freeSpace.Height);
                var points1 = new List<PointF>
                                  {
                                      new PointF(x1, y1),
                                      new PointF(x1 - w/6, y1 + h/3.5f),
                                      new PointF(x2 + w/6, y2 - h/3.5f),
                                      new PointF(x2, y2),
                                  };
                g.DrawCurve(pen, points1.ToArray(), 0.5f);
            }
            if (rNodeImg != null)
            {
                g.DrawImage(rNodeImg, lSize.Width + freeSpace.Width, nodeBg.Size.Height + freeSpace.Height);
                x2 = rCenter + lSize.Width + freeSpace.Width;
                w = Math.Abs(x2 - x1);
                var points = new List<PointF>
                                 {
                                     new PointF(x1, y1),
                                     new PointF(x1 + w/6, y1 + h/3.5f),
                                     new PointF(x2 - w/6, y2 - h/3.5f),
                                     new PointF(x2, y2)
                                 };
                g.DrawCurve(pen, points.ToArray(), 0.5f);
   
            }
            lastImage = result;
            lastImageLocationOfStarterNode = center;
            return result;
        }
        
    }

   

}

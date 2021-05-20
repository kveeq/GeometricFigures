using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        PointF vertex0;
        float r;
        PointF[] vertexes = new PointF[3];

        public Form1()
        {
            InitializeComponent();
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //Запоминаем вторую вершину

                try
                {
                    //Считаем третью вершину
                    vertexes[2] = GetThirdVertex(vertexes[0], vertexes[1]);
                }
                catch (Exception)
                {
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            Graphics g = panel2.CreateGraphics();
            g.Clear(Color.Silver);
        }

        private PointF GetThirdVertex(PointF FirstVertex, PointF SecondVertex)
        {
            //Угол наклона прямой, образованной известными вершинами
            double alpha = Math.Atan((FirstVertex.Y - SecondVertex.Y) / (FirstVertex.X - SecondVertex.X));
            /* k13 - коэффициент наклона прямой, проходящей через первую и третью вершину;
             * k23 - коэффициент наклона прямой, проходящей через вторую и третью вершину;*/
            double k13 = 0, k23 = 0;
            float x, y;
            //Вычисление угловых коэффициентов в зависимости от угла наклона первой стороны
            if (FirstVertex.Y > SecondVertex.Y)
            {
                k13 = Math.Tan(alpha - 2 * Math.PI / 3);
                k23 = Math.Tan(alpha - Math.PI / 3);
            }
            else if (FirstVertex.Y <= SecondVertex.Y)
            {
                k13 = Math.Tan(alpha + Math.PI / 3);
                k23 = Math.Tan(alpha + 2 * Math.PI / 3);
            }
            //Координата X третьей вершины
            x = (float)((k13 * FirstVertex.X - k23 * SecondVertex.X + SecondVertex.Y - FirstVertex.Y) / (k13 - k23));
            //Координата Y третьей вершины
            y = (float)(k13 * (x - FirstVertex.X) + FirstVertex.Y);

            return new PointF(x, y);
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            vertexes[1] = e.Location;
            r = vertexes[1].X - vertex0.X;

            if (btn_Circle.Focused)
            {
                Circle circle = new Circle(vertex0.X, vertex0.Y, r);
                circle.Draw(panel2, button10, trackBar1);
            }
            else if (btn_section.Focused)
            {
                Section section = new Section(vertex0.X, vertex0.Y, vertexes[1].X, vertexes[1].Y);
                section.Draw(panel2, button10, trackBar1);
            }
            else if (btn_Rectangle.Focused)
            {
                float width = Math.Abs(vertexes[1].X - vertex0.X);
                float height = Math.Abs(vertexes[1].Y - vertex0.Y);
                Rectangles rectangle = new Rectangles(vertex0.X, vertex0.Y, width, height);
                rectangle.Draw(panel2, button10, trackBar1);
            }
            else if (btn_triangle.Focused)
            {
                vertexes[0] = vertex0;
                Triangle triangle = new Triangle(vertexes);
                triangle.Draw(panel2, button10, trackBar1);
            }
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            vertex0 = e.Location;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                button10.BackColor = colorDialog1.Color;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button10.BackColor = ((Button)sender).BackColor;
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string tempPath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                tempPath = saveFileDialog.FileName;
            }
            Rectangle rect = new Rectangle(Point.Empty, panel2.Size);
            Bitmap bitmap = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            panel2.DrawToBitmap(bitmap, rect);
            bitmap.Save(tempPath, ImageFormat.Jpeg);
        }

        private void btn_Open_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                panel2.BackgroundImage = Image.FromFile(openFileDialog.FileName);
            }
            openFileDialog.Dispose();
        }
    }
}

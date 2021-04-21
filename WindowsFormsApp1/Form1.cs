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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        PointF vertex0;
        PointF[] vertexes = new PointF[3];
        Bitmap bmp;

        public Form1()
        {
            InitializeComponent();
            bmp = new Bitmap(panel2.Width, panel2.Height);
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
            float r = vertexes[1].X - vertex0.X;

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
                Rectangle rectangle = new Rectangle(vertex0.X, vertex0.Y, width, height);
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
    }


    class Figure
    {
        protected float X;
        protected float Y;
        protected PointF[] vertexes;
        //public Bitmap bmp;

        public Figure(float newX, float newY)
        {
            X = newX;
            Y = newY;
        }

        public Figure(PointF[] vertex)
        {
            vertexes = vertex;
        }

        public virtual bool Draw(int newX, int newY)
        {
            return false;
        }
    }

    class Circle : Figure
    {
        float radius;
        public Circle(float newX, float newY, float R) : base(newX, newY)
        {
            radius = R;
        }

        public void Draw(Panel panel2, Button btn, TrackBar trackbar)
        { 
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawEllipse(pen, X, Y, radius, radius);
            //Graphics.FromImage(bmp).DrawEllipse(pen, X, Y, radius, radius);
            //panel2.DrawToBitmap(bmp, g.DrawEllipse());
            //g.CopyFromScreen = bmp;
            
        }
    }

    class Section : Figure
    {
        float X2;
        float Y2;
        public Section(float x1, float y1, float x2, float y2) : base(x1, y1)
        {
            X2 = x2;
            Y2 = y2;
        }

        public void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawLine(pen, X, Y, X2, Y2);
        }
    }

    class Rectangle : Figure
    {
        float width;
        float height;
        public Rectangle(float x1, float y1, float x2, float y2) : base(x1, y1)
        {
            width = x2;
            height = y2;
        }

        public void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawRectangle(pen, X, Y, width, height);
        }
    }

    class Triangle : Figure
    {
        public Triangle(PointF[] vertexes) : base(vertexes)
        {

        }

        public void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawPolygon(pen, vertexes);
        }
    }
}

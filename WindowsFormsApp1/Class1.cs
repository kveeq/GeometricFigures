using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    class Figure
    {
        protected float X;
        protected float Y;
        protected PointF[] vertexes;

        public Figure(float newX, float newY)
        {
            X = newX;
            Y = newY;
        }

        public Figure(PointF[] vertex)
        {
            vertexes = vertex;
        }

        public virtual void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {

        }
    }

    class Circle : Figure
    {
        float radius;
        public Circle(float newX, float newY, float R) : base(newX, newY)
        {
            radius = R;
        }

        public override void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawEllipse(pen, X, Y, radius, radius);
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

        public override void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawLine(pen, X, Y, X2, Y2);
        }
    }

    class Rectangles : Figure
    {
        float width;
        float height;
        public Rectangles(float x1, float y1, float x2, float y2) : base(x1, y1)
        {
            width = x2;
            height = y2;
        }

        public override void Draw(Panel panel2, Button btn, TrackBar trackbar)
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

        public override void Draw(Panel panel2, Button btn, TrackBar trackbar)
        {
            Pen pen = new Pen(Color.Black, trackbar.Value);
            pen.Color = btn.BackColor;
            Graphics g = panel2.CreateGraphics();
            g.DrawPolygon(pen, vertexes);
        }
    }
}

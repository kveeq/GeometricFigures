using System;

namespace Console_Geometric_Figures
{
    class Program
    {
        static void Main(string[] args)
        {
            Triangle test = new Triangle(1, 1, 2, 2);
            test.Draw(3, 3);
        }
    }

    class GeometricfFigures
    {
        public int x;
        public int y;


        public GeometricfFigures(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public virtual void Draw(int width, int height)
        {
            Console.WriteLine("Figure is drawn");
        }
    }

    class Section : GeometricfFigures
    {
        public Section(int x, int y) : base(x, y)
        {

        }

        public override void Draw(int length, int height)
        {
            Console.WriteLine($"Section with length {length} with starting points X:{x} and Y:{y} is drawn");
        }
    }

    class Rectangle : GeometricfFigures
    {
        public Rectangle(int x, int y) : base(x, y)
        {

        }

        public override void Draw(int width, int height)
        {
            Console.WriteLine($"a Rectangle with width {width} and height {height} with starting points X:{x} and Y:{y} is drawn");
        }
    }

    class Triangle : GeometricfFigures
    {
        private int x2;
        private int y2;

        public Triangle(int x, int y, int x2, int y2) : base(x, y)
        {
            this.x2 = x2;
            this.y2 = y2;
        }

        public override void Draw(int x3, int y3)
        {
            Console.WriteLine($"a Triangle with first point X:{x} Y:{y} second point X:{x2} Y:{y2} and thirt point X:{x3} Y:{y3} is drawn");
        }
    }

    class Circle : GeometricfFigures
    {
        public Circle(int x, int y) : base(x, y)
        {

        }

        public override void Draw(int radius, int height)
        {
            Console.WriteLine($"a Circle with radius {radius} and with starting points X:{x} Y:{y} is drawn");
        }
    }
}

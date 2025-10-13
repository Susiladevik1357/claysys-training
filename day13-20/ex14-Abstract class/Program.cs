using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
public abstract class Shape
{
    public abstract double GetArea();
    public abstract double GetPerimeter();
}
public class Rectangle : Shape
{
    public double Length { get; set; }
    public double Width { get; set; }
    public Rectangle(double length, double width)
    {
        Length = length;
        Width = width;
    }
    public override double 
        GetArea()
    {
        return Length * Width;
    }
    public override double
        GetPerimeter()
    {
        return 2 * (Length + Width);
    }
}
public class Triangle : Shape
{
    public double SideA { get; set; }
    public double SideB { get; set; }
    public double SideC { get; set; }
    public Triangle(double a, double b, double c)
    {
        SideA = a; ;
        SideB = b;
        SideC = c;
    }
    public override double GetArea()
        {
            double s = (SideA + SideB + SideC) / 2;
            return Math.Sqrt(s * (s - SideA) * (s - SideB) * (s - SideC));
        }
        public override double GetPerimeter()
    {
        return SideA + SideB + SideC;
    }
}
public class Circle : Shape
{
    public double Radius { get; set; }
    public Circle(double radius)
    {
        Radius = radius;
    }
    public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
        public override double GetPerimeter()
    {
        return 2 * Math.PI * Radius;
    }
}
class Program
{
    static void Main(string[] args)
    {
        List<Shape> shapes = new List<Shape>();
        shapes.Add(new Rectangle(5, 3));
        shapes.Add(new Triangle(3, 4, 5));
        shapes.Add(new Circle(2.5));
        double totalArea = 0;
        double totalPerimeter = 0;
        foreach(var shape in shapes)
        {
            Console.WriteLine($"{shape.GetType().Name}-Area:{shape.GetArea():F2},Perimeter:{shape.GetPerimeter():F2}");
            totalArea += shape.GetArea();
            totalPerimeter += shape.GetPerimeter();
        }
        Console.WriteLine($"\nTotal Area of all shapes:{totalArea:F2}");
        Console.WriteLine($"Total Perimeter of all shapes:{totalPerimeter:F2}");

    }
}

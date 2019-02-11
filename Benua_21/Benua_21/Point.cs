using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benua_21
{
    public class Point
    {
        /// <summary>
        /// X axis coord
        /// </summary>
        public double X { get; private set; }

        /// <summary>
        /// Y axis coord
        /// </summary>
        public double Y { get; private set; }

        /// <summary>
        /// Empty Constructor
        /// </summary>
        public Point() { }

        /// <summary>
        /// Constructor with two coordinates
        /// </summary>
        /// <param name="x"> X axis coordinate</param>
        /// <param name="y"> Y axis coordinate</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }
        /// <summary>
        /// Overridden  multiplication for Point and scalar
        /// </summary>
        /// <param name="a">Point to be multiplied</param>
        /// <param name="b">scalar to be multiplied on</param>
        /// <returns></returns>
        public static Point operator *(Point a, double b)
        {
            return new Point(a.X * b, a.Y * b);
        }
        /// <summary>
        /// Overriden division for Point and scalar
        /// </summary>
        /// <param name="a">Point to be divide </param>
        /// <param name="b">scalar to be divide on</param>
        /// <returns></returns>
        public static Point operator /(Point a, double b)
        {
            return new Point(a.X / b, a.Y / b);
        }
        /// <summary>
        /// Overridden sum for two Points
        /// </summary>
        /// <param name="a">first Point</param>
        /// <param name="b">second  Point</param>
        /// <returns></returns>
        public static Point operator +(Point a, Point b)
        {
            return new Point(a.X + b.X, a.Y + b.Y);
        }
        /// <summary>
        /// Overriden subtraction for two Points
        /// </summary>
        /// <param name="a">first Point</param>
        /// <param name="b"> second Point</param>
        /// <returns></returns>
        public static Point operator -(Point a, Point b)
        {
            return new Point(a.X - b.X, a.Y - b.Y);
        }
        /// <summary>
        /// Rotates Vector, represented by Point by angle
        /// </summary>
        /// <param name="a">Vector to be rotated</param>
        /// <param name="angle"> angle to be rotated On</param>
        /// <returns></returns>
        public static Point Rotate(Point a, double angle)
        {
            return new Point(a.X * Math.Cos(angle) - a.Y * Math.Sin(angle),
                a.X * Math.Sin(angle) + a.Y * Math.Cos(angle));
        }
        /// <summary>
        /// Overriden TOtring method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}

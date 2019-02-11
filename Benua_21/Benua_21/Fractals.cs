using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benua_21
{

    public abstract class Fractal
    {
        /// <summary>
        /// Pen for erasing
        /// </summary>
        public static Pen eraser = new Pen(Color.LightGray, (float)3.1);
        /// <summary>
        /// for zoom and reating image with good quality
        /// </summary>
        public static float imageQualityFactor = (float)1;
        /// <summary>
        /// Thickness of Pen on first iteration
        /// </summary>
        public static float startThickness = 1;
        /// <summary>
        /// Drawing left high point - start point
        /// </summary>
        public static Point offsetPoint = new Point();
        /// <summary>
        /// Start Len/radius for elements in Fractal
        /// </summary>
        public double StartLen { get;protected set; }
        /// <summary>
        /// Color for first iteration
        /// </summary>
        public Color StartColor { get; protected set; }
        /// <summary>
        /// Color for last iteration
        /// </summary>
        public Color EndColor { get; protected set; }
        /// <summary>
        /// Maximal depth for fractal
        /// </summary>
        public int MaxDepth { get; protected set; }
        /// <summary>
        /// Current dept for drawing subfractal
        /// </summary>
        public int CurDepth { get; protected set; }
        /// <summary>
        /// Thickness decrease by iteration
        /// </summary>
        //public static float thicknessChangePerIteration = 0.1f;

        /// <summary>
        /// empty Contructor
        /// </summary>
        protected Fractal()
        {
            StartLen = MaxDepth = CurDepth = 0;
            StartColor = EndColor = Color.Black;
        }

        /// <summary>
        /// Full constructor
        /// </summary>
        /// <param name="startLen">Len or radius for first iteration</param>
        /// <param name="startColor">Color for first iteration</param>
        /// <param name="endColor">Maximal depth for fractal</param>
        /// <param name="maxDepth">Maximal depth for fractal</param>
        /// <param name="curDepth">Current dept for drawing subfractal</param>
        protected Fractal(double startLen, Color startColor, Color endColor, int maxDepth, int curDepth = 0)
        {
            StartLen = startLen;
            StartColor = startColor;
            EndColor = endColor;
            MaxDepth = maxDepth;
            CurDepth = curDepth;
        }
        /// <summary>
        /// Abstract function to draw current part of fractal
        /// </summary>
        /// <param name="image">Image to draw on</param>
        /// <param name="pt1">leftmost point</param>
        /// <param name="pt2">rightmost point</param>
        public abstract void Draw(Bitmap image, Point pt1, Point pt2 = null, int helper = 0);

        /// <summary>
        /// Generates Color for given iteration
        /// </summary>
        /// <param name="start">color for first iteration</param>
        /// <param name="end"> color for last iteration</param>
        /// <param name="depth"> current depth of fractal's part</param>
        /// <param name="maxDepth"> maximum depth for fractal</param>
        /// <returns>Gradient color for given iteration</returns>
        public static Color GetGradientColor(Color start, Color end, int depth, int maxDepth)
        {
            int rMin = start.R;
            int rMax = end.R;

            int gMin = start.G;
            int gMax = end.G;

            int bMin = start.B;
            int bMax = end.B;

            int neededR = rMin + (int)((double) (rMax - rMin) * depth / maxDepth);

            int neededG = gMin + (int)((double)(gMax - gMin) * depth / maxDepth);

            int neededB = bMin + (int)((double)(bMax - bMin) * depth / maxDepth);

            return Color.FromArgb(neededR, neededG, neededB);
        }
    }

    

    

    
}

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benua_21
{
    /// <summary>
    /// Class for describing H-Fractal
    /// </summary>
    public class HFractal : Fractal
    {
        /// <summary>
        /// Empty constuctor
        /// </summary>
        public HFractal()
        {
        }
        /// <summary>
        /// Full Contructor
        /// </summary>
        /// <param name="startLen">len or radius of items on first iteration</param>
        /// <param name="startColor"> first iteration color</param>
        /// <param name="endColor">last iteration color</param>
        /// <param name="maxDepth">Maximum depth for fractal's parts</param>
        /// <param name="curDepth">current depth of subfractal</param>
        public HFractal(double startLen, Color startColor, Color endColor, int maxDepth = 10, int curDepth = 0) : base(startLen, startColor,
            endColor, maxDepth, curDepth)
        {
        }
        /// <summary>
        /// method for drawing H-Fractal on image
        /// </summary>
        /// <param name="image">image to draw on</param>
        /// <param name="A">leftmost Point</param>
        /// <param name="E">rightmost Point</param>
        public override void Draw(Bitmap image, Point pt1, Point pt2 = null, int helper = 0)
        {
            if (MaxDepth == CurDepth)
            {
                return;
            }
            double curLen = StartLen / (Math.Pow(2, CurDepth));
            /*
             *
             *C |    |D
             * A|____|B
             *  |    |
             *E |    |F
             */
            //pt1 -= offsetPoint;

            Point A = new Point(pt1.X - curLen / 2, pt1.Y);
            Point B = new Point(pt1.X + curLen / 2, pt1.Y);
            Point C = new Point(A.X, A.Y + curLen / 2);

            Point D = new Point(B.X, B.Y + curLen / 2);

            Point E = new Point(A.X, A.Y - curLen / 2);

            Point F = new Point(B.X, B.Y - curLen / 2);


            Pen GradientPen = new Pen(Fractal.GetGradientColor(StartColor, EndColor, CurDepth, MaxDepth), (startThickness));
            using (var graphics = Graphics.FromImage(image))
            {
                var offA = (A - offsetPoint) * imageQualityFactor;
                var offB = (B - offsetPoint) * imageQualityFactor;
                var offC = (C - offsetPoint) * imageQualityFactor;
                var offD = (D - offsetPoint) * imageQualityFactor;
                var offF = (F - offsetPoint) * imageQualityFactor;
                var offE = (E - offsetPoint) * imageQualityFactor;


                graphics.DrawLine(GradientPen, (float)offA.X, (float)offA.Y, (float)offE.X, (float)offE.Y);
                graphics.DrawLine(GradientPen, (float)offA.X, (float)offA.Y, (float)offC.X, (float)offC.Y);
                graphics.DrawLine(GradientPen, (float)offA.X, (float)offA.Y, (float)offB.X, (float)offB.Y);
                graphics.DrawLine(GradientPen, (float)offB.X, (float)offB.Y, (float)offF.X, (float)offF.Y);
                graphics.DrawLine(GradientPen, (float)offB.X, (float)offB.Y, (float)offD.X, (float)offD.Y);
            }

            Point[] arr = { C, E, D, F };
            for (int i = 0; i < arr.Length; ++i)
            {
                HFractal nextStepFractal = new HFractal(StartLen, StartColor, EndColor, MaxDepth, CurDepth + 1);
                nextStepFractal.Draw(image, arr[i]);
            }

        }

    }
}

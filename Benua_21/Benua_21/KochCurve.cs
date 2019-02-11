using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benua_21
{
    /// <summary>
    /// Koch Curve Fractal class
    /// </summary>
    public class KochCurve : Fractal
    {
        /// <summary>
        /// Empty constructor
        /// </summary>
        public KochCurve()
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
        public KochCurve(double startLen, Color startColor, Color endColor, int maxDepth = 10, int curDepth = 0) : base(
            startLen, startColor,
            endColor, maxDepth, curDepth)
        {
        }

        /// <summary>
        /// Draws Koch Curve fractal
        /// </summary>
        /// <param name="image"></param>
        /// <param name="A"></param>
        /// <param name="E"></param>
        /// <param name="helper"></param>
        public override void Draw(Bitmap image, Point A, Point E, int helper = 0)
        {
            /*       C
             *  A__B/\D__E
             */
            if (CurDepth == MaxDepth)
            {
                Pen gradientPen = new Pen(Fractal.GetGradientColor(StartColor, EndColor, helper, MaxDepth),
                    (float)(startThickness));
                using (var graphics = Graphics.FromImage(image))
                {
                    var offA = (A - offsetPoint) * imageQualityFactor;
                    var offE = (E - offsetPoint) * imageQualityFactor;
                    graphics.DrawLine(gradientPen, (float)offA.X, (float)offA.Y, (float)offE.X, (float)offE.Y);
                }
            }
            else
            {
                Point B = A + (E - A) / 3;
                Point D = A + (E - A) * (2.0 / 3);

                Point temp = D - B;
                temp = Point.Rotate(temp, -Math.PI / 3);
                Point C = B + temp;

                Point[] arr = new Point[5] { A, B, C, D, E };
                for (int i = 0; i < 4; ++i)
                {
                    var newFractal = new KochCurve(StartLen, StartColor, EndColor, MaxDepth, CurDepth + 1);
                    int newHelper = helper;
                    if (i == 1 || i == 2)
                    {
                        newHelper++;
                    }

                    newFractal.Draw(image, arr[i], arr[i + 1], newHelper);
                }
            }
        }
    }
}

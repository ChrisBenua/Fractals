using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Benua_21
{
    /// <summary>
    /// class for describing CircleFractal
    /// </summary>
    public class CircleFractal : Fractal
    {
        /// <summary>
        /// Empty consructor
        /// </summary>
        public CircleFractal() { }
        /// <summary>
        /// Full Contructor
        /// </summary>
        /// <param name="startLen">radius of items on first iteration</param>
        /// <param name="startColor"> first iteration color</param>
        /// <param name="endColor">last iteration color</param>
        /// <param name="maxDepth">Maximum depth for fractal's parts</param>
        /// <param name="curDepth">current depth of subfractal</param>
        public CircleFractal(double startLen, Color startColor, Color endColor, int maxDepth = 10, int curDepth = 0) : base(startLen, startColor,
            endColor, maxDepth, curDepth)
        { }
        /// <summary>
        /// method for drawing Circle fractal on image
        /// </summary>
        /// <param name="image">image to draw on</param>
        /// <param name="A">center Point</param>
        /// <param name="E">null</param>
        public override void Draw(Bitmap image, Point pt1, Point pt2 = null, int helper = 0)
        {
            if (CurDepth == MaxDepth)
            {
                return;
            }

            Pen gradientPen = new Pen(Fractal.GetGradientColor(StartColor, EndColor, CurDepth, MaxDepth), startThickness);
            double curRadius = StartLen / (Math.Pow(3, CurDepth));

            Point[] arr = new Point[7];
            arr[0] = pt1;
            double distFromCenter = curRadius * 2 / 3;

            for (int i = -1; i < 6; ++i)
            {

                Point rotated = new Point(-distFromCenter, 0);
                rotated = Point.Rotate(rotated, Math.PI / 6 + Math.PI * i / 3);
                Point diag = new Point(-curRadius / 3 * Math.Sqrt(2), 0);
                diag = Point.Rotate(diag, Math.PI / 4);
                arr[i + 1] = rotated + pt1;

                //drawing circle, that is in our center
                if (i == -1)
                {
                    arr[0] = pt1;
                }

                Point temp = arr[i + 1] + diag;


                using (var graphics = Graphics.FromImage(image))
                {

                    graphics.DrawEllipse(gradientPen,
                        new RectangleF((float)(temp.X - offsetPoint.X) * imageQualityFactor, (float)(temp.Y - offsetPoint.Y) * imageQualityFactor, (float)curRadius / 3 * (float)2 * imageQualityFactor, (float)curRadius / 3 * (float)2 * imageQualityFactor));
                }

                CircleFractal fractal = new CircleFractal(StartLen, StartColor, EndColor, MaxDepth, CurDepth + 1);
                fractal.Draw(image, arr[i + 1]);
            }

        }
    }
}

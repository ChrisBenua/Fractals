using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Encoder = System.Drawing.Imaging.Encoder;

namespace Benua_21
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Previous numericUpDown element value
        /// </summary>
        private int prevNumericUpDownValue = 1;
        /// <summary>
        /// Arrays of Zooms factors
        /// </summary>
        private int[] selectedIndexToZoom = {1, 2, 3, 5};
        /// <summary>
        /// ComplexityPowers of fractal
        /// </summary>
        private int[] complexityFactor = {3, 4, 7};
        /// <summary>
        /// Image for saving fractal on
        /// </summary>
        private Bitmap lastImage;

        private double mEndX, mEndY;

        /// <summary>
        /// move on x-axis
        /// </summary>
        private double moveDx;
        /// <summary>
        /// move on y-axis
        /// </summary>
        private double moveDy;
        private double mStartX, mStartY;

        /// <summary>
        /// Height diff between picturebox and form
        /// </summary>
        private double heightDiff = 0;
        /// <summary>
        /// width diff between picturebox and form
        /// </summary>
        private double widthDiff = 0;
        /// <summary>
        /// Start color for linear gradient
        /// </summary>
        private Color startColor = System.Drawing.Color.Black;
        /// <summary>
        /// end color for linear gradient
        /// </summary>
        private Color endColor = System.Drawing.Color.Black;
        /// <summary>
        /// User started to drag
        /// </summary>
        private bool isMoving = false;

        public Form1()
        {
            InitializeComponent();
            //this.Resize += OnResize;
            //this.OnResizeEnd += OnResize;
            //picCanvas.SizeMode = PictureBoxSizeMode.StretchImage;
            //picCanvas.SizeMode = PictureBoxSizeMode.AutoSize;
            //this.MinimumSize = new Size(Screen.PrimaryScreen.Bounds.Width / 2, saveImageButton.Size.Height + saveImageButton.Location.Y + 40);
            this.selectZoomComboBox.Items.AddRange(new string[] {
                "1X",
                "2X",
                "3X",
                "5X"});
            picCanvas.BackColor = System.Drawing.Color.LightGray;
            this.ResizeEnd += OnResizeEnd;
            this.Resize += OnResize;
            this.Load += OnLoad;
            fractalComboBox.SelectedIndex = 0;
            fractalComboBox.SelectedIndexChanged += FractalComboBoxOnSelectedIndexChanged;
            selectZoomComboBox.SelectedIndex = 0;
            selectZoomComboBox.SelectedIndexChanged += SelectZoomComboBoxOnSelectedIndexChanged;
            picCanvas.MouseUp += PicCanvasOnMouseUp;
            picCanvas.MouseDown += PicCanvasOnMouseDown;
            saveImageButton.Click += SaveImageButtonOnClick;
            iterationsUpDown.Maximum = 8;
        }

        /// <summary>
        /// Handles tap on maximize button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Maximized)
            {
                this.OnResizeEnd(EventArgs.Empty);
            }
        }

        /// <summary>
        /// Saves Image of selected fractal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveImageButtonOnClick(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            
            dialog.Filter = "Images (*.png)|*.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string filename = dialog.FileName;
                {
                    double dx = moveDx;
                    double dy = moveDy;
                    moveDx = 0;
                    moveDy = 0;
                    Fractal.imageQualityFactor = 10;
                    Fractal.eraser.Color = Color.White;
                    Fractal.startThickness = 4f;
                    DrawSelectedFractal(fractalComboBox.SelectedIndex, false);
                    moveDx = dx;
                    moveDy = dy;
                }
                Fractal.startThickness = 1f;

                Fractal.eraser.Color = Color.LightGray;
                Fractal.imageQualityFactor = selectedIndexToZoom[selectZoomComboBox.SelectedIndex];
   
                lastImage.Save(filename, ImageFormat.Png);
            }
        }
        /// <summary>
        /// Handling dragging: Begin of Drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicCanvasOnMouseDown(object sender, MouseEventArgs e)
        {
            mStartX = e.X;
            mStartY = e.Y;
            Console.WriteLine("start dragging");
            isMoving = true;
        }
        /// <summary>
        /// Handling Dragging : End of Drag
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PicCanvasOnMouseUp(object sender, MouseEventArgs e)
        {
            if (!isMoving)
            {
                return;
            }
            int zoom = selectedIndexToZoom[selectZoomComboBox.SelectedIndex];

            mEndX = e.X;
            mEndY = e.Y;
            //moving in diffrent zooms has diffrent impact on offset point
            moveDx += (mStartX - mEndX) / zoom;
            moveDy += (mStartY - mEndY) / zoom;
            Console.WriteLine("MOVING  " + moveDx + " " + moveDy);
            DrawSelectedFractal(fractalComboBox.SelectedIndex);
            isMoving = false;
        }
        /// <summary>
        /// Sets diffrent maximum value for numericUpDown element
        /// </summary>
        private void ValidateDepth()
        {
            if (fractalComboBox.SelectedIndex == 2)
            {
                iterationsUpDown.Maximum = 6;
                iterationsUpDown.Value = Math.Min(iterationsUpDown.Value, iterationsUpDown.Maximum);
            }
            else if (fractalComboBox.SelectedIndex == 1)
            {
                iterationsUpDown.Maximum = 9;
            }
            else
            {
                iterationsUpDown.Maximum = 8;
                iterationsUpDown.Value = Math.Min(iterationsUpDown.Value, iterationsUpDown.Maximum);
            }
        }
        /// <summary>
        /// Handling change of zoom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectZoomComboBoxOnSelectedIndexChanged(object sender, EventArgs e)
        {
            float prevZoom = Fractal.imageQualityFactor;
            int zoom = selectedIndexToZoom[selectZoomComboBox.SelectedIndex];
            Fractal.imageQualityFactor = zoom;
            Point centerPoint = new Point(moveDx + picCanvas.Width / prevZoom / 2, moveDy + picCanvas.Height / 2 / prevZoom);
            //to zoom in center
            Point newOrigin = centerPoint - new Point(picCanvas.Width / zoom / 2, picCanvas.Height / zoom / 2);
            // moveDx = (picCanvas.Width) / 4 * (zoom - 1) ;
            //moveDy = (picCanvas.Height) / 4 * (zoom - 1) ;
            moveDx = newOrigin.X;
            moveDy = newOrigin.Y;

            DrawSelectedFractal(fractalComboBox.SelectedIndex);
            
            //moveDy = moveDx = 0;
        }
        /// <summary>
        /// handling change of Fractal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FractalComboBoxOnSelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedState = fractalComboBox.SelectedIndex;
            ValidateDepth();
            moveDx = moveDy = 0;

            DrawSelectedFractal(selectedState);
        }

        private void OnLoad(object sender, EventArgs e)
        {
            SetDifferenceInWidthAndHeight();
            this.OnResizeEnd(EventArgs.Empty);

            DrawSelectedFractal(0);
        }

        /// <summary>
        /// Draws fractal depending on selected index in combobox
        /// </summary>
        /// <param name="index">selected index in combobox</param>
        /// <param name="flag">refresh pictureBox image?</param>
        private void DrawSelectedFractal(int index, bool flag = true)
        {
            Fractal.offsetPoint = new Point(moveDx, moveDy);
            if (index == 0)
            {
                DrowKochCurve(flag);
            }
            else if (index == 1)
            {
                DrawHFractal(flag);
            }
            else
            {
                DrawCircleFractal(flag);
            }
        }
        /// <summary>
        /// handles end of form's resize
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnResizeEnd(object sender, EventArgs e)
        {
            Console.WriteLine(widthDiff);
            picCanvas.Width = this.Width - (int)widthDiff;
            picCanvas.Height = this.Height - (int)heightDiff;
            //DrowKochCurve();
            DrawSelectedFractal(fractalComboBox.SelectedIndex);
        }
        /// <summary>
        /// calculates Difference in width and height between from and pictureBox
        /// </summary>
        private void SetDifferenceInWidthAndHeight()
        {
            heightDiff = this.Height - picCanvas.Height;
            widthDiff = this.Width - picCanvas.Width;
        }
        /// <summary>
        /// Draws Koch Curve
        /// </summary>
        /// <param name="flag">refresh image in imageBox</param>
        private void DrowKochCurve(bool flag = true)
        {
            Console.WriteLine("Redraw");
            Console.WriteLine("Size is ");
            Console.WriteLine(picCanvas.Width + " " + picCanvas.Height);
            Console.WriteLine(this.Width.ToString() + " " + this.Height.ToString());
            //mBm = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);
            double width = this.picCanvas.Width;
            double height = this.picCanvas.Height;
            double len = Math.Min((height-20) * 2 / Math.Sqrt(3), (width - 20) / 3);
            Console.WriteLine($"Len is {len}");
            lastImage = new Bitmap((int)(width * Fractal.imageQualityFactor), (int)(height * Fractal.imageQualityFactor));
            Point startPoint = new Point((width - 3 * len) / 2, height / 2 + len / 4 * Math.Sqrt(3));
            Point endPoint = new Point(width - (width - 3 *len) / 2, startPoint.Y);
            Console.WriteLine(startPoint);
            Console.WriteLine(endPoint);
            KochCurve curve = new KochCurve(0, startColor, endColor, (int)iterationsUpDown.Value);
            curve.Draw(lastImage, startPoint, endPoint);
            if (flag)
            {
                (picCanvas.Image)?.Dispose();
                picCanvas.Image = lastImage;
            }
        }
        /// <summary>
        /// Draws H-Fractal
        /// </summary>
        /// <param name="flag">refresh image on PictureBox?</param>
        private void DrawHFractal(bool flag = true)
        {
            Console.WriteLine("H-fractal");
            double width = this.picCanvas.Width;
            double height = this.picCanvas.Height;

            double maxLen = (double) Math.Min(picCanvas.Width, picCanvas.Height) / 2.1;
            Console.WriteLine($"MaxLen is {maxLen}");

            Point midPoint = new Point(width / 2, height / 2);
            lastImage = new Bitmap((int)(width * Fractal.imageQualityFactor), (int)(height * Fractal.imageQualityFactor));

            Console.WriteLine("MidPoint " + midPoint);

            HFractal fractal = new HFractal(maxLen, startColor, endColor, (int)iterationsUpDown.Value);
            fractal.Draw(lastImage, midPoint);
            if (flag)
            {
                (picCanvas.Image)?.Dispose();
                picCanvas.Image = lastImage;
            }
        }
        /// <summary>
        /// Draws Circle Fractal
        /// </summary>
        /// <param name="flag"> refresh image on pictureBox?</param>
        private void DrawCircleFractal(bool flag = true)
        {
            double width = this.picCanvas.Width;
            double height = this.picCanvas.Height;

            double maxLen = (double)Math.Min(picCanvas.Width, picCanvas.Height) / 2.1;
            Point midPoint = new Point(width / 2, height / 2);
            lastImage = new Bitmap((int)(width * Fractal.imageQualityFactor), (int)(height * Fractal.imageQualityFactor));
            Console.WriteLine(iterationsUpDown.Value);
            CircleFractal fractal = new CircleFractal(maxLen, startColor, endColor, (int)iterationsUpDown.Value);
            fractal.Draw(lastImage, midPoint);
            if (flag)
            {
                (picCanvas.Image)?.Dispose();
                picCanvas.Image = lastImage;
            }
        }
        /// <summary>
        /// Number of iterations changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IterationsUpDown_ValueChanged(object sender, EventArgs e)
        {
            if (Math.Pow((int) iterationsUpDown.Value, complexityFactor[fractalComboBox.SelectedIndex]) > 5000 && prevNumericUpDownValue < (int)iterationsUpDown.Value)
            {
                var result = MessageBox.Show("May cause very long response, do you want to continue?",
                    "Too much elements are going to be drawn", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    iterationsUpDown.Value = prevNumericUpDownValue;
                    return;
                }
                prevNumericUpDownValue = Math.Max(prevNumericUpDownValue, (int)iterationsUpDown.Value);

            }
            DrawSelectedFractal(fractalComboBox.SelectedIndex);
            prevNumericUpDownValue = (int) iterationsUpDown.Value;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Opens ColorDialog for choosing startColor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog startColorDialog = new ColorDialog();
            startColorDialog.AllowFullOpen = false;
            if (startColorDialog.ShowDialog() == DialogResult.OK)
            {
                startColor = startColorDialog.Color;
                DrawSelectedFractal(fractalComboBox.SelectedIndex);
                //DrowKochCurve();
            }
        }
        /// <summary>
        /// Opens Colordialog for choosing endColor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartEndColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog endColorDialog = new ColorDialog();
            endColorDialog.AllowFullOpen = false;
            if (endColorDialog.ShowDialog() == DialogResult.OK)
            {
                endColor = endColorDialog.Color;
                if (startColor != null)
                    DrawSelectedFractal(fractalComboBox.SelectedIndex);
            }
        }
    }
}
